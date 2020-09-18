using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace vPowerManager.Share
{
    public class MemoryInfo
    {
        private ulong _Total = 0;
        /// <summary>
        /// 总内存(M)
        /// </summary>
        public ulong Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        private ulong _Free = 0;

        /// <summary>
        /// 可用内存(M)
        /// </summary>
        public ulong Free
        {
            get { return _Free; }
            set { _Free = value; }
        }
        private ulong _Used = 0;

        /// <summary>
        /// 使用内存(M)
        /// </summary>
        public ulong Used
        {
            get { return _Used; }
            set { _Used = value; }
        }
        private int _Usage = 0;
        /// <summary>
        /// 内存使用百分比
        /// </summary>
        public int Usage
        {
            get { return _Usage; }
            set { _Usage = value; }
        }

        public static MemoryInfo QueryInfo()
        {
            ManagementScope namespaceScope = new ManagementScope("\\\\.\\ROOT\\CIMV2");
            ObjectQuery memQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectSearcher memSearcher = new ManagementObjectSearcher(namespaceScope, memQuery);
            ManagementObjectCollection colmem = memSearcher.Get();
            ulong totalm = 1; ulong avilablem = 1;
            foreach (ManagementObject mo in colmem)
            {
                string total = mo.GetPropertyValue("TotalPhysicalMemory").ToString();
                totalm = ulong.Parse(total) / (ulong)1024 / (ulong)1024;
            }

            ObjectQuery aviQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher aviSearcher = new ManagementObjectSearcher(namespaceScope, aviQuery);
            ManagementObjectCollection colavi = aviSearcher.Get();
            foreach (ManagementObject mo in colavi)
            {
                //Number, in kilobytes, of physical memory currently unused and available
                string avilable = mo.GetPropertyValue("FreePhysicalMemory").ToString();
                avilablem = ulong.Parse(avilable) / (ulong)1024;
            }
            ulong usedm = totalm - avilablem;
            double memoryusage = (double)usedm * (double)100 / (double)totalm;

            return new MemoryInfo() { Total = totalm, Free = avilablem, Used = usedm, Usage = Convert.ToInt32(memoryusage) };
        }
    }
}
