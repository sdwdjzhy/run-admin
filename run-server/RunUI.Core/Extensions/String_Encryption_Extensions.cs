using System.Text;
using System.Text.RegularExpressions;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_Encryption_Extensions
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <returns></returns>
        public static string ToMd5(this string s)
        {
            return s.ToMd5(Encoding.UTF8);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s">要加密的字符串</param>
        /// <param name="encoding">要对字符串进行的编码格式</param>
        /// <returns></returns>
        public static string ToMd5(this string s, Encoding encoding)
        {
            return encoding.GetBytes(s).GetMd5();
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string ToSha1(this string strIn)
        {
            return strIn.ToSha1(Encoding.UTF8);
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToSha1(this string strIn, Encoding encoding)
        {
            return strIn.ToBytes(encoding).GetSha1(encoding);
        }

        /// <summary>
        /// sha256加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string ToSha256(this string strIn)
        {
            return strIn.ToSha256(Encoding.UTF8);
        }

        /// <summary>
        /// sha256加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToSha256(this string strIn, Encoding encoding)
        {
            return strIn.ToBytes(encoding).GetSha256(encoding);
        }

        /// <summary>
        /// sha512加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static string ToSha512(this string strIn)
        {
            return strIn.ToSha512(Encoding.UTF8);
        }

        /// <summary>
        /// sha512加密
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToSha512(this string strIn, Encoding encoding)
        {
            return strIn.ToBytes(encoding).GetSha512(encoding);
        }

        /// <summary>
        /// 转换成Unicode编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnicodeString(this string str)
        {
            var strResult = new StringBuilder();
            if (string.IsNullOrWhiteSpace(str)) return strResult.ToString();
            foreach (var t in str)
            {
                strResult.Append("\\u");
                strResult.Append(((int)t).ToString("x"));
            }

            return strResult.ToString();
        }

        /// <summary>
        /// 转换输入字符串中的任何转义字符。
        /// </summary>
        /// <param name="str">包含任何转换为非转义形式的转义字符的字符串。</param>
        /// <returns></returns>
        public static string Unescape(this string str)
        {
            return Regex.Unescape(str);
            //最直接的方法Regex.Unescape(str);
            //StringBuilder strResult = new StringBuilder();
            //if (!string.IsNullOrWhiteSpace(str))
            //{
            //    string[] strlist = str.Replace("\\", "").Split('u');
            //    try
            //    {
            //        for (int i = 1; i < strlist.Length; i++)
            //        {
            //            int charCode = Convert.ToInt32(strlist[i], 16);
            //            strResult.Append((char)charCode);
            //        }
            //    }
            //    catch
            //    {
            //        return Regex.Unescape(str);
            //    }
            //}
            //return strResult.ToString();
        }
    }
}