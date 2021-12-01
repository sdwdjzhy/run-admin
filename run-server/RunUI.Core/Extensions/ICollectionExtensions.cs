using System.Collections.Generic;

namespace RunUI
{
    /// <summary>
    /// ICollection扩展类
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Join
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">数组</param>
        /// <param name="str">加入的字符串</param>
        /// <returns></returns>
        public static string Join<T>(this ICollection<T> list, string str)
        {
            return string.Join(str, list);
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">数组</param>
        /// <param name="str">加入的字符</param>
        /// <returns></returns>
        public static string Join<T>(this ICollection<T> list, char str)
        {
            return string.Join(str.ToString(), list);
        }
    }
}