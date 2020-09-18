using Jw.Share.File;
using System;
using System.IO;

namespace Jw.Winform.Forms
{
    public class FileCopyService: IProgressService
    {
        public FileCopyService(string src, string dst, bool move)
        {
            this.Src = src;
            this.Dst = dst;
            this.IsMove = move;
        }

        public void Start()
        {
            bool WriteError = false;
            byte[] buffer = new byte[1024 * 1024 * 50]; // 10MB buffer
            DateTime dtStart = DateTime.Now;
            var completeArgs = new FileCopyProgressCompleteArgs()
            {
                 Src = this.Src,
                 Dst = this.Dst,
            };
            if(!Directory.Exists(Path.GetDirectoryName(Dst)))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Dst));
                }
                catch(Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "500";
                    completeArgs.ErrorMessage = $"创建目标文件夹失败\r\n{exp}";
                    goto finished;
                }
            }
            OnProgressChanged?.Invoke(this, new FileCopyProgressArgs() { Src = Src, Dst = this.Dst, Progress = 0, TaskName = Src });
            FileStream source = null;
            try
            {
                source = new FileStream(Src, FileMode.Open, FileAccess.Read);
            }
            catch(Exception exp)
            {
                completeArgs.CompleteType = ProgressCompleteType.Error;
                completeArgs.ErrorCode = "501";
                completeArgs.ErrorMessage = $"打开源文件失败\r\n{exp}";
                goto finished;
            }
            //using ()
            {
                long fileLength = source.Length;
                FileStream dest = null;
                long totalBytes = 0;
                int currentBlockSize = 0;
                try
                {
                    dest = new FileStream(Dst, FileMode.CreateNew, FileAccess.Write);
                }
                catch(Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "502";
                    completeArgs.ErrorMessage = $"创建目标文件失败\r\n{exp}";
                    source.Close();
                    goto finished;
                }

                while (!_Cancel && (currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    totalBytes += currentBlockSize;
                    var persentage = totalBytes * 100 / fileLength;

                    try
                    {
                        dest.Write(buffer, 0, currentBlockSize);
                    }
                    catch (Exception exp)
                    {
                        WriteError = true;
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "503";
                        completeArgs.ErrorMessage = $"复制文件失败\r\n{exp}";
                        break;
                    }

                    OnProgressChanged?.Invoke(this, new FileCopyProgressArgs() { 
                        Src = Src, 
                        Dst = this.Dst, 
                        Progress = (int)persentage, 
                        TaskName=Src,
                        LeftInfo = $"{persentage}%",
                        RightInfo = $"{FileSize.GetAutoSizeString(totalBytes)}/{FileSize.GetAutoSizeString(fileLength)}",
                    });
                }

                dest.Close();
            }

            source.Close();

            if (WriteError)
            {
                try
                {
                    File.Delete(Dst);
                }
                catch (Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "504";
                    completeArgs.ErrorMessage = $"复制文件失败\r\n{exp}";
                    goto finished;
                }
            }
            if (_Cancel)
            {
                completeArgs.CompleteType = ProgressCompleteType.Cancel;
                try
                {
                    File.Delete(Dst);
                }
                catch(Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "505";
                    completeArgs.ErrorMessage = $"删除目标文件失败\r\n{exp}";
                    goto finished;
                }
            }
            if (IsMove && !_Cancel && !WriteError)
            {
                try
                {
                    File.Delete(Src);
                }
                catch (Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "506";
                    completeArgs.ErrorMessage = $"删除源文件失败\r\n{exp}";
                    goto finished;
                }
            }
            if (!_Cancel && !WriteError)
            {
                completeArgs.CompleteType = ProgressCompleteType.Finish;
                completeArgs.ErrorCode = "000";
                completeArgs.ErrorMessage = "操作成功";
            }
finished:
            completeArgs.UsedTime = DateTime.Now - dtStart;
            OnComplete?.Invoke(this, completeArgs);
        }

        public string Src { get; set; }
        public string Dst { get; set; }
        public bool IsMove { get; set; }

        private bool _Cancel = false;

        public void Cancel()
        {
            _Cancel = true;
        }
        public bool CanCancel { get { return true; } }

        public event ProgressChangeEvent OnProgressChanged;
        public event ProgressCompleteEvent OnComplete;
    }   
}
