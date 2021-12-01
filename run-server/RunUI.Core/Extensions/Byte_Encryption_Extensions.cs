using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace RunUI
{
    public static class Byte_Encryption_Extensions
    {
        private static byte[] GetShaBytes(this byte[] buffer, HashAlgorithm sha)
        {
            var tmpByte = sha.ComputeHash(buffer);
            sha.Clear();
            return tmpByte;
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetMd5(this byte[] buffer)
        {
            using var md5 = MD5.Create();
            var t = md5.ComputeHash(buffer);
            return t.Select(i => i.ToString("x2")).Join("");
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetSha1(this byte[] buffer)
        {
            return buffer.GetSha1(Encoding.UTF8);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetSha1(this byte[] buffer, Encoding encoding)
        {
            return buffer.GetSha1Bytes().ToString(encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] GetSha1Bytes(this byte[] buffer)
        {
            using var sha = SHA1.Create();
            return GetShaBytes(buffer, sha);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetSha256(this byte[] buffer)
        {
            return buffer.GetSha256(Encoding.UTF8);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetSha256(this byte[] buffer, Encoding encoding)
        {
            return buffer.GetSha256Bytes().ToString(encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] GetSha256Bytes(this byte[] buffer)
        {
            using var sha = SHA256.Create();
            return GetShaBytes(buffer, sha);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetSha512(this byte[] buffer)
        {
            return buffer.GetSha512(Encoding.UTF8);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetSha512(this byte[] buffer, Encoding encoding)
        {
            return buffer.GetSha512Bytes().ToString(encoding);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] GetSha512Bytes(this byte[] buffer)
        {
            using var sha = SHA512.Create();
            return GetShaBytes(buffer, sha);
        }
    }
}
