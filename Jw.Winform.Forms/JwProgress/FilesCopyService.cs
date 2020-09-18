using Jw.Share.File;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jw.Winform.Forms
{
    public class FilesCopyService : IProgressService
    {
        public FilesCopyService(string[] srcs, string dstFolder, bool move)
        {
            foreach (var src in srcs)
            {
                _Tasks.Add(new FileCopyTask() { src = src, dst = Path.Combine(dstFolder, Path.GetFileName(src)) });
            }
            this.IsMove = move;
        }

        public void Start()
        {
            bool WriteError = false;
            byte[] buffer = new byte[1024 * 1024 * 50]; // 10MB buffer
            DateTime dtStart = DateTime.Now;
            var completeArgs = new FileCopyProgressCompleteArgs();
            for (var i = 0; i < _Tasks.Count; i++)
            {
                var task = _Tasks[i];
                completeArgs.Src = task.src;
                completeArgs.Dst = task.dst;                
                if (!Directory.Exists(Path.GetDirectoryName(task.dst)))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(task.dst));
                    }
                    catch (Exception exp)
                    {
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "500";
                        completeArgs.ErrorMessage = $"创建目标文件夹失败\r\n{exp}";
                        goto finished;
                    }
                }
                OnProgressChanged?.Invoke(this, new FileCopyProgressArgs() { Src = task.src, Dst = task.dst, Progress = 0, TaskName = $"[{i+1}/{_Tasks.Count}]{Path.GetFileName(task.dst)}" });
                FileStream source = null;
                try
                {
                    source = new FileStream(task.src, FileMode.Open, FileAccess.Read);
                }
                catch (Exception exp)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Error;
                    completeArgs.ErrorCode = "501";
                    completeArgs.ErrorMessage = $"打开源文件{Path.GetFileName(task.src)}失败\r\n{exp}";
                    goto finished;
                }

                {
                    long fileLength = source.Length;
                    FileStream dest = null;
                    long totalBytes = 0;
                    int currentBlockSize = 0;
                    try
                    {
                        dest = new FileStream(task.dst, FileMode.CreateNew, FileAccess.Write);
                    }
                    catch (Exception exp)
                    {
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "502";
                        completeArgs.ErrorMessage = $"创建目标文件{Path.GetFileName(task.src)}失败\r\n{exp}";
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
                            completeArgs.ErrorMessage = $"写入文件{Path.GetFileName(task.dst)}失败\r\n{exp}";
                            break;
                        }

                        OnProgressChanged?.Invoke(this, new FileCopyProgressArgs()
                        {
                            Src = task.src,
                            Dst = task.dst,
                            Progress = (int)persentage,
                            TaskName = $"[{i + 1}/{_Tasks.Count}]{Path.GetFileName(task.dst)}",
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
                        File.Delete(task.dst);
                    }
                    catch (Exception exp)
                    {
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "504";
                        completeArgs.ErrorMessage = $"删除文件{Path.GetFileName(task.dst)}失败\r\n{exp}";
                        goto finished;
                    }
                }
                if (_Cancel)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Cancel;
                    try
                    {
                        File.Delete(task.dst);
                    }
                    catch (Exception exp)
                    {
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "505";
                        completeArgs.ErrorMessage = $"删除目标文件{Path.GetFileName(task.dst)}失败\r\n{exp}";
                        goto finished;
                    }
                }
                if (IsMove && !_Cancel && !WriteError)
                {
                    try
                    {
                        File.Delete(task.src);
                    }
                    catch (Exception exp)
                    {
                        completeArgs.CompleteType = ProgressCompleteType.Error;
                        completeArgs.ErrorCode = "506";
                        completeArgs.ErrorMessage = $"删除源文件{Path.GetFileName(task.src)}失败\r\n{exp}";
                        goto finished;
                    }
                }
                if (!_Cancel && !WriteError)
                {
                    completeArgs.CompleteType = ProgressCompleteType.Finish;
                    completeArgs.ErrorCode = "000";
                    completeArgs.ErrorMessage = "操作成功";
                }
            }
finished:
            completeArgs.UsedTime = DateTime.Now - dtStart;
            OnComplete?.Invoke(this, completeArgs);
        }

        private List<FileCopyTask> _Tasks = new List<FileCopyTask>();
        public bool IsMove { get; set; }

        private bool _Cancel = false;

        public void Cancel()
        {
            _Cancel = true;
        }
        public bool CanCancel { get { return true; } }

        public event ProgressChangeEvent OnProgressChanged;
        public event ProgressCompleteEvent OnComplete;

        public class FileCopyTask
        {
            public string src;
            public string dst;
        }
    }   
}
