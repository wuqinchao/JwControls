using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jw.Share
{
    public class EnumConvert<T>
    {
        /// <summary>
        /// 根据字符串返回指定枚举类型相匹配的枚举值
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T getValue(string text)
        {
            return (T)Enum.Parse(typeof(T), text);
        }
    }
}
