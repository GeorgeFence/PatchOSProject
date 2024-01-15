﻿using Cosmos.System.Network.Config;
using Cosmos.System.Network.IPv4;
using Cosmos.System.Network.IPv4.TCP;
using Cosmos.System.Network.IPv4.UDP.DHCP;
using Cosmos.System.Network.IPv4.UDP.DNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosHttp.Client;
using static System.Net.WebRequestMethods;

namespace PatchOS.Files.Drivers.Network
{
    public static class NetworkMgr
    {
        public static DHCPClient client = new DHCPClient();
        public static DnsClient dnsClient = new DnsClient();
        //public static Tcp tcpClient = new Tcp(80);

        public static void Initialize()
        {
            try
            {
                client.SendDiscoverPacket();
            }
            catch(Exception e)
            {
                SYS32.KernelPanic(e, "NetworkMgr");
            }
        }

        public static string GetIP()
        {
            return NetworkConfiguration.CurrentAddress.ToString();
        }

        public static string GetUpdatePackage()
        {
            string path = null;
            //Download(@"0:\update.pkg", "");
            return path;
        }
        public static string Wget(string domain)
        {
            try
            {
                HttpRequest request = new();
                request.IP = "34.223.124.45";
                request.Domain = "neverssl.com";
                request.Path = "/";
                request.Method = "GET";
                request.Send();
                GUIConsole.WriteLine(request.Response.Content);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

            return string.Empty;
        }

        public static void Ping(string URL)
        {
            int PacketSent = 0;
            int PacketReceived = 0;
            int PacketLost = 0;
            int PercentLoss;

            Address source;
            Address destination = Address.Parse(URL);

            if (destination != null)
            {
                source = IPConfig.FindNetwork(destination);
            }
            else //Make a DNS request if it's not an IP
            {
                var xClient = new DnsClient();
                xClient.Connect(DNSConfig.DNSNameservers[0]);
                xClient.SendAsk(URL);
                destination = xClient.Receive();
                xClient.Close();

                if (destination == null)
                {
                    
                }

                source = IPConfig.FindNetwork(destination);
            }

            try
            {
                GUIConsole.WriteLine("Sending ping to " + destination.ToString());

                var xClient = new ICMPClient();
                xClient.Connect(destination);

                for (int i = 0; i < 4; i++)
                {
                    xClient.SendEcho();

                    PacketSent++;

                    var endpoint = new EndPoint(Address.Zero, 0);

                    int second = xClient.Receive(ref endpoint, 4000);

                    if (second == -1)
                    {
                        GUIConsole.WriteLine("Destination host unreachable.");
                        PacketLost++;
                    }
                    else
                    {
                        if (second < 1)
                        {
                            GUIConsole.WriteLine("Reply received from " + endpoint.Address.ToString() + " time < 1s");
                        }
                        else if (second >= 1)
                        {
                            GUIConsole.WriteLine("Reply received from " + endpoint.Address.ToString() + " time " + second + "s");
                        }

                        PacketReceived++;
                    }
                }

                xClient.Close();
            }
            catch
            {
                
            }

            PercentLoss = 25 * PacketLost;

            GUIConsole.WriteLine("");
            GUIConsole.WriteLine("Ping statistics for " + destination.ToString() + ":");
            GUIConsole.WriteLine("    Packets: Sent = " + PacketSent + ", Received = " + PacketReceived + ", Lost = " + PacketLost + " (" + PercentLoss + "% loss)");

        }
    }
}