using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;
//using log4net;
using Jw.Share;

namespace vPowerManager.Share
{
    public class ProcessWatcher : IDisposable
    {
        public static ProcessWatcher Instance = null;
        //private ILog log = LogManager.GetLogger("ProcessWatcher");
        private object Locker = new object();
        private ManagementEventWatcher startProcWatcher;
        private List<string> BlockList = new List<string>();
        private ProcessWatcher() { }

        static ProcessWatcher()
        {
            Instance = new ProcessWatcher();
        }
        public void Start()
        {
            if (startProcWatcher == null)
            {
                WatchForProcessStart();
                //log.Debug("ProcessWatcher running!");
            }
        }

        public void Stop()
        {
            if (startProcWatcher != null)
            {
                startProcWatcher.Stop();
                startProcWatcher = null;
                //log.Debug("ProcessWatcher stopped!");
            }
        }

        public void AddToBlock(string exe)
        {
            if (!exe.IsNullOrEmpty())
            {
                lock (Locker)
                {
                    string target = exe.ToLower();
                    if (!BlockList.Contains(target))
                    {
                        BlockList.Add(target);
                    }
                }
            }
        }

        public void RemoveBlock(string exe)
        {
            if (!exe.IsNullOrEmpty())
            {
                lock (Locker)
                {
                    string target = exe.ToLower();
                    if (BlockList.Contains(target))
                    {
                        BlockList.Remove(target);
                    }
                }
            }
        }

        public void AddToBlock(string[] exes)
        {
            if (BlockList.Count<1 && exes != null && exes.Length > 0)
            {
                foreach (string exe in exes)
                {
                    AddToBlock(exe);
                }
            }
        }

        public void RemoveBlock(string[] exes)
        {
            if (BlockList.Count > 1 && exes != null && exes.Length > 0)
            {
                foreach (string exe in exes)
                {
                    RemoveBlock(exe);
                }
            }
        }

        public void ClearFilter()
        {
            lock (Locker)
            {
                BlockList.Clear();
            }
        }

        private static string FMT_USB_NAME = "TargetInstance.Name='{0}'";

        private string JoinFilter()
        {
            string filter = " AND ({0})";
            StringBuilder cons = new StringBuilder();
            for (int n=0;n<BlockList.Count;n++)
            {
                if (n > 0) cons.Append(" OR ");
                cons.Append(string.Format(FMT_USB_NAME, BlockList[n]));
            }
            return string.Format(filter, cons.ToString());
        }

        public void WatchForProcessStart()
        {
            StringBuilder queryString = new StringBuilder();
            queryString.Append("SELECT TargetInstance " +
                               "FROM __InstanceCreationEvent " +
                               "WITHIN 1 " +
                               "WHERE TargetInstance ISA 'Win32_Process'");
            if (BlockList.Count > 0)
            {
                queryString.Append(JoinFilter());
            }

            // The dot in the scope means use the current machine
            string scope = @"\\.\root\CIMV2";
            // Create a watcher and listen for events
            startProcWatcher = new ManagementEventWatcher(scope, queryString.ToString());
            startProcWatcher.EventArrived += ProcessStarted;
            startProcWatcher.Start();
            //log.Debug("ProcessWatcher running...");
        }
        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            string processName = "";
            UInt32 processId = 0;
            try
            {
                processName = targetInstance.Properties["Name"].Value.ToString().ToLower();
                processId = (UInt32)targetInstance.Properties["ProcessId"].Value;
                //log.DebugFormat("{0} {1} running", processId, processName);
                if (BlockList.Contains(processName))
                {
                    Process p = Process.GetProcessById(Convert.ToInt32(processId));
                    if (p != null)
                    {
                        p.Kill();
                        //log.DebugFormat("KILL {0} {1}", processId, processName);
                    }
                    else
                    {
                        //log.DebugFormat("Cannot find {0} {1}", processId, processName);
                    }
                }
            }
            catch(SystemException exp)
            {
                //log.ErrorFormat("{0} {1}", processId, processName);
                //log.Error(exp.ToString());
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
