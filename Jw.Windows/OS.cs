using Jw.Share;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Jw.Windows
{
    public class OS
    {
        public static System.OperatingSystem osInfo = System.Environment.OSVersion;

        public static int Major
        {
            get { return osInfo.Version.Major; }
        }

        public static int Minor
        {
            get { return osInfo.Version.Minor; }
        }

        public static bool IsXP
        {
            get { return osInfo.Version.Major < 6; }
        }
        /// <summary>
        /// 判断运行时是否为64位
        /// </summary>
        public static bool Is64BitProcess
        {
            get { return Environment.Is64BitProcess; }
        }
        public static bool Is64BitOs
        {
            get { return Environment.Is64BitOperatingSystem; }
        }
        public static Result RunSC(string Arguments)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "sc";
                p.StartInfo.Arguments = Arguments;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.CreateNoWindow = true;
                //p.StartInfo.RedirectStandardOutput = true;  UseShellExecute = true 时不能重定义流
                if (!IsAdmin())
                {
                    p.StartInfo.Verb = "runas";
                }
                p.Start();

                p.WaitForExit();
                int code = p.ExitCode;
                p.Close();

                if (code == 0)
                {
                    return new Result() { Code = "000", Message = "" };
                }
                else
                {
                    return new Result() { Code = code.ToString(), Message = $"操作失败({code})" };
                }
            }
        }
        /// <summary>
        /// 当前程序是否有ADMIN权限
        /// </summary>
        /// <returns></returns>
        public static bool IsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public static void Sleep()
        {
            Process.Start("shutdown.exe", "/h");
        }

        public static void Shutdown(int sec=10)
        {
            Process.Start("shutdown.exe", "/s /t " + sec.ToString() + " /c \"远程管理员要求关机(" + sec.ToString() + "秒后)\"");
        }

        public static void Reboot(int sec=10)
        {
            Process.Start("shutdown.exe", "/r /t " + sec.ToString() + " /c \"远程管理员要求重启(" + sec.ToString() + "秒后)\"");
        }

        /*
        Operating System	                   Version Number
        -----------------------------------------------------
        Windows 1.0	                           1.04
        Windows 2.0	                           2.11
        Windows 3.0	                           3
        Windows NT 3.1	                       3.10.528
        Windows for Workgroups 3.11	           3.11
        Windows NT Workstation 3.5	           3.5.807
        Windows NT Workstation 3.51	           3.51.1057
        Windows 95	                           4.0.950
        Windows NT Workstation 4.0	           4.0.1381
        Windows 98	                           4.1.1998
        Windows 98 Second Edition	           4.1.2222
        Windows Me	                           4.90.3000
        Windows 2000 Professional	           5.0.2195
        Windows XP	                           5.1.2600
        Windows XP Professional x64 Edition    5.2.3790
        Windows Server 2003	                   5.2
        Windows Server 2003 R2	               5.2
        Windows Vista	                       6.0.6000
        Windows Server 2008	                   6.0
        Windows Server 2008 R2	               6.1
        Windows 7	                           6.1.7600
        Windows 8	                           6.2.9200
        Windows 8.1                            6.3
        Windows 10                             6.4
        Windows 10                             10.0
        */
    }
}
