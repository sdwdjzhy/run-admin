using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream)
        {
            // 分开使用，是因为 使用字节时，会出现 UTF8-BOM 的多于字节的问题
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadToEnd(this Stream stream, Encoding encoding)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);
            return buffer.ToString(encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<string> ReadToEndAsync(this Stream stream)
        {            
            using (var sr = new StreamReader(stream))
            {
                return await sr.ReadToEndAsync();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> ReadToEndAsync(this Stream stream, Encoding encoding)
        {
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, (int)stream.Length);
            return buffer.ToString(encoding);
        }
    }
}