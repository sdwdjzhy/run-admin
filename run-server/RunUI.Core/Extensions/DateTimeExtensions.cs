using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 今天晚上23:59:59
        /// </summary>
        public static DateTime TodayEnd => DateTime.Now.EndOfDay();

        /// <summary>
        /// 今天凌晨0:0:0
        /// </summary>
        public static DateTime TodayStart => DateTime.Today;

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(this DateTime dateTime1, DateTime dateTime2)
        {
            var ts = dateTime2 - dateTime1;
            string dateDiff;
            if (ts.Days >= 1)
            {
                dateDiff = dateTime1.Month + "月" + dateTime1.Day + "日";
            }
            else
            {
                if (ts.Hours > 1)
                    dateDiff = ts.Hours + "小时前";
                else
                    dateDiff = ts.Minutes + "分钟前";
            }

            return dateDiff;
        }

        /// <summary>
        /// 一天的结束 23:59:59
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, dt.Kind);
        }

        /// <summary>
        /// 小时的结束
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfHour(this DateTime dt)
        {
            return dt.AddHours(1).StartOfHour().AddTicks(-1);
        }

        /// <summary>
        /// 分钟的结束
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfMinute(this DateTime dt)
        {
            return dt.AddMinutes(1).StartOfMinute().AddTicks(-1);
        }

        /// <summary>
        /// 一个月的结束
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return dt.AddMonths(1).StartOfMonth().AddDays(-1).EndOfDay();
        }

        /// <summary>
        /// 一个周的结束
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var end = dt.StartOfDay();
            var endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0) endDayOfWeek = DayOfWeek.Saturday;

            if (end.DayOfWeek == endDayOfWeek) return end;
            return endDayOfWeek < end.DayOfWeek ? end.AddDays(7 - (end.DayOfWeek - endDayOfWeek)) : end.AddDays(endDayOfWeek - end.DayOfWeek);
        }

        /// <summary>
        /// 一个年的结束
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31, 0, 0, 0, dt.Kind).EndOfDay();
        }

        /// <summary>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? GetDateTimeFromTimestamp(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return null;
            str = str.Trim();

            if (str.IsMatch("^\\d+$"))
            {
                if (str.Length < 15)
                {
                    return null;
                }

                var year = str[..4].ToInt();

                var dt = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                dt = dt.AddMilliseconds(str[4..].ToLong());
                return dt.ToLocalTime();
            }

            return null;
        }

        /// <summary>
        /// 是否是工作日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWeekDay(this DateTime dt)
        {
            return !dt.IsWeekend();
        }

        /// <summary>
        /// 是否是周六、周日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 一天的开始 0:0:0
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// 小时的开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfHour(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0, dt.Kind);
        }

        /// <summary>
        /// 分钟的开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfMinute(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Kind);
        }

        /// <summary>
        /// 一个月的开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, dt.Kind);
        }

        /// <summary>
        /// 一个周的开始
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var start = dt.StartOfDay();

            if (start.DayOfWeek == startDayOfWeek) return start;
            var d = startDayOfWeek - start.DayOfWeek;
            return startDayOfWeek <= start.DayOfWeek ? start.AddDays(d) : start.AddDays(-7 + d);
        }

        /// <summary>
        /// 一个年的开始
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1, 0, 0, 0, dt.Kind);
        }

        /// <summary>
        /// 时间戳，yyyy + 从1-1 00:00:00 开始的毫秒值
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string TimeStamp(this DateTime dateTime)
        {
            var dt = new DateTime(dateTime.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.Year.ToString() + (dateTime.ToLong() - dt.ToLong());
        }

        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime date)
        {
            return date.ToString(AppConst.DateFormat);
        }

        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateFormat() : "";
        }

        /// <summary>
        /// 根据从1970-01-01到现在的时间差，获取时间
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long l)
        {
            var dt = l.ToDateTimeUtc();
            return dt.ToLocalTime();
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this DateTime date)
        {
            return date.ToString(AppConst.DateTimeFormat);
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeFormat(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateTimeFormat() : "";
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.sss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeFormatLong(this DateTime date)
        {
            return date.ToString(AppConst.DateTimeFormatLong);
        }

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.sss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeFormatLong(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateTimeFormatLong() : "";
        }

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeUrlFormat(this DateTime date)
        {
            return date.ToString(AppConst.DateTimeUrlFormat);
        }

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeUrlFormat(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateTimeUrlFormat() : "";
        }

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss.sss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeUrlFormatLong(this DateTime date)
        {
            return date.ToString(AppConst.DateTimeUrlFormatLong);
        }

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss.sss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeUrlFormatLong(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateTimeUrlFormatLong() : "";
        }

        /// <summary>
        /// 根据从1970-01-01到现在的时间差，获取时间
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeUtc(this long l)
        {
            var dtStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dtStart.AddMilliseconds(l);
        }

        /// <summary>
        /// UTC 时间 格式 yyyy'-'MM'-'dd'T'HH':'mm':'ssK
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToIso8601Date(this DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        /// <summary>
        /// UTC 时间 格式 yyyy'-'MM'-'dd'T'HH':'mm':'ssK
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToIso8601Date(this DateTime? date)
        {
            return date == null ? "" : date.Value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        /// <summary>
        /// 根据时间，获取从1970-01-01到现在的时间差,毫秒
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToLong(this DateTime datetime)
        {
            return datetime.ToLongUtc();
        }

        /// <summary>
        /// 根据时间，获取从1970-01-01到现在的时间差,毫秒
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ToLongUtc(this DateTime datetime)
        {
            var dtStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            datetime = datetime.ToUniversalTime();
            return (long)(datetime - dtStart).TotalMilliseconds;
        }

        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToTimeFormat(this DateTime date)
        {
            return date.ToString(AppConst.TimeFormat);
        }

        /// <summary>
        /// HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToTimeFormat(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToTimeFormat() : "";
        }
    }
}
