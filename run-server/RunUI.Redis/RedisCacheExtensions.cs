namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class RedisCacheExtensions
    {
        /// <summary>
        /// 清楚Redis缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void ClearRedisCacheValue(this string key)
        {
            RedisCache.ClearCache(key);
        }

        /// <summary>
        /// 清楚Redis缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task ClearRedisCacheValueAsync(this string key)
        {
            await RedisCache.ClearCacheAsync(key);
        }

        /// <summary>
        /// 清楚Redis缓存
        /// </summary>
        /// <param name="key">通配符语法，xxx*,xx:xx*</param>
        /// <returns></returns>
        public static void ClearRedisCacheValueWithPattern(this string key)
        {
            RedisCache.ClearCacheWithPattern(key);
        }

        /// <summary>
        /// 清楚Redis缓存
        /// </summary>
        /// <param name="key">通配符语法，xxx*,xx:xx*</param>
        /// <returns></returns>
        public static async Task ClearRedisCacheValueWithPatternAsync(this string key)
        {
            await RedisCache.ClearCacheWithPatternAsync(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetRedisCacheValue<T>(this string key)
        {
            return RedisCache.GetCache<T>(key);
        }

        /// <summary>
        /// Redis缓存，滑动过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration">秒</param>
        /// <returns></returns>
        public static T RedisCacheValue<T>(this string key, Func<string, T> func, int? expiration = null)
        {
            return RedisCache.CacheValue(key, func, expiration);
        }

        /// <summary>
        /// Redis缓存，带绝对过期时间，只能精确到秒
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expirationTime">绝对过期时间</param>
        /// <returns></returns>
        public static T RedisCacheValue<T>(this string key, Func<string, T> func, DateTime expirationTime)
        {
            return RedisCache.CacheValue(key, func, (int)(expirationTime - DateTime.Now).TotalSeconds);
        }

        /// <summary>
        /// Redis缓存，滑动过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration">秒</param>
        /// <returns></returns>
        public static async Task<T> RedisCacheValueAsync<T>(this string key, Func<string, Task<T>> func, int? expiration = null)
        {
            return await RedisCache.CacheValueAsync(key, func, expiration);
        }

        /// <summary>
        /// Redis缓存，带绝对过期时间，只能精确到秒
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expirationTime">秒</param>
        /// <returns></returns>
        public static async Task<T> RedisCacheValueAsync<T>(this string key, Func<string, Task<T>> func, DateTime expirationTime)
        {
            return await RedisCache.CacheValueAsync(key, func, (int)(expirationTime - DateTime.Now).TotalSeconds);
        }
    }
}