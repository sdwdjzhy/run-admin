using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    /// <summary>
    /// 序列号
    /// </summary>
    public static class OrderIdHelper
    {
        public static Func<string>? GetOrderIdCustom;
        private readonly static ObjectIdFactory factory = new ObjectIdFactory();

        /// <summary>
        /// 28位
        /// </summary>
        /// <returns></returns>
        public static string NewId()
        {
            var str = GetOrderIdCustom?.Invoke();
            if (str != null && str.IsNullOrWhiteSpace()) return str;

            var now = DateTime.UtcNow;

            var msecsArray = BitConverter.GetBytes((long)(now.TimeOfDay.TotalSeconds / 3.333333));
            msecsArray = msecsArray.Take(2).ToArray();
            Array.Reverse(msecsArray);

            var randomBytes = new byte[8];
            RandomNumberGenerator.Fill(randomBytes);
            return now.ToString("yyyyMMdd") + msecsArray.Select(b => b.ToString("x2")).Join("") + randomBytes.Select(b => b.ToString("x2")).Join("");
        }
        /// <summary>
        /// 32位
        /// </summary>
        /// <returns></returns>
        public static string GuidString()
        {
            return SequentialGuid.GuidString();
        }
        /// <summary>
        /// 24位
        /// </summary>
        /// <returns></returns>
        public static string ObjecId()
        {
            return factory.NewId();
        }
    }
}
