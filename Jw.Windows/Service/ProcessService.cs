using Jw.Share;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Jw.Windows
{
    public delegate void ProcessExited(object send, TResult<int> result);
    public class ProcessService
    {
        private const string Error_Args = "jw.process.arguments";
        private const string Error_Fail = "jw.process.failed";
        private const string Error_Run = "jw.process.already.run";
        private string _FileName;
        private string _Arguments;
        private bool _AdminRequired = false;
        private bool _WaitForExit = true;

        private Process _Process;
        private bool _Running = false;

        private int _ExitCode;

        public event ProcessExited OnProcessExited;
        /// <summary>
        /// 程序名称
        /// </summary>
        public string FileName { get => _FileName; set => _FileName = value; }
        /// <summary>
        /// 启动参数
        /// </summary>
        public string Arguments { get => _Arguments; set => _Arguments = value; }
        /// <summary>
        /// 是否需要管理员权限
        /// </summary>
        public bool   AdminRequired { get => _AdminRequired; set => _AdminRequired = value; }
        /// <summary>
        /// 是否需要进程执行结果
        /// </summary>
        public bool   WaitForExit { get => _WaitForExit; set => _WaitForExit = value; }
        /// <summary>
        /// 进程退出代码(WaitForExit为false时此值无效)
        /// </summary>
        public int    ExitCode { get => _ExitCode; set => _ExitCode = value; }
        /// <summary>
        /// 进程是否已启动
        /// </summary>
        public bool   Running { get => _Running; }
        /// <summary>
        /// 同步锁
        /// </summary>
        private object _Locker = new object();
        /// <summary>
        /// 构造进程
        /// </summary>
        /// <returns>是否构造成功</returns>
        private bool MakeProcess()
        {
            if (FileName.IsNotEmpty()) return false;
            _Process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = FileName,
                    Arguments = Arguments,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                },
            };
            if (AdminRequired && !OS.IsAdmin())
            {
                _Process.StartInfo.Verb = "runas";
            }
            return true;
        }
        /// <summary>
        /// 进程退出后清理资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProcessExited(object sender, EventArgs e)
        {
            _Process.Exited -= ProcessExited;
            Close();
        }

        /// <summary>
        /// 异步调用
        /// </summary>
        /// <returns>返回值表示是否启动进程,进程执行结果在OnProcessExited中通知</returns>
        public Result RunAsync()
        {
            lock (_Locker)
            {
                if (Running) return new Result() { Code = Error_Run, Message = "进程已启动" }; 
                _Process.Exited += ProcessExited;
                Task proc = new Task(new Action(()=> {
                    var result = RunSync();
                    FireExitEvent(result.Code, result.Message, result.Tag);
                }));
                proc.Start();
                return new Result(ResultCode.SuccCode, string.Empty);
            }
        }
        /// <summary>
        /// 同步调用
        /// </summary>
        /// <returns>进程执行结果</returns>
        public TResult<int> RunSync()
        {
            lock (_Locker)
            {
                if (!MakeProcess()) return new TResult<int>() { Code = Error_Args, Message = "参数错误", Tag = ExitCode  };
                _Running = true;
                _Process.Start();

                if (WaitForExit)
                {
                    _Process.WaitForExit();
                    ExitCode = _Process.ExitCode;
                }
                else
                {
                    return new TResult<int>() { Code = ResultCode.SuccCode, Message = "", Tag = ExitCode };
                }

                Close();

                if (ExitCode == 0)
                {
                    return new TResult<int>() { Code = ResultCode.SuccCode, Message = "", Tag = ExitCode };
                }
                else
                {
                    return new TResult<int>() { Code = Error_Fail, Message = $"{ExitCode}", Tag = ExitCode };
                }
            }
        }

        private void FireExitEvent(string code, string message, int exit)
        {
            OnProcessExited?.Invoke(this, new TResult<int>() { Code = code, Message = message, Tag = exit });
        }

        private void Close()
        {
            _Running = false;
            _Process.Close();
            _Process = null;
        }
    }
}
