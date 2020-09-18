using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jw.Share
{
    public static class LongExtension
    {
        /// <summary>
        /// 将long形式的Ticks转换为DateTime, <see cref="DateTimeExtension.ToLong">long的计算起点需为1970-01-01 00:00:00.000</see>
        /// </summary>
        /// <param name="input">要转换的值</param>
        /// <returns>转换后的时间日期</returns>
        public static DateTime ToDateTime(this long input)
        {
            if (input == 0)
            {
                return DateTime.MinValue;
            }

            DateTime dt = new DateTime(input * TimeSpan.TicksPerMillisecond + 621355968000000000);
            dt = dt.AddHours(8);
            return dt;
        }
    }
}
