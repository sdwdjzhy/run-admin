using System.Globalization;

namespace RunUI
{
    /// <summary>
    /// ///
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 自动返回数值，如果存在小数位，就强制保留两位小数，如果没有小数位，就返回整形
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToAutoMoney(this decimal d)
        {
            return $"{d.ToMoney():0.##}";
        }

        /// <summary>
        /// 默认保留两位小数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToDefaultDecimalFormat(this decimal d)
        {
            return $"{d:F}";
        }

        /// <summary>
        /// 强制保留2位小数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal ToMoney(this decimal d)
        {
            return d.ToSubDecimalFormat().ToDecimal();
        }

        /// <summary>
        /// 四舍五入保留两位小数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToRoundDecimalFormat(this decimal d)
        {
            return Math.Round(d, 2).ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 截取强制保留两位小数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToSubDecimalFormat(this decimal d)
        {
            var n = (long)(d * 100);
            d = n / 100m;
            return $"{d:0.00}";
        }
    }
}