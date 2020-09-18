using System;

namespace Jw.Share
{
    public class ResultCode
    {
        public const string SuccCode = "succ";
    }
    public class TResult<T> : Result
    {
        public T Tag { get; set; }

        public TResult() { }

        public TResult(string code, string message) : base(code, message)
        {
        }
        public TResult(string code, string message, T tag) : base(code, message)
        {
            this.Tag = tag;
        }
    }
    public class Result
    {
        public String Code { get; set; }
        public String Message { get; set; }
        public Result()
        {
        }
        public Result(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }
        public bool IsSucc()
        {
            return Code.Equals(ResultCode.SuccCode, StringComparison.Ordinal);
        }
    }
}
