using System.Globalization;
using System.Text;

namespace RunUI
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class String_Encoding_Extensions
    {
        /// <summary>
        /// 64解密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToBase64Decode(this string s)
        {
            return ToBase64Decode(s, Encoding.UTF8);
        }

        /// <summary>
        /// 64解密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">解密编码</param>
        /// <returns></returns>
        public static string ToBase64Decode(this string s, Encoding encoding)
        {
            return encoding.GetString(Convert.FromBase64String(s));
        }

        /// <summary>
        /// 64加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToBase64Encode(this string s)
        {
            return ToBase64Encode(s, Encoding.UTF8);
        }

        /// <summary>
        /// 64加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding">加密编码</param>
        /// <returns></returns>
        public static string ToBase64Encode(this string s, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(s));
        }

        /// <summary>
        /// 将显示为16进制的字符串转换成二进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexByte(this string hexString)
        {
            hexString = hexString.Replace(" ", "").Replace("%", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++) returnBytes[i] = byte.Parse(hexString.Substring(i * 2, 2), NumberStyles.HexNumber);
            return returnBytes;
        }

        /// <summary>
        /// 将显示为16进制的字符串转换成实际含义的字符串
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string ToHexString(this string hexString)
        {
            return ToHexString(hexString, Encoding.UTF8);
        }

        /// <summary>
        /// 将显示为16进制的字符串转换成实际含义的字符串
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToHexString(this string hexString, Encoding e)
        {
            return e.GetString(hexString.ToHexByte());
        }
    }
}