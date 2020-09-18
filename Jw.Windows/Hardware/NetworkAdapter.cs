using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace vPowerManager.Share
{
    public class NetworkAdapter
    {
        private string _ConnectionID;

        public string ConnectionID
        {
            get { return _ConnectionID; }
            set { _ConnectionID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _MACAddress;

        public string MACAddress
        {
            get { return _MACAddress; }
            set { _MACAddress = value; }
        }
        private string _AdapterType;

        public string AdapterType
        {
            get { return _AdapterType; }
            set { _AdapterType = value; }
        }

        private UInt64 _Speed = 0;
        /// <summary>
        /// Units ("bits per second")
        /// </summary>
        public UInt64 Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        private ulong _Speed_Down;
        /// <summary>
        /// Rate at which bytes are received on the interface, including framing characters.
        /// </summary>
        public ulong Speed_Down
        {
            get { return _Speed_Down; }
            set { _Speed_Down = value; }
        }
        private ulong _Speed_Up;
        /// <summary>
        /// Rate at which bytes are sent on the interface, including framing characters.
        /// </summary>
        public ulong Speed_Up
        {
            get { return _Speed_Up; }
            set { _Speed_Up = value; }
        }
        private double _Percent;

        public double Percent
        {
            get { return _Percent; }
            set { _Percent = value; }
        }
        private ulong _Bandwidth;
        /// <summary>
        /// Estimate of the interface's current bandwidth in bits per second (bps). For interfaces that do not vary in bandwidth or for those where no accurate estimation can be made, this value is the nominal bandwidth
        /// </summary>
        public ulong Bandwidth
        {
            get { return _Bandwidth; }
            set { _Bandwidth = value; }
        }

        public override string ToString()
        {
            return string.Format("NAME:{0} SPEED:{1}M BANDWIDTH:{2}M PERCENT:{3}% DOWN:{4} UP:{5}", ConnectionID, Speed / 1000 / 1000, Bandwidth / 1000 / 1000, Percent, Speed_Down, Speed_Up);
        }

        public static List<NetworkAdapter> QueryInfo()
        {
            List<string> avilbeNet = new List<string>();
            List<NetworkAdapter> list = new List<NetworkAdapter>();
            ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
            //ObjectQuery diskQuery = new ObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DriveType = " + HARD_DISK + "");
            ObjectQuery diskQuery = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter WHERE MACAddress<>null and Manufacturer<>'Microsoft' and NetEnabled=True");
            ManagementObjectSearcher mgmtObjSearcher = new ManagementObjectSearcher(namespaceScope, diskQuery);
            ManagementObjectCollection colDisks = mgmtObjSearcher.Get();
            foreach (ManagementObject obj in colDisks)
            {
                NetworkAdapter net = new NetworkAdapter();
                net.Name = obj["Name"] == null ? "" : obj["Name"].ToString();
                net.ConnectionID = obj["NetConnectionID"] == null ? net.Name : obj["NetConnectionID"].ToString();
                net.AdapterType = obj["AdapterType"] == null ? "" : obj["AdapterType"].ToString();
                net.MACAddress = obj["MACAddress"] == null ? "" : obj["MACAddress"].ToString();
                if (obj["Speed"] != null)
                { 
                    net.Speed = UInt64.Parse(obj["Speed"].ToString());
                }
                avilbeNet.Add(net.Name);
                list.Add(net);
            }
            ObjectQuery speedQuery = new ObjectQuery("SELECT * FROM Win32_PerfFormattedData_Tcpip_NetworkInterface");
            ManagementObjectSearcher speedSearcher = new ManagementObjectSearcher(namespaceScope, speedQuery);
            ManagementObjectCollection colspeed = speedSearcher.Get();
            foreach (ManagementObject obj in colspeed)
            {
                string name = obj["Name"].ToString();
                foreach (NetworkAdapter net in list)
                {
                    if (RemoveAny(net.Name).Equals(RemoveAny(name)))
                    {
                        decimal down = decimal.Parse(obj["BytesReceivedPerSec"].ToString());
                        decimal up = decimal.Parse(obj["BytesSentPerSec"].ToString());
                        decimal width = decimal.Parse(obj["CurrentBandwidth"].ToString());
                        decimal percent = (down * (decimal)800) / (width);

                
                        net.Speed_Down = decimal.ToUInt64(down);
                        net.Speed_Up = decimal.ToUInt64(up);
                        net.Bandwidth = decimal.ToUInt64(width);
                        net.Percent = decimal.ToDouble(percent);
                    }
                }
                
            }

            return list;
        }

        public static string RemoveAny(string input)
        {
            string[] remove = new string[] { "(",")","[","]","_","-","@","#","%","^","&","*",":","\"","'","\\","/","!","|","<", ">",".",","};
            foreach (string item in remove)
            {
                input = input.Replace(item, "");
            }
            return input;
        }
    }
}
