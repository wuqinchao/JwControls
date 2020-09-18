using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace vPowerManager.Share
{
    public class ProcessorInfo
    {
        //public static readonly int ProcessorCount = Environment.ProcessorCount;
        private string _DeviceID;

        public string DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        private string _Name;
       
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private UInt16 _Architecture;
        /// <summary>
        /// x86 (0), MIPS (1), Alpha (2), PowerPC (3), ARM (5), ia64 (6), x64 (9)
        /// </summary>
        public UInt16 Architecture
        {
            get { return _Architecture; }
            set { _Architecture = value; }
        }

        private UInt32 _NumberOfLogicalProcessors = 0;

        public UInt32 NumberOfLogicalProcessors
        {
            get { return _NumberOfLogicalProcessors; }
            set { _NumberOfLogicalProcessors = value; }
        }

        private UInt16 _LoadPercentage = 0;
        /// <summary>
        /// 使用率(0-100)
        /// </summary>
        public UInt16 LoadPercentage
        {
            get { return _LoadPercentage; }
            set { _LoadPercentage = value; }
        }

        public static List<ProcessorInfo> QueryInfo()
        {
            List<ProcessorInfo> ps = new List<ProcessorInfo>();
            ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
            ObjectQuery memQuery = new ObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher memSearcher = new ManagementObjectSearcher(namespaceScope, memQuery);
            ManagementObjectCollection colmem = memSearcher.Get();
            foreach (ManagementObject mo in colmem)
            {
                string dev = mo.GetPropertyValue("DeviceID").ToString();
                string name = mo.GetPropertyValue("Name").ToString();
                object load = mo.GetPropertyValue("LoadPercentage");
                UInt32 logicalNumber = UInt32.Parse(mo.GetPropertyValue("NumberOfLogicalProcessors").ToString());
                UInt16 percent = load == null ? (UInt16)0 : UInt16.Parse(load.ToString());
                UInt16 architecture = UInt16.Parse(mo.GetPropertyValue("Architecture").ToString());
                ps.Add(new ProcessorInfo() { DeviceID = dev, Name = name, Architecture = architecture, LoadPercentage = percent, NumberOfLogicalProcessors = logicalNumber });
            }

            return ps;
        }

        private static PerformanceCounter pcCpuLoad = null;
        static ProcessorInfo()
        {
            pcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pcCpuLoad.MachineName = ".";
            pcCpuLoad.NextValue(); 

        }
        public static int Query()
        {
            //UInt16 Count = 0;
            //UInt16 Percentage = 0;
            //ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
            //ObjectQuery memQuery = new ObjectQuery("SELECT * FROM Win32_Processor");
            //ManagementObjectSearcher memSearcher = new ManagementObjectSearcher(namespaceScope, memQuery);
            //ManagementObjectCollection colmem = memSearcher.Get();
            //foreach (ManagementObject mo in colmem)
            //{
            //    Count++;
            //    object value = mo.GetPropertyValue("LoadPercentage");
            //    if (value != null)
            //    {
            //        Percentage += UInt16.Parse(value.ToString());
            //    }
            //}
            //Percentage = Convert.ToUInt16(Percentage / Count);
            //return Percentage;

            return Convert.ToInt32(pcCpuLoad.NextValue());
        }
    }
}
