echo off
C:\Users\defaultAdministrator\AppData\Local\NASM\nasm.exe -f bin E:\Dokumenty\PATCHOS\VS\PatchOSInstaller\Installer\Installer\boot_sector.asm
copy "C:\Users\defaultAdministrator\AppData\Roaming\Cosmos User Kit\Build\VMware\Workstation\FS new.vmdk" "C:\Users\defaultAdministrator\AppData\Roaming\Cosmos User Kit\Build\VMware\Workstation\Filesystem.vmdk"
pause