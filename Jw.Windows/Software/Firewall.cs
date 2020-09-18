using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace vPowerManager.Share
{
    public class Firewall
    {
        //private static ILog log = LogManager.GetLogger("firewall");

        private const string exe = "netsh";

        public static bool ExistRule(string name)
        {
            string arguments = string.Format("advfirewall firewall show rule name=\"{0}\"", name);
            return RunCmd(exe, arguments);
        }

        public static bool AddRule(string name, string program, bool is_in, bool allow, bool enable)
        {
            string arguments = string.Format("advfirewall firewall add rule name=\"{0}\" program=\"{1}\" dir={2} action={3} enable={4}",
                                              name,
                                              program,
                                              is_in ? "in" : "out",
                                              allow ? "allow" : "block",
                                              enable ? "yes" : "no");
            return RunCmd(exe, arguments);
        }

        public static bool DelRule(string name, string program)
        {
            string arguments = string.Format("advfirewall firewall delete rule name=\"{0}\" program=\"{1}\"", name, program);
            return RunCmd(exe, arguments);
        }

        public static bool EnableRule(string name)
        {
            string arguments = string.Format("advfirewall firewall set rule name=\"{0}\" new enable=yes", name);
            return RunCmd(exe, arguments);
        }

        public static bool DisableRule(string name)
        {
            string arguments = string.Format("advfirewall firewall set rule name=\"{0}\" new enable=no", name);
            return RunCmd(exe, arguments);
        }

        public static bool Open()
        {
            string arguments = string.Format("advfirewall set currentprofile state {0}", "on");
            return RunCmd(exe, arguments);
        }

        public static bool Close()
        {
            string arguments = string.Format("advfirewall set currentprofile state {0}", "off");
            return RunCmd(exe, arguments);
        }

        private static bool RunCmd(string exe, string arguments)
        {
            Process p = Process.Start(exe, arguments);
            p.WaitForExit();
            //log.DebugFormat("RUN {0} {1} return {2}", exe, arguments, p.ExitCode);
            return p.ExitCode == 0;
        }
    }
}
