using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{

    /// <summary>
    /// </summary>
    public static class SysCache
    {
        private static readonly object O = new object();

        private static MemoryCache CacheDefault
        {
            get
            {
                lock (O)
                {
                    cache ??= new MemoryCache(Options.Create(new MemoryCacheOptions()));
                }

                return cache;
            }
        }

        private static MemoryCache cache;

        private class CacheObject<T>
        {
            public T Result { get; set; }
        }

        /// <summary>
        /// 返回缓存对应键名的值（深拷贝），没有缓存时计算获取值并写入序列化写入缓存(取出时反序列化出来，缓存对象需要可json序列化与反序列化)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键名</param>
        /// <param name="func">取值函数 参数 string 为key</param>
        /// <param name="datetime">缓存过期时间</param>
        /// <returns></returns>
        public static T CacheClone<T>(this string key, Func<string, T> func, DateTime? datetime = null)
        {
            var objStr = GetCache(key);
            if (objStr == null)
                lock (LockHelper.GetLockObject("SysCache-" + key))
                {
                    objStr = GetCache(key);
                    if (objStr == null)
                    {
                        var obj = func(key);
                        var cacheObj = new CacheObject<T> { Result = obj };
                        var json = cacheObj.JsonSerialize();
                        SetCacheForNoSlidingExpiration(key, json, datetime);
                        objStr = json;
                    }
                }

            var s = objStr.ToStringExt()?.JsonDeserialize<CacheObject<T>>();
            return s == null ? default : s.Result;
        }

        /// <summary>
        /// 返回缓存对应键名的值（深拷贝），没有缓存时计算获取值并写入序列化写入缓存(取出时反序列化出来，缓存对象需要可json序列化与反序列化)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键名</param>
        /// <param name="func">取值函数 参数 string 为key</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns></returns>
        public static T CacheClone<T>(this string key, Func<string, T> func, TimeSpan timespan)
        {
            var objStr = GetCache(key);
            if (objStr == null)
                lock (LockHelper.GetLockObject("SysCache-" + key))
                {
                    objStr = GetCache(key);
                    if (objStr == null)
                    {
                        var obj = func(key);
                        var cacheObj = new CacheObject<T> { Result = obj };
                        var json = cacheObj.JsonSerialize();
                        SetCacheForNoAbsoluteExpiration(key, json, timespan);
                        objStr = json;
                    }
                }

            var s = objStr.ToStringExt()?.JsonDeserialize<CacheObject<T>>();
            return s == null ? default : s.Result;
        }

        /// <summary>
        /// 返回缓存对应键名的值，没有缓存时计算获取值并写入缓存
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键名</param>
        /// <param name="func">取值函数 参数 string 为key</param>
        /// <param name="datetime">缓存过期时间</param>
        /// <returns></returns>
        public static T CacheValue<T>(this string key, Func<string, T> func, DateTime? datetime = null)
        {
            //先去检测是否存在
            if (GetCache(key) is not CacheObject<T> cacheObj)
                //不存在
                //加锁，防止多次设置
                lock (LockHelper.GetLockObject("SysCache-" + key))
                {
                    //再次检测是否存在
                    if (GetCache(key) is not CacheObject<T> s)
                    {
                        //使用传入的函数，赋值
                        s = new CacheObject<T> { Result = func(key) };
                        SetCacheForNoSlidingExpiration(key, s, datetime);
                    }

                    if (s.Result == null) return default;
                    return s.Result;
                }

            if (cacheObj.Result == null) return default;
            return cacheObj.Result;
        }

        /// <summary>
        /// 返回缓存对应键名的值，没有缓存时计算获取值并写入缓存。这个是滑动过期
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">缓存键名</param>
        /// <param name="func">取值函数 参数 string 为key</param>
        /// <param name="timespan">缓存过期时间</param>
        /// <returns></returns>
        public static T CacheValue2<T>(this string key, Func<string, T> func, TimeSpan? timespan = null)
        {
            if (GetCache(key) is not CacheObject<T> cacheObj)
                lock (LockHelper.GetLockObject("SysCache-" + key))
                {
                    if (GetCache(key) is not CacheObject<T> s)
                    {
                        s = new CacheObject<T> { Result = func(key) };
                        SetCacheForNoAbsoluteExpiration(key, s, timespan);
                    }

                    if (s.Result == null) return default;
                    return s.Result;
                }

            if (cacheObj.Result == null) return default;
            return cacheObj.Result;
        }

        /// <summary>
        /// 从内存缓存中删除指定项
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void ClearCache(this string cacheKey)
        {
            CacheDefault.Remove(cacheKey);
        }

        public static T Get<T>(string key, Func<ICacheEntry, T> factory)
        {
            return CacheDefault.GetOrCreate(key, factory);
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            return CacheDefault.Get(key);
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            var n = CacheDefault.Get<CacheObject<T>>(key);
            return n != null ? n.Result : default;
        }

        /// <summary>
        /// 设置绝对过期的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiration">like 10，单位秒，默认是10秒</param>
        public static void SetCache<T>(string key, T obj, long? expiration = null)
        {
            DateTime datetime;
            if (!expiration.HasValue)
            {
                var memoryCacheTimeout = AppConfigHelper.GetConfigInt("MemoryCacheTimeout", 10);
                datetime = DateTime.UtcNow.AddSeconds(memoryCacheTimeout);
            }
            else
            {
                datetime = DateTime.UtcNow.AddSeconds(expiration.Value);
            }

            SetCacheForNoSlidingExpiration(key, obj, datetime);
        }

        /// <summary>
        /// 设置滑动过期
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="timespan">like TimeSpan.FromSeconds(10)</param>
        public static void SetCacheForNoAbsoluteExpiration<T>(string cacheKey, T objObject, TimeSpan? timespan = null)
        {
            if (!timespan.HasValue)
            {
                var memoryCacheTimeout = AppConfigHelper.GetConfigInt("MemoryCacheTimeout", 10);
                timespan = TimeSpan.FromSeconds(memoryCacheTimeout);
            }

            if (objObject == null)
                CacheDefault.Remove(cacheKey);
            else
            {
                CacheDefault.Set(cacheKey, objObject, new MemoryCacheEntryOptions() { AbsoluteExpirationRelativeToNow = timespan });
            }
        }

        /// <summary>
        /// 设置绝对过期
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="datetime">like DateTime.Now.AddSeconds(10)</param>
        public static void SetCacheForNoSlidingExpiration<T>(string cacheKey, T objObject, DateTime? datetime = null)
        {
            if (!datetime.HasValue)
            {
                var memoryCacheTimeout = AppConfigHelper.GetConfigInt("MemoryCacheTimeout", 10);
                datetime = DateTime.Now.AddSeconds(memoryCacheTimeout);
            }

            if (objObject == null)
                CacheDefault.Remove(cacheKey);
            else
            {
                CacheDefault.Set(cacheKey, objObject, datetime.Value);
            }
        }
    }
}
