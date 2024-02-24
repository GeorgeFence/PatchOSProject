using System;
using System.Collections.Generic;
using Sys = Cosmos.System;
using Cosmos.HAL;
using Cosmos.HAL.BlockDevice;
using Cosmos.HAL.BlockDevice.Registers;
using Cosmos.HAL.BlockDevice.Ports;
using Cosmos.Core;
using Cosmos.Common.Extensions;
using Cosmos.System.FileSystem;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace PatchOS.Files.Drivers
{
    public class AHCI_DISK
    {
        Cosmos.Debug.Kernel.Debugger ahciDebugger = new Cosmos.Debug.Kernel.Debugger("AHCI");

        public static PCIDevice device = PCI.GetDeviceClass(ClassID.MassStorageController, SubclassID.SATAController, ProgramIF.SATA_AHCI);

        private static List<StoragePort> ports = new List<StoragePort>();

        private static GenericRegisters generic;

        private static ulong mABAR;

        public static bool Supports64bitAddressing;

        public static bool SupportsNativeCommandQueuing;

        public static bool SupportsSNotificationRegister;

        public static bool SupportsMechanicalPresenceSwitch;

        public static bool SupportsStaggeredSpinup;

        public static bool SupportsAggressiveLinkPowerManagement;

        public static bool SupportsActivityLED;

        public static bool SupportsCommandListOverride;

        public static uint InterfaceSpeedSupport;

        public static bool SupportsAHCIModeOnly;

        public static bool SupportsPortMutliplier;

        public static bool FISBasedSwitchingSupported;

        public static bool PIOMultipleDRQBlock;

        public static bool SlumberStateCapable;

        public static bool PartialStateCapable;

        public static uint NumOfCommandSlots;

        public static bool CommandCompletionCoalsecingSupported;

        public static bool EnclosureManagementSupported;

        public static bool SupportsExternalSATA;

        public static uint NumOfPorts;

        public const ulong RegularSectorSize = 512uL;

        public string SerialNo;


        /// <summary>
        /// Init AHCI-SATA Disks
        /// </summary>
        public void Init()
        {
            Console.Clear();
            Console.WriteLine("_");
            try
            {
                //try { Cosmos.Core.Global.Init(); } catch { }
                PCI.Setup();
                Cosmos.HAL.BlockDevice.AHCI.Wait(1000);
                Cosmos.Core.ACPI.Start(true, true);
                Cosmos.Core.Bootstrap.Init();
                Cosmos.Core.Global.Init();

                int i = 0;
                foreach (var dvc in PCI.Devices)
                {
                    i++;
                    //var b0 = device.BAR0.ToString();
                    Console.Clear();
                    Console.WriteLine($"PCI #0{i}=> VendorID: {dvc.VendorID} |  BAR0: {dvc.BAR0}  | ProgIf: {dvc.ProgIF}  ||  BUS!: {dvc.bus} | func!: {dvc.function} | slot!: {dvc.slot} ||  Class: {dvc.ClassCode}  | Sub: {dvc.Subclass} | CMD: {GetCMD(dvc.Command)}");

                    if (dvc.ClassCode == 1 && dvc.Subclass == 6)
                    {
                        Console.WriteLine("in start----------");

                        PCIDevice pci = new PCIDevice(dvc.bus, dvc.slot, dvc.function);
                        Cosmos.HAL.BlockDevice.AHCI hci = new Cosmos.HAL.BlockDevice.AHCI(pci);
                        device = dvc;
                        pci.EnableBusMaster(enable: true);
                        pci.EnableMemory(enable: true);
                        pci.EnableDevice();
                        mABAR = pci.BaseAddressBar[5].BaseAddress;

                        generic = new GenericRegisters(pci.BaseAddressBar[5].BaseAddress);
                        generic.GlobalHostControl |= 2147483648u;
                        GetCapabilities();
                        ports.Capacity = (int)NumOfPorts;
                        GetPorts();


                        string mPortName; uint mPortNum = 0; uint aAddressX = (uint)pci.BaseAddressBar[0].BaseAddress + 20;
                        foreach (StoragePort port in ports)
                        {
                            Console.WriteLine($"STR PORT Name: {port.mPortName} | STR Num: {port.mPortNumber} | BlockSize: {port.BlockSize}");
                            if (port.mPortType == PortType.SATA) { mPortName = port.mPortName; mPortNum = port.mPortNumber; };
                            aAddressX = 128;
                            StoragePort.Devices.Add(port);
                        }
                        StoragePort.Devices.ForEach(device => { Console.WriteLine($"(old)FOUND A STORAGE DEVICE !. Type: {GetBlockType(device.Type)} | BlockSize: {device.BlockSize} | BlockCount: {device.BlockCount}"); });
                        Console.WriteLine($"ADDR: {aAddressX} | PortNum: {mPortNum}");
                        PortRegisters prtg = new PortRegisters(aAddressX, mPortNum);
                        Console.WriteLine($"is Active prtg : {prtg.Active} | port num: {prtg.mPortNumber} | port CMD: {prtg.CMD} | Resrv : {prtg.Reserved}");
                        Console.WriteLine("BEFORE");
                        BlockDevice block = null;
                        //try { block = new SATA(prtg); } catch (Exception ex) { Console.WriteLine($"!!!! ERR !: {ex.Message}"); }
                        Console.WriteLine("AFTER");

                        try { Cosmos.Core.Global.Init(); } catch { }

                        List<BlockDevice> satadevices = SATA.Devices;
                        Console.WriteLine($"--\namnt of satas : {satadevices.Count}\n");
                        foreach (var satadevice in satadevices)
                        {
                            Console.WriteLine($"one is bsize = {satadevice.BlockSize} | bcount = {satadevice.BlockCount} | {GetBlockType(satadevice.Type)}");

                        }
                        Console.Clear();
                    }
                }




                Console.Write("\n\n_");
            }
            catch (Exception ex) { BSOD(ex); }

        }

        /// <summary>
        /// Check if Init done
        /// </summary>
        public static void Check()
        {
            Console.Clear();
            Console.WriteLine("Initializing SATA - AHCI Storage Devices...");
            List<BlockDevice> satadevices = SATA.Devices;
            Console.WriteLine($"[ok] Amount of found devices: {satadevices.Count}\n");

        }

        private string GetCMD(PCIDevice.PCICommand type)
        {
            switch (type)
            {
                case PCIDevice.PCICommand.Wait:
                    return "Wait";
                case PCIDevice.PCICommand.VGA_Pallete:
                    return "VGA_PALLETE";
                case PCIDevice.PCICommand.Special:
                    return "Special";
                case PCIDevice.PCICommand.SERR:
                    return "SERR";
                case PCIDevice.PCICommand.Parity:
                    return "Parity";
                case PCIDevice.PCICommand.Memory:
                    return "Memory";
                case PCIDevice.PCICommand.Master:
                    return "Master";
                case PCIDevice.PCICommand.IO:
                    return "I/O";
                case PCIDevice.PCICommand.Invalidate:
                    return "Invalidate";
                case PCIDevice.PCICommand.Fast_Back:
                    return "Fast_Back 512";
                default:
                    return "Unknown CMD";
            }
        }

        private string GetPType(PortType port)
        {
            switch (port)
            {
                case PortType.Nothing:
                    return "N/A";
                case PortType.PM:
                    return "PM";
                case PortType.SATA:
                    return "SATA";
                case PortType.SATAPI:
                    return "SATA-PI";
                case PortType.SEMB:
                    return "SEMB";

                default:
                    return "Unknown";
            }
        }

        private string GetBlockType(BlockDeviceType type)
        {
            switch (type)
            {
                case BlockDeviceType.Removable:
                    return "Removable Disk";
                case BlockDeviceType.HardDrive:
                    return "Hard Drive";
                case BlockDeviceType.RemovableCD:
                    return "CD or DvD";
                default:
                    return "Unknown";
            }
        }

        private void BSOD(Exception exception)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("KERNEL ERROR:");
            Console.WriteLine($"{exception.GetType().ToString()}\n");
            Console.WriteLine($"Details: {exception.ToString()}\n");
            Console.WriteLine($"Message: {exception.Message}\n");
            //uint addV;
            //unsafe { Exception* ptr = &exception; addV = (uint)new IntPtr(ptr).ToInt32(); }

            Console.WriteLine($"\nInstruction Counter : {exception.HResult}");
            //Console.WriteLine($"Last Instruction : {CI}");
            //Console.WriteLine($"Linked to : {exception.HelpLink.ToCharArray()}");
            Console.WriteLine($"\n\n\n\n ERR_CODE = [0x{exception.HResult.ToHex()}]\n\nPress Any Key to Reset..");
            Console.ReadKey();
            Sys.Power.Reboot();
        }

        private void GetCapabilities()
        {
            NumOfPorts = generic.Capabilities & 0x1Fu;
            SupportsExternalSATA = ((generic.Capabilities >> 5) & 1) == 1;
            EnclosureManagementSupported = ((generic.Capabilities >> 6) & 1) == 1;
            CommandCompletionCoalsecingSupported = ((generic.Capabilities >> 7) & 1) == 1;
            NumOfCommandSlots = (generic.Capabilities >> 8) & 0x1Fu;
            PartialStateCapable = ((generic.Capabilities >> 13) & 1) == 1;
            SlumberStateCapable = ((generic.Capabilities >> 14) & 1) == 1;
            PIOMultipleDRQBlock = ((generic.Capabilities >> 15) & 1) == 1;
            FISBasedSwitchingSupported = ((generic.Capabilities >> 16) & 1) == 1;
            SupportsPortMutliplier = ((generic.Capabilities >> 17) & 1) == 1;
            SupportsAHCIModeOnly = ((generic.Capabilities >> 18) & 1) == 1;
            InterfaceSpeedSupport = (generic.Capabilities >> 20) & 0xFu;
            SupportsCommandListOverride = ((generic.Capabilities >> 24) & 1) == 1;
            SupportsActivityLED = ((generic.Capabilities >> 25) & 1) == 1;
            SupportsAggressiveLinkPowerManagement = ((generic.Capabilities >> 26) & 1) == 1;
            SupportsStaggeredSpinup = ((generic.Capabilities >> 27) & 1) == 1;
            SupportsMechanicalPresenceSwitch = ((generic.Capabilities >> 28) & 1) == 1;
            SupportsSNotificationRegister = ((generic.Capabilities >> 29) & 1) == 1;
            SupportsNativeCommandQueuing = ((generic.Capabilities >> 30) & 1) == 1;
            Supports64bitAddressing = ((generic.Capabilities >> 31) & 1) == 1;
        }

        private void GetPorts()
        {
            uint num = generic.ImplementedPorts;
            for (int i = 0; i < 32; i++)
            {
                if ((num & (true ? 1u : 0u)) != 0)
                {
                    PortRegisters portRegisters = new PortRegisters((uint)((int)mABAR + 256), (uint)i);
                    PortType portType = (portRegisters.mPortType = CheckPortType(portRegisters));
                    string text = "0:" + ((i.ToString().Length <= 1) ? i.ToString().PadLeft(1, '0') : i.ToString());
                    switch (portType)
                    {
                        case PortType.SATA:
                            {
                                ahciDebugger.Send("Initializing Port " + text + " with type SATA");
                                PortRebase(portRegisters, (uint)i);
                                SATA item = new SATA(portRegisters);
                                ports.Add(item);
                                break;
                            }
                        case PortType.SATAPI:
                            ahciDebugger.Send("Initializing Port " + text + " with type Serial ATAPI");
                            break;
                        case PortType.SEMB:
                            ahciDebugger.Send("SEMB Drive at port " + text + " found, which is not supported yet!");
                            break;
                        case PortType.PM:
                            ahciDebugger.Send("Port Multiplier Drive at port " + text + " found, which is not supported yet!");
                            break;
                        default:
                            throw new Exception("SATA Error");
                        case PortType.Nothing:
                            break;
                    }
                }

                num >>= 1;
            }
        }

        private void PortRebase(PortRegisters aPort, uint aPortNumber)
        {
            ahciDebugger.Send("Stop");
            if (!StopCMD(aPort))
            {
                SATA.PortReset(aPort);
            }

            aPort.CLB = 4194304 + 1024 * aPortNumber;
            aPort.FB = 4227072 + 256 * aPortNumber;
            aPort.SERR = 1u;
            aPort.IS = 0u;
            aPort.IE = 0u;
            new MemoryBlock(aPort.CLB, 1024u).Fill(0);
            new MemoryBlock(aPort.FB, 256u).Fill(0);
            GetCommandHeader(aPort);
            if (!StartCMD(aPort))
            {
                SATA.PortReset(aPort);
            }

            aPort.IS = 0u;
            aPort.IE = uint.MaxValue;
            ahciDebugger.Send("Finished!");
        }

        private PortType CheckPortType(PortRegisters aPort)
        {
            InterfacePowerManagementStatus interfacePowerManagementStatus = (InterfacePowerManagementStatus)((aPort.SSTS >> 8) & 0xFu);
            CurrentInterfaceSpeedStatus currentInterfaceSpeedStatus = (CurrentInterfaceSpeedStatus)((aPort.SSTS >> 4) & 0xFu);
            DeviceDetectionStatus deviceDetectionStatus = (DeviceDetectionStatus)(aPort.SSTS & 0xFu);
            uint sIG = aPort.SIG;
            if (interfacePowerManagementStatus != InterfacePowerManagementStatus.Active)
            {
                return PortType.Nothing;
            }

            if (deviceDetectionStatus != DeviceDetectionStatus.DeviceDetectedWithPhy)
            {
                return PortType.Nothing;
            }

            return (sIG >> 16) switch
            {
                0u => PortType.SATA,
                60180u => PortType.SATAPI,
                49980u => PortType.SEMB,
                38505u => PortType.PM,
                65535u => PortType.Nothing,
                _ => throw new Exception("SATA Error: Unknown drive found at port: " + aPort.mPortNumber),
            };
        }

        public static void HBAReset()
        {
            generic.GlobalHostControl = 1u;
            uint num = 0u;
            do
            {
                Wait(1);
            }
            while ((generic.GlobalHostControl & (true ? 1u : 0u)) != 0);
        }

        public static void Wait(int microsecondsTimeout)
        {
            for (int i = 0; i < microsecondsTimeout; i++)
            {
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
                IOPort.Wait();
            }
        }

        private bool StartCMD(PortRegisters aPort)
        {
            int i;
            for (i = 0; i < 101; i++)
            {
                if ((aPort.CMD & 0x8000) == 0)
                {
                    break;
                }

                Wait(5000);
            }

            if (i == 101)
            {
                return false;
            }

            aPort.CMD |= 16u;
            aPort.CMD |= 1u;
            return true;
        }

        private bool StopCMD(PortRegisters aPort)
        {
            aPort.CMD &= 4294967294u;
            int i;
            for (i = 0; i < 101; i++)
            {
                if ((aPort.CMD & 0x8000) == 0)
                {
                    break;
                }

                Wait(5000);
            }

            if (i == 101)
            {
                return false;
            }

            for (i = 0; i < 101; i++)
            {
                if (aPort.CI == 0)
                {
                    break;
                }

                Wait(50);
            }

            if (i == 101)
            {
                return false;
            }

            aPort.CMD &= 4294967279u;
            if (SupportsCommandListOverride && (aPort.TFD & 0x80u) != 0)
            {
                aPort.CMD |= 8u;
            }

            for (i = 0; i < 101; i++)
            {
                if ((aPort.CMD & 0x8000) == 0 && (aPort.CMD & 0x4000) == 0 && (aPort.CMD & 1) == 0 && (aPort.CMD & 0x10) == 0)
                {
                    break;
                }

                Wait(5000);
            }

            if (i == 101)
            {
                if (SupportsCommandListOverride)
                {
                    aPort.CMD |= 8u;
                }
                else
                {
                    aPort.CMD &= 4294967287u;
                }

                return false;
            }

            return true;
        }

        private static HBACommandHeader[] GetCommandHeader(PortRegisters aPort)
        {
            HBACommandHeader[] array = new HBACommandHeader[32];
            for (uint num = 0u; num < array.Length; num++)
            {
                array[num] = new HBACommandHeader(aPort.CLB, num)
                {
                    PRDTL = 8,
                    CTBA = 4235264 + 8192 * aPort.mPortNumber + 256 * num,
                    CTBAU = 0u
                };
                new MemoryBlock(array[num].CTBA, 256u).Fill(0);
            }

            return array;
        }

    }
}
