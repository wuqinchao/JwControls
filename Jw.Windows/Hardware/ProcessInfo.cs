//using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace vPowerManager.Share
{
    public class ProcessInfo
    {
        //private static ILog log = LogManager.GetLogger("ProcessInfo");
        public ProcessInfo(int ProcessID, string ProcessName, double ProcessorTime,
                               long WorkingSet, string ProcessPath)
        {
            this.ProcessID = ProcessID;
            this.ProcessName = ProcessName;
            this.ProcessorTime = ProcessorTime;
            this.WorkingSet = WorkingSet;
            this.ProcessPath = ProcessPath;
        }

        private int _ProcessID;
        public int ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }

        private string _ProcessName;
        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }

        private double _ProcessorTime;
        public double ProcessorTime
        {
            get { return _ProcessorTime; }
            set { _ProcessorTime = value; }
        }

        private long _WorkingSet;
        public long WorkingSet
        {
            get { return _WorkingSet; }
            set { _WorkingSet = value; }
        }

        private string _ProcessPath;
        public string ProcessPath
        {
            get { return _ProcessPath; }
            set { _ProcessPath = value; }
        }

        public static List<ProcessInfo> Query()
        {
            List<ProcessInfo> pInfo = new List<ProcessInfo>();
            Process[] processes = Process.GetProcesses();
            foreach (Process instance in processes)
            {
                try
                {
                    pInfo.Add(new ProcessInfo(instance.Id,
                        instance.ProcessName,
                        instance.TotalProcessorTime.TotalMilliseconds,
                        instance.WorkingSet64,
                        instance.MainModule.FileName));
                }
                catch { }
            }
            return pInfo;
        }

        public static bool Kill(int pid)
        {
            Process p = null;
            try
            {
                p = Process.GetProcessById(pid);
            }
            catch (SystemException exp)
            {
                //log.ErrorFormat("Kill Processs(GetProcessById {0}): {1}", pid, exp.ToString());
                return false;
            }

            if (p == null)
            {
                return false;
            }
            try
            {
                p.Kill();
            }
            catch (SystemException exp)
            {
                //log.ErrorFormat("Kill Processs(Kill {0}): {1}", pid, exp.ToString());
                return false;
            }

            return true;
        }
    }
}
