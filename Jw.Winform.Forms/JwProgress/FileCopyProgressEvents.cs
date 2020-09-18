namespace Jw.Winform.Forms
{
    public class FileCopyProgressArgs : ProgressArgs
    {
        public string Src { get; set; }
        public string Dst { get; set; }
        public double Speed { get; set; }

        public FileCopyProgressArgs()
        {
            Name = "复制文件";
        }
    }
    public class FileCopyProgressCompleteArgs : ProgressCompleteArgs
    {
        public string Src { get; set; }
        public string Dst { get; set; }
    }
}
