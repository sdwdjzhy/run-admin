using System.Text;

namespace RunUI
{
    /// <summary>
    /// ByteExtensions
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// 将数组根据编码，转换成字符串
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToString(this byte[] bt, Encoding encoding)
        {
            return encoding.GetString(bt);
        }
    }
}