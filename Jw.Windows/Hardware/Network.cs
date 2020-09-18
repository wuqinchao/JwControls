using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;

namespace vPowerManager.Share
{
    public class Network
    {
        public static string Mac()
        {
            if (NetworkUp())
            {
                ManagementClass class2 = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject obj2 in class2.GetInstances())
                {
                    if (obj2["IPEnabled"].ToString() == "True")
                    {
                        string[] ips = (string[])obj2["IPAddress"];
                        string IP = Is_IPv4(ips[0]) ? ips[0] : ips[1];
                        if (!string.IsNullOrEmpty(IP) && !IP.StartsWith("169.254") && IP != "127.0.0.1")
                        {
                            return obj2["MacAddress"].ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static bool NetworkUp()
        {
            try
            {
                NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface n in adapters)
                {
                    if (n.OperationalStatus != OperationalStatus.Up)
                        continue;
                    UnicastIPAddressInformationCollection IPconllection = n.GetIPProperties().UnicastAddresses;
                    foreach (UnicastIPAddressInformation ipinfo in IPconllection)
                    {

                        if (IPAddress.IsLoopback(ipinfo.Address))                       //环回地址
                            continue;
                        if (ipinfo.Address.ToString() == IPAddress.IPv6None.ToString()) //IPV6下的无效地址
                            continue;
                        if (ipinfo.Address.ToString() == IPAddress.None.ToString())     //IPV4下的无效地址
                            continue;
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }
        public static bool Is_IPv4(string input)
        {
            return !input.Contains(':');
        }
    }
}
