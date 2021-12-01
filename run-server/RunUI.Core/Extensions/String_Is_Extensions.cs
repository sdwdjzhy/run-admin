using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;  

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_Is_Extensions
    {
        //public static T CreateType<T>(this string typeName, params object[] args)
        //{
        //    Type type = Type.GetType(typeName, true, true);
        //    return (T)Activator.CreateInstance(type, args);
        //}
        ///// <summary>
        /////
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static T ToEnum<T>(this string value)
        //{
        //    return ToEnum<T>(value, false);
        //}
        ///// <summary>
        /////
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <param name="ignorecase"></param>
        ///// <returns></returns>
        //public static T ToEnum<T>(this string value, bool ignorecase)
        //{
        //    if (value == null) throw new ArgumentException("Value");
        //    value = value.Trim();
        //    if (value.Length == 0) throw new ArgumentException("Must specify valid information for parsing in the string.", "value");
        //    Type t = typeof(T);
        //    if (!t.IsEnum) throw new ArgumentException("Type provided must be an Enum.", "T");
        //    return (T)Enum.Parse(t, value, ignorecase);
        //}
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <param name="strchar">指定字符</param>
        public static string DelLastChar(this string str, char strchar)
        {
            if (str.LastIndexOf(strchar) < 0) return str;
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <param name="strchar">指定字符串</param>
        public static string DelLastChar(this string str, string strchar)
        {
            return str.LastIndexOf(strchar, StringComparison.Ordinal) < 0 ? str : str.Substring(0, str.LastIndexOf(strchar, StringComparison.Ordinal));
        }

        /// <summary>
        /// 两者之间的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static MatchCollection FindBetween(this string s, string startString, string endString)
        {
            return s.FindBetween(startString, endString, true);
        }

        /// <summary>
        /// 两者之间的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static MatchCollection FindBetween(this string s, string startString, string endString, bool recursive)
        {
            startString = Regex.Escape(startString);
            endString = Regex.Escape(endString);
            var regex = new Regex("(?<=" + startString + ").*(?=" + endString + ")");
            var matches = regex.Matches(s);
            if (!recursive) return matches;
            if (matches.Count <= 0) return matches;
            if (matches[0].ToString().IndexOf(Regex.Unescape(startString), StringComparison.Ordinal) <= -1) return matches;
            s = matches[0] + Regex.Unescape(endString);
            return s.FindBetween(Regex.Unescape(startString), Regex.Unescape(endString));
        }

        /// <summary>
        /// 两者之间的字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static List<string> FindBetweenValue(this string text, string strStart, string strEnd)
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(text))
                return list;
            var regex = @"" + strStart + "(?<content>.+?)" + strEnd + "";
            var rgClass = new Regex(regex, RegexOptions.Singleline);
            var matches = rgClass.Matches(text);
            foreach (Match item in matches) list.Add(item.Groups["content"].Value);
            return list;
        }

        /// <summary>
        /// 格式化Json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatJsonString(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return "";
            try
            {
                //格式化json字符串
                var serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                var jtr = new JsonTextReader(tr);
                var obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    var textWriter = new StringWriter();
                    var jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Newtonsoft.Json.Formatting.Indented,
                        Indentation = 1,
                        IndentChar = '\t'
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }

                return str;
            }
            catch
            {
                return str;
            }
        }
         
        /// <summary>
        /// 返回Email的域名
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static string GetEmailHostName(this string strEmail)
        {
            return strEmail.IndexOf("@", StringComparison.Ordinal) < 0 ? "" : strEmail.Substring(strEmail.LastIndexOf("@", StringComparison.Ordinal)).ToLower();
        }

        /// <summary>
        /// 获取Html里的第一个Img的Src
        /// </summary>
        /// <param name="html">html源代码</param>
        /// <returns></returns>
        public static string GetFirstImgUrl(this string html)
        {
            var regImg = @"<img([^>]*)>";
            var srcUrl = string.Empty;
            var reg = new Regex(regImg, RegexOptions.IgnoreCase);
            var mMatch = reg.Match(html);
            if (mMatch.Success)
            {
                var doc = new XmlDocument();
                try
                {
                    doc.LoadXml(mMatch.Value.Replace("\'", "\"")); //主要是这一块,如果<Img写得不规范,比如说少了引号,会出错
                    foreach (XmlAttribute a in doc.FirstChild.Attributes)
                        if (a.Name.ToLower() == "src")
                        {
                            srcUrl = a.Value;
                            break;
                        }
                }
                catch
                {
                    srcUrl = string.Empty;
                }
            }

            return srcUrl;
        }

        /// <summary>
        /// 使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static MatchCollection GetMatches(this string value, string regexPattern)
        {
            return GetMatches(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection GetMatches(this string value, string regexPattern, RegexOptions options)
        {
            return Regex.Matches(value, regexPattern, options);
        }

        /// <summary>
        /// 使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern)
        {
            return GetMatchingValues(value, regexPattern, RegexOptions.None);
        }

        /// <summary>
        /// 使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetMatchingValues(this string value, string regexPattern, RegexOptions options)
        {
            foreach (Match match in GetMatches(value, regexPattern, options))
                if (match.Success)
                    yield return match.Value;
        }

        /// <summary>
        /// 使用指定的匹配选项在指定的输入字符串中搜索指定的正则表达式的所有匹配项。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regexPattern"></param>
        /// <param name="rep1"></param>
        /// <param name="rep2"></param>
        /// <returns></returns>
        public static IList<string> GetMatchingValues(this string value, string regexPattern, string rep1, string rep2)
        {
            IList<string> txtTextArr = new List<string>();
            foreach (Match m in Regex.Matches(value, regexPattern))
            {
                var matchVale = m.Value.Trim().Replace(rep1, "").Replace(rep2, "");
                txtTextArr.Add(matchVale);
            }

            return txtTextArr;
        }

        /// <summary>
        /// 获取Html的Title
        /// </summary>
        /// <param name="html">html源代码</param>
        /// <returns></returns>
        public static string GetTitleUrl(this string html)
        {
            var regImg = @"(<title>)([^>]*)>";
            var srcUrl = string.Empty;
            var reg = new Regex(regImg, RegexOptions.IgnoreCase);
            var mMatch = reg.Match(html);
            if (mMatch.Success)
            {
                var doc = new XmlDocument();
                try
                {
                    doc.LoadXml(mMatch.Value.Replace("\'", "\"")); //主要是这一块,如果<Img写得不规范,比如说少了引号,会出错
                    srcUrl = doc.FirstChild.InnerXml;
                }
                catch
                {
                    srcUrl = string.Empty;
                }
            }

            return srcUrl;
        }

        /// <summary>
        /// 是否[^a-zA-Z0-9]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string input)
        {
            return input.IsMatch(@"[^a-zA-Z0-9]");
        }

        /// <summary>
        /// 是否是base64加密的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64StringReg(this string str)
        {
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBool(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return bool.TryParse(str, out _);
        }

        /// <summary>
        /// 判断字符串是否包含汉字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsChinese(this string str)
        {
            return IsMatch(str, @"[\u4e00-\u9fa5]+");
        }

        /// <summary>
        /// 是否是Date
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static bool IsDate(this string dateStr)
        {
            return DateTime.TryParse(dateStr, out _);
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return DateTime.TryParse(str, out _);
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return decimal.TryParse(str, out _);
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDouble(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return double.TryParse(str, out var i) && !double.IsNaN(i) && !double.IsInfinity(i);
        }

        /// <summary>
        /// 是否是Email
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(this string input)
        {
            return input.IsMatch(AppConst.验证Email地址);
        }

        /// <summary>
        /// n位的数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsFixLengthNumber(this string str, int n)
        {
            return IsMatch(str, @"^\d{" + n + "}$");
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return float.TryParse(str, out _);
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            try
            {
                var i = new Guid(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是真实的身份证号
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static bool IsIdCard(this string cid)
        {
            const string reg = @"^\d{18}$|^\d{17}[xX]$";

            if (Regex.IsMatch(cid, reg) == false) return false;

            var aCity = new[]
            {
                null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null,
                null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南",
                "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null,
                "香港", "澳门", null, null, null, null, null, null, null, null, "国外"
            };
            double iSum = 0;

            cid = cid.ToLower();
            cid = cid.Replace("x", "a");
            if (aCity[int.Parse(cid.Substring(0, 2))] == null) return false;
            try
            {
                var dt = DateTime.Parse(cid.Substring(6, 4) + "-" + cid.Substring(10, 2) + "-" + cid.Substring(12, 2));
                if (dt.Year < 1800) return false;
                if (dt > DateTime.Now) return false;
            }
            catch
            {
                return false;
            }

            for (var i = 17; i >= 0; i--) iSum += Math.Pow(2, i) % 11 * int.Parse(cid[17 - i].ToString(), NumberStyles.HexNumber);
            return iSum % 11 == 1;
        }

        /// <summary>
        /// 是否是图片格式的文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsImgFileName(this string filename)
        {
            if (filename.IsNullOrWhiteSpace()) return false;
            var ext = Path.GetExtension(filename);
            var exts = new[] { ".jpg", ".jpeg", ".gif", ".bmp", ".ico" };
            if (ext.IsNullOrWhiteSpace()) return false;
            if (exts.Contains(ext.ToLower())) return true;
            return false;
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return int.TryParse(str, out _);
        }

        /// <summary>
        /// 是否是IP地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsIP(this string input)
        {
            return input.IsMatch(AppConst.IpAddress); //@"^(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))(\.(([01]?[\d]{1,2})|(2[0-4][\d])|(25[0-5]))){3}$";
        }

        /// <summary>
        /// 至少n位的数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsLeastNumber(this string str, int n)
        {
            return IsMatch(str, @"^\d{" + n + ",}$");
        }

        /// <summary>
        /// m~n位的数字
        /// </summary>
        /// <param name="str"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsLengthNumber(this string str, int m, int n)
        {
            return IsMatch(str, @"^\d{" + m + "," + n + "}$");
        }

        /// <summary>
        /// 没有去除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLong(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return long.TryParse(str, out _);
        }

        /// <summary>
        /// 是否正则匹配上
        /// </summary>
        /// <param name="str"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public static bool IsMatch(this string str, string op)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            var re = new Regex(op, RegexOptions.IgnoreCase);
            return re.IsMatch(str);
        }

        /// <summary>
        /// 验证手机号是否正确
        /// </summary>
        /// <param name="oldMobile"></param>
        /// <returns></returns>
        public static bool IsMobile(this string oldMobile)
        {
            if (string.IsNullOrWhiteSpace(oldMobile)) return false;
            oldMobile = oldMobile.Trim();
            string newmobile;
            if (oldMobile.StartsWith("+86"))
                newmobile = oldMobile.Substring(3);
            else if (oldMobile.StartsWith("86"))
                newmobile = oldMobile.Substring(2);
            else
                newmobile = oldMobile;
            return Regex.IsMatch(newmobile, @"^[1]\d{10}$");
            //return System.Text.RegularExpressions.Regex.IsMatch(newmobile, @"^((\+)?86)?[1][34578]\d{9}$");
        }

        /// <summary>
        /// 输入数字，无符号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(this string str)
        {
            return IsMatch(str, @"^[0-9]+$");
        }

        /// <summary>
        /// 输入数字，有符号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumberNoSign(this string str)
        {
            return IsMatch(str, @"^[+-]?[0-9]+$");
        }

        /// <summary>
        /// 是否是安全的SQL语句
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSafeSqlString(this string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 是否是SSN
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsSSN(this string input)
        {
            var pet = @"\d{18}|\d{15}";
            return input.IsMatch(pet);
        }

        /// <summary>
        /// 校验一个字符串是否为固定电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTelephone(this string str)
        {
            var regex = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$", RegexOptions.IgnoreCase);
            return regex.Match(str).Success;
        }

        /// <summary>
        /// 是否是Time
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        public static bool IsTime(this string timeStr)
        {
            return timeStr.IsMatch(@"^([0-1]\\d|2[0-3]):[0-5]\\d:[0-5]\\d$"); //^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$
        }

        /// <summary>
        /// 是否是true
        /// <para>数值大于0，y、yes、是、t、true、ok</para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTrue(this string str)
        {
            if (str.IsNullOrWhiteSpace())
                return false;
            if (str.IsInt() && str.ToInt() > 0) return true;

            switch (str.Trim().ToLower())
            {
                case "y":
                case "yes":
                case "是":
                case "t":
                case "true":
                case "ok":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 是否是Url
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUrl(this string input)
        {
            //string pet = @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$";//@"^http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            return input.IsMatch(AppConst.验证InternetUrl);
        }

        /// <summary>
        /// 是否是压缩
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsZip(this string input)
        {
            return input.IsMatch(@"\d{6}");
        }

        /// <summary>
        /// 转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(this string input)
        {
            var c = input.ToCharArray();
            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }

                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }

            return new string(c);
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(this string input)
        {
            //半角转全角：
            var c = input.ToCharArray();
            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }

                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }

            return new string(c);
        }
    }
}