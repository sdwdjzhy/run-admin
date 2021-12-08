using System.Globalization;
using System.Text;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_Convert_Extensions
    {
        /// <summary>
        /// 将字符串转换成Boolean
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            return bool.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成bool型");
        }

        /// <summary>
        /// 将字符串转换成Bool，没有则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ToBoolOrDefault(this string str, bool b = default)
        {
            if (str.IsNullOrWhiteSpace()) return b;

            str = str.Trim().ToLower();
            if (str == "true" || str == "y" || str == "yes" || long.TryParse(str, out var n) && n > 0)
                return true;
            if (str == "false" || str == "n" || str == "no" || long.TryParse(str, out var n1) && n1 <= 0)
                return false;
            return b;
        }

        /// <summary>
        /// 将字符串转换成Bool?
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool? ToBoolOrNull(this string str)
        {
            return bool.TryParse(str, out var n) ? (bool?)n : null;
        }

        /// <summary>
        /// 将字符串转换成Byte
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ToByte(this string str)
        {
            return Convert.ToByte(str);
        }

        /// <summary>
        /// 将字符串，返回UTF8编码的Byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string s)
        {
            return ToBytes(s, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串，返回指定编码的Byte[]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s);
        }

        /// <summary>
        /// 将字符串转换成Char
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char ToChar(this string str)
        {
            return Convert.ToChar(str);
        }

        /// <summary>
        /// 将字符串转换成DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            return DateTime.Parse(str, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 根据格式，将字符串转换成DateTime
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 将字符串转换成DateTime?
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(this string str)
        {
            return DateTime.TryParse(str, out var dateTime) ? (DateTime?)dateTime : null;
        }

        /// <summary>
        /// 将字符串转换成Decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str)
        {
            return decimal.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成decimal型");
        }

        /// <summary>
        /// 将字符串转换成Decimal，没有则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal ToDecimalOrDefault(this string str, decimal num = default)
        {
            return str.ToDecimalOrNull() ?? num;
        }

        /// <summary>
        /// 将字符串转换成Decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(this string str)
        {
            return decimal.TryParse(str, out var n) ? (decimal?)n : null;
        }

        /// <summary>
        /// 将字符串转换成Double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            return double.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成double型");
        }

        /// <summary>
        /// 将字符串转换成Double，没有则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double ToDoubleOrDefault(this string str, double num = default)
        {
            return str.ToDoubleOrNull() ?? num;
        }

        /// <summary>
        /// 将字符串转换成Double?
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(this string str)
        {
            return double.TryParse(str, out var n) ? (double?)n : null;
        }

        /// <summary>
        /// 将字符串转换成Float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str)
        {
            return float.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成float型");
        }

        /// <summary>
        /// 将字符串转换成Float，没有则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static float ToFloatOrDefault(this string str, float num = default)
        {
            return str.ToFloatOrNull() ?? num;
        }

        /// <summary>
        /// 将字符串转换成Float
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float? ToFloatOrNull(this string str)
        {
            return float.TryParse(str, out var n) ? (float?)n : null;
        }

        /// <summary>
        /// 将字符串转换成Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            return Guid.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成Guid型");
        }

        /// <summary>
        /// 将字符串转换成Guid，没有则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Guid? ToGuidOrDefault(this string str, Guid g = default)
        {
            return str.ToGuidOrNull() ?? g;
        }

        /// <summary>
        /// 将字符串转换成Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid? ToGuidOrNull(this string str)
        {
            return Guid.TryParse(str, out var guid) ? (Guid?)guid : null;
        }

        /// <summary>
        /// 将字符串转换成Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return int.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成int型");
        }

        /// <summary>
        /// 将字符串转换成Int?
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ToIntOrDefault(this string str, int num = default)
        {
            return str.ToIntOrNull() ?? num;
        }

        /// <summary>
        /// 将字符串转换成Int?
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str)
        {
            return int.TryParse(str, out var n) ? (int?)n : null;
        }

        //public static Nullable<T> ToNullable<T>(this string s) where T : struct
        //{
        //    T result = null;
        //    if (!s.Trim().IsNullOrWhiteSpace())
        //    {
        //        TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
        //        result = (T)converter.ConvertFrom(s);
        //    }
        //    return result;
        //}

        /// <summary>
        /// 根据Environment.NewLine分割字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<string> ToLines(this string text)
        {
            return text.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// 根据指定字符分割字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="chars">分割的字符数组</param>
        /// <returns></returns>
        public static List<string> ToLines(this string text, params char[] chars)
        {
            return text.Split(chars, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// 根据指定字符串分割字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static List<string> ToLines(this string text, params string[] chars)
        {
            return text.Split(chars, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// 根据指定字符分割字符串，并剔除空白
        /// </summary>
        /// <param name="text"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static List<string> ToLinesEnough(this string text, params char[] chars)
        {
            return text.Split(chars, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// 根据指定字符串分割字符串，并剔除空白
        /// </summary>
        /// <param name="text"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static List<string> ToLinesEnough(this string text, params string[] chars)
        {
            return text.Split(chars, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// 将字符串转换成Long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            return long.TryParse(str, out var n) ? n : throw new ArgumentException($"不能将值：[{str}]转换成long型");
        }

        /// <summary>
        /// 字符串转换成Long型，没有就返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static long ToLongOrDefault(this string str, long num = default)
        {
            return str.ToLongOrNull() ?? num;
        }

        /// <summary>
        /// 将字符串转换成Long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long? ToLongOrNull(this string str)
        {
            return long.TryParse(str, out var n) ? n : null;
        }

        /// <summary>
        /// 根据字符串转换成DayOfWeek
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DayOfWeek ToWeekDay(this string str)
        {
            str = str?.Replace("星期", "")?.Replace("周", "");
            switch (str)
            {
                case "0":
                case "7":
                case "日":
                    return DayOfWeek.Sunday;

                case "1":
                case "一":
                    return DayOfWeek.Monday;

                case "2":
                case "二":
                    return DayOfWeek.Tuesday;

                case "3":
                case "三":
                    return DayOfWeek.Wednesday;

                case "4":
                case "四":
                    return DayOfWeek.Thursday;

                case "5":
                case "五":
                    return DayOfWeek.Friday;

                case "6":
                case "六":
                    return DayOfWeek.Saturday;

                default:
                    return DayOfWeek.Sunday;
            }
        }
    }
}