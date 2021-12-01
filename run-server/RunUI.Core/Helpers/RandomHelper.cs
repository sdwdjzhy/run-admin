using System;
using System.Text;

namespace RunUI
{
    /// <summary>
    /// 随机数辅助类
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random rnd = new Random();

        /// <summary>
        /// 生成六位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetRandomString()
        {
            return GetRandomString(6);
        }

        /// <summary>
        /// 生成n位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetRandomString(int n, bool containsletter = false)
        {
            if (n <= 0) throw new Exception("无效长度");
            var code = containsletter ? "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ" : "0123456789";

            var ch = code.ToCharArray();
            var sb = new StringBuilder();
            for (var i = 0; i < n; i++) sb.Append(ch[rnd.Next(code.Length)]);
            return sb.ToString();
        }

        /// <summary>
        /// 图片验证码里的随机字符,去除易错的字符，无小写
        /// </summary>
        /// <returns></returns>
        public static string GetValidCode(int n = 5, bool containsletter = false)
        {
            if (n <= 0) throw new Exception("无效长度");
            var code = containsletter ? "3456789ABCDEFGHJKMPQRSTWXY" : "0123456789";

            var ch = code.ToCharArray();
            var sb = new StringBuilder();
            for (var i = 0; i < n; i++) sb.Append(ch[rnd.Next(code.Length)]);
            return sb.ToString();
        }

        /// <summary>
        /// 返回非负随机数
        /// </summary>
        /// <returns></returns>
        public static int Next()
        {
            return rnd.Next();
        }

        /// <summary>
        /// 返回一个小于所指定的最大值的非负随机数
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int max)
        {
            return rnd.Next(max);
        }

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            return rnd.Next(min, max);
        }

        /// <summary>
        /// 生成设置范围内的Double的随机数 eg:_random.NextDouble(1.5, 2.5)
        /// </summary>
        /// <param name="miniDouble">生成随机数的最大值</param>
        /// <param name="maxiDouble">生成随机数的最小值</param>
        /// <returns>当Random等于NULL的时候返回0;</returns>
        public static decimal NextDecimal(decimal miniDouble, decimal maxiDouble)
        {
            if (rnd != null)
                return rnd.NextDouble().ToDecimal() * (maxiDouble - miniDouble) + miniDouble;
            return 0.0m;
        }

        /// <summary>
        /// 生成Double的随机数
        /// </summary>
        /// <returns>当Random等于NULL的时候返回0;</returns>
        public static double NextDouble()
        {
            return rnd.NextDouble();
        }

        /// <summary>
        /// 生成设置范围内的Double的随机数 eg:_random.NextDouble(1.5, 2.5)
        /// </summary>
        /// <param name="miniDouble">生成随机数的最大值</param>
        /// <param name="maxiDouble">生成随机数的最小值</param>
        /// <returns>当Random等于NULL的时候返回0;</returns>
        public static double NextDouble(double miniDouble, double maxiDouble)
        {
            if (rnd != null)
                return rnd.NextDouble() * (maxiDouble - miniDouble) + miniDouble;
            return 0.0d;
        }
    }
}