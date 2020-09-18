using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Forms
{
    public delegate void ProgressChangeEvent(object sender, ProgressArgs args);
    public delegate void ProgressCompleteEvent(object sender, ProgressCompleteArgs args);
    public enum ProgressCompleteType
    {
        None = 0,
        Finish,
        Cancel,
        Error,
    }
    public class ProgressArgs
    {
        public string Name { get; set; }
        public int Progress { get; set; }
        public string TaskName { get; set; }
        public string LeftInfo { get; set; }
        public string RightInfo { get; set; }
    }

    public class ProgressCompleteArgs
    {
        public ProgressCompleteType CompleteType { get; set; }
        public String ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public TimeSpan UsedTime { get; set; }
    }
}
