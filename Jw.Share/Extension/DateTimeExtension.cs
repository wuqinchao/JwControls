using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jw.Windows
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 将DateTime转换为Ticks的Long形式
        /// </summary>
        /// <remarks>
        /// 兼容java.util.Calendar.getTimeInMillis
        /// 转换后计时起点为1970-01-01 00:00:00.000
        /// </remarks>
        /// <param name="input">准备转换的时间</param>
        /// <returns>转换结果</returns>
        public static long ToLong(this DateTime input)
        {
            DateTime global = input.AddHours(-8);
            long val = Decimal.ToInt64(Decimal.Divide(global.Ticks - 621355968000000000,TimeSpan.TicksPerMillisecond));
            return val;
        }
        /// <summary>
        /// 将DateTime转换为Ticks的Long形式,时间为当天的最开始时间
        /// </summary>
        /// <remarks>
        /// 兼容java.util.Calendar.getTimeInMillis
        /// 转换后计时起点为1970-01-01 00:00:00.000
        /// </remarks>
        /// <param name="input">准备转换的时间</param>
        /// <returns>转换结果</returns>
        public static long ToDayStartLong(this DateTime input)
        {
            DateTime global = new DateTime(input.Year, input.Month, input.Day).AddHours(-8);
            long val = Decimal.ToInt64(Decimal.Divide(global.Ticks - 621355968000000000, TimeSpan.TicksPerMillisecond));
            return val;
        }
        /// <summary>
        /// 将DateTime转换为Ticks的Long形式,时间为当天的最结束时间
        /// </summary>
        /// <remarks>
        /// 兼容java.util.Calendar.getTimeInMillis
        /// 转换后计时起点为1970-01-01 00:00:00.000
        /// </remarks>
        /// <param name="input">准备转换的时间</param>
        /// <returns>转换结果</returns>
        public static long ToDayEndLong(this DateTime input)
        {
            DateTime global = new DateTime(input.Year, input.Month, input.Day).AddDays(1).AddTicks(-1).AddHours(-8);
            long val = Decimal.ToInt64(Decimal.Divide(global.Ticks - 621355968000000000, TimeSpan.TicksPerMillisecond));
            return val;
        }
    }
}
