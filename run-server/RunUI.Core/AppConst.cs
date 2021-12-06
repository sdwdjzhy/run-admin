using System.Text;

namespace RunUI
{
    /// <summary>
    /// 全局常量
    /// </summary>
    public static class AppConst
    {
        /// <summary>
        /// Csv的最大查询数量 默认值
        /// </summary>
        public const int CsvDefaultItemCount = 1000000;

        /// <summary>
        /// 0.00
        /// </summary>
        public const string DecimalFormat = "0.00";

        /// <summary>
        /// ¥0.00
        /// </summary>
        public const string DecimalFormatWithCurrency = "¥0.00";

        /// <summary>
        /// 搜索区域中的数字的使用说明
        /// </summary>
        public const string SearchAdvancedUsageDesc =
            "支持数字组合（例:10 或 10,20,30）<br> " +
            "支持数字区间（例:10~20, 10~, ~20）<br> " +
            "支持数学符号（例:>10, <10, >=10, <=10）";

        #region Encoding

        /// <summary>
        /// UTF8
        /// </summary>
        public static Encoding UTF8 = Encoding.UTF8;

        #endregion Encoding

        #region 正则表达式

        /// <summary>
        /// ^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$
        /// </summary>
        public const string IpAddress = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// ^[a-zA-Z_][0-9a-zA-Z_]+$
        /// </summary>
        public const string 不带汉字用户名的字符串 = @"^[a-zA-Z_][0-9a-zA-Z_]+$";

        /// <summary>
        /// ^[a-zA-Z_\u4e00-\u9fa5][0-9a-zA-Z_\u4e00-\u9fa5]+$
        /// </summary>
        public const string 带汉字用户名的字符串 = @"^[a-zA-Z_\u4e00-\u9fa5][0-9a-zA-Z_\u4e00-\u9fa5]+$";

        /// <summary>
        /// ^\-[1-9][0-9]*$
        /// </summary>
        public const string 非零的负整数 = @"^\-[1-9][0-9]*$";

        /// <summary>
        /// ^\+?[1-9][0-9]*$
        /// </summary>
        public const string 非零的正整数 = @"^\+?[1-9][0-9]*$";

        /// <summary>
        /// ^[a-zA-Z_][0-9a-zA-Z_]{5-12}$
        /// </summary>
        public const string 密码的字符串 = @"^[a-zA-Z_][0-9a-zA-Z_]{5-12}$";

        /// <summary>
        /// [^\x00-\xff]
        /// </summary>
        public const string 匹配双字节字符包括汉字在内 = @"[^\x00-\xff]";

        /// <summary>
        /// ^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$
        /// </summary>
        public const string 验证Email地址 = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        /// <summary>
        /// ^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;
        /// %\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+ &amp;%\$#\=~_\-]+))*$
        /// </summary>
        public const string 验证InternetUrl =
            @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$";

        /// <summary>
        /// ^[A-Z]+$
        /// </summary>
        public const string 由26个大写英文字母组成的字符串 = @"^[A-Z]+$";

        /// <summary>
        /// ^[a-z]+$
        /// </summary>
        public const string 由26个小写英文字母组成的字符串 = @"^[a-z]+$";

        /// <summary>
        /// ^[A-Za-z]+$
        /// </summary>
        public const string 由26个英文字母组成的字符串 = @"^[A-Za-z]+$";

        /// <summary>
        /// ^[A-Za-z0-9]+$
        /// </summary>
        public const string 由数字和26个英文字母组成的字符串 = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// ^[0-9a-zA-Z_]+$
        /// </summary>
        public const string 由数字或26个英文字母或者下划线组成的字符串 = @"^[0-9a-zA-Z_]+$";

        //@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        //@"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";

        #endregion 正则表达式

        #region DateTime

        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// MM月dd日
        /// </summary>
        public const string DateFormatShortZh = "MM月dd日";

        /// <summary>
        /// yyyy年MM月dd日
        /// </summary>
        public const string DateFormatZh = "yyyy年MM月dd日";

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// yyyy-MM-dd HH:mm:ss.fffff
        /// </summary>
        public const string DateTimeFormatLong = "yyyy-MM-dd HH:mm:ss.fffff";

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss
        /// </summary>
        public const string DateTimeUrlFormat = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// yyyy-MM-ddTHH:mm:ss.fffff
        /// </summary>
        public const string DateTimeUrlFormatLong = "yyyy-MM-ddTHH:mm:ss.fffff";

        /// <summary>
        /// HH:mm:ss
        /// </summary>
        public const string TimeFormat = "HH:mm:ss";

        #endregion DateTime
    }
}