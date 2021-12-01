using System.Text.RegularExpressions;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 在忽略大小的情况下，是否包含指定字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string? str, string? s)
        {
            if (s == null) return true;
            if (str.IsNullOrWhiteSpace() && s.IsNullOrWhiteSpace()) return true;
            if (str.IsNullOrWhiteSpace()) return false;
            return str.Contains(s, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 在忽略大小的情况下，是否以指定字符串结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool EndsWithIgnoreCase(this string? str, string? s)
        {
            if (str.IsNullOrWhiteSpace() && s.IsNullOrWhiteSpace()) return true;
            if (str.IsNullOrWhiteSpace()) return false;
            if (s.IsNullOrWhiteSpace()) return true;
            return str.EndsWith(s, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 在忽略大小的情况下，比较两个字符串是否相等
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string? str, string? s)
        {
            if (str == null) return s == null;
            if (str.IsNullOrWhiteSpace() && s.IsNullOrWhiteSpace()) return true;
            return str.Equals(s, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 是否有值，与IsNullOrWhiteSpace相对
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HasValue(this string? str)
        {
            return !str.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string? s)
        {
            if (s == null) return false;
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// 当为空白字符串的时候，返回指定的默认值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string NullWhiteSpaceForDefault(this string? s, string defaultValue = "")
        {
            if (s == null || s.IsNullOrWhiteSpace())
                return defaultValue;
            return s;
        }

        /// <summary>
        /// 用指定字符串，替换正则表达式所匹配的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static string? ReplaceWith(this string? value, string regexPattern, string replaceValue)
        {
            return ReplaceWith(value, regexPattern, replaceValue, RegexOptions.None);
        }

        /// <summary>
        /// 用指定字符串，替换正则表达式所匹配的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="replaceValue"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string? ReplaceWith(this string? value, string regexPattern, string replaceValue, RegexOptions options)
        {
            return Regex.Replace(value, regexPattern, replaceValue, options);
        }

        /// <summary>
        /// 用指定字符串，替换正则表达式所匹配的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static string? ReplaceWith(this string? value, string regexPattern, MatchEvaluator evaluator)
        {
            return ReplaceWith(value, regexPattern, RegexOptions.None, evaluator);
        }

        /// <summary>
        /// 用指定字符串，替换正则表达式所匹配的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="options"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        public static string? ReplaceWith(this string? value, string regexPattern, RegexOptions options, MatchEvaluator evaluator)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return value;
            }
            return Regex.Replace(value, regexPattern, evaluator, options);
        }

        /// <summary>
        /// 用指定字符串，替换正则表达式所匹配的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="replaceString"></param>
        /// <param name="isCaseInsensetive"></param>
        /// <returns></returns>
        public static string? ReplaceWith(this string? value, string regexPattern, string replaceString, bool isCaseInsensetive)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return value;
            }
            return Regex.Replace(value, regexPattern, replaceString, isCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? Reverse(this string? value)
        {
            if (value.IsNullOrWhiteSpace()) return value;
            var chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 用指定的正则表达式分割字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] Split(this string? value, string regexPattern, RegexOptions options)
        {
            if (value.IsNullOrWhiteSpace())
            {
                return Array.Empty<string>();
            }
            return Regex.Split(value, regexPattern, options);
        }

        /// <summary>
        /// 用指定的正则表达式分割字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static string[] Split(this string? value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 分割字符串，并排除空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string[] Split(this string? value, params string[] splitstr)
        {
            if (value.IsNullOrWhiteSpace()) return Array.Empty<string>();
            return value.Split(splitstr, StringSplitOptions.None);
        }

        /// <summary>
        /// 分割字符串，并排除空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string[] SplitWithoutEmpty(this string? value, params string[] splitstr)
        {
            if (value.IsNullOrWhiteSpace()) return Array.Empty<string>();
            return value.Split(splitstr, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 分割字符串，支持中英文逗号，并删除空白
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> Split逗号(this string? value)
        {
            return value.SplitWithoutEmpty(",", "，").ToList();
        }
        /// <summary>
        /// 分割字符串，支持中英文分号，并删除空白
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> Split分号(this string? value)
        {
            return value.SplitWithoutEmpty("；", ";").ToList();
        }
        /// <summary>
        /// 分割字符串，支持中英文逗号/分号，并删除空白
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> Split逗号分号(this string? value)
        {
            return value.SplitWithoutEmpty(",", "，", "；", ";").ToList();
        }

        /// <summary>
        /// 分割字符串，并排除空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static string[] SplitWithoutEmpty(this string? value, params char[] splitchar)
        {
            if (value.IsNullOrWhiteSpace()) return Array.Empty<string>();
            return value.Split(splitchar, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 在忽略大小的情况下，是否以指定字符串开头
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool StartsWithIgnoreCase(this string? str, string s)
        {
            if (str.IsNullOrWhiteSpace() && s.IsNullOrWhiteSpace()) return true;
            if (str.IsNullOrWhiteSpace()) return false;
            if (s.IsNullOrWhiteSpace()) return true;
            return str.StartsWith(s, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 扩展，不报错的substring
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string? SubStringExt(this string? str, int start, int length)
        {
            if (str.IsNullOrWhiteSpace()) return str;

            if (start < 0) start = 0;

            if (length + start > str.Length) return str.Substring(start);

            return str.Substring(start, length) + "...";
        }

        /// <summary>
        /// 去除指定字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string? Trim(this string? str, params string[] s)
        {
            if (str.IsNullOrWhiteSpace()) return str;
            return str.TrimStart(s).TrimEnd(s);
        }

        /// <summary>
        /// 去除指定字符串后缀
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string? TrimEnd(this string? str, params string[] s)
        {
            if (str.IsNullOrWhiteSpace())
                return str;
            if (s == null) return str;
            foreach (var item in s)
            {
                while (str.EndsWith(item)) str = str.Substring(0, str.Length - item.Length);

                break;
            }

            return str;
        }

        /// <summary>
        /// 去除指定字符串前缀
        /// </summary>
        /// <param name="str"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string? TrimStart(this string? str, params string[] s)
        {
            if (str.IsNullOrWhiteSpace())
                return str;
            if (s != null)
                foreach (var item in s)
                    if (str.StartsWith(item))
                    {
                        str = str.Substring(item.Length);
                        break;
                    }

            return str;
        }
    }
}