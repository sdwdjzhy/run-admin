namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class RedisCache
    {
        private class CacheObject<T>
        {
            public T Result { get; set; }
        }

        /// <summary>
        /// Redis缓存，滑动过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration">秒</param>
        /// <returns></returns>
        public static T CacheValue<T>(string key, Func<string, T> func, int? expiration = null)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;

            string objStr;

            objStr = RedisLockHelper.Get(redisKey);

            if (objStr == null)
                using (new RedisLock(redisKey))
                {
                    objStr = RedisLockHelper.Get(redisKey);

                    if (objStr == null)
                    {
                        var obj = func(key);
                        var cacheobj = new CacheObject<T> { Result = obj };
                        var json = cacheobj.JsonSerialize();

                        expiration = expiration ?? RedisManager.MemoryCacheTimeout;

                        RedisLockHelper.Set(redisKey, json, expiration.Value);

                        objStr = json;
                    }
                }

            var s = objStr.ToStringExt()?.JsonDeserialize<CacheObject<T>>();
            if (s != null)
                return s.Result;
            return default;
        }

        /// <summary>
        /// Redis缓存，滑动过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <param name="expiration">秒</param>
        /// <returns></returns>
        public static async Task<T> CacheValueAsync<T>(string key, Func<string, Task<T>> func, int? expiration = null)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;

            string objStr;

            objStr = RedisLockHelper.Get(redisKey);

            if (objStr == null)
                using (new RedisLock(redisKey))
                {
                    objStr = RedisLockHelper.Get(redisKey);

                    if (objStr == null)
                    {
                        var obj = await func(key);
                        var cacheobj = new CacheObject<T> { Result = obj };
                        var json = cacheobj.JsonSerialize();

                        expiration = expiration ?? RedisManager.MemoryCacheTimeout;

                        RedisLockHelper.Set(redisKey, json, expiration.Value);

                        objStr = json;
                    }
                }

            var s = objStr.ToStringExt()?.JsonDeserialize<CacheObject<T>>();
            if (s != null)
                return s.Result;
            return default;
        }

        /// <summary>
        /// 清除Redis缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void ClearCache(string key)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;

            RedisLockHelper.Del(redisKey);
        }

        /// <summary>
        /// 清除Redis缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task ClearCacheAsync(string key)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;
            await RedisLockHelper.DelAsync(redisKey);
        }

        /// <summary>
        /// 清除Redis缓存
        /// </summary>
        /// <param name="pattern">通配符语法，xxx*,xx:xx*</param>
        /// <returns></returns>
        public static void ClearCacheWithPattern(string pattern)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + pattern;

            //var script = $"return redis.call('del', unpack(redis.call('keys','*{pattern}*')))";
            var script = $@"
local dataInfos = redis.call('keys','{redisKey}')
if(dataInfos ~= nil) then
        for i=1,#dataInfos,1 do
                redis.call('del',dataInfos[i])
        end
        return #dataInfos
else
        return 0
end
";
            RedisLockHelper.Eval(script, "");
        }

        /// <summary>
        /// 清除Redis缓存
        /// </summary>
        /// <param name="pattern">通配符语法，xxx*,xx:xx*</param>
        /// <returns></returns>
        public static async Task ClearCacheWithPatternAsync(string pattern)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + pattern;
            //var script = $"return redis.call('del', unpack(redis.call('keys','*{pattern}*')))";
            var script = $@"
local dataInfos = redis.call('keys','{redisKey}')
if(dataInfos ~= nil) then
        for i=1,#dataInfos,1 do
                redis.call('del',dataInfos[i])
        end
        return #dataInfos
else
        return 0
end
";
            await RedisLockHelper.EvalAsync(script, "");
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;
            string objStr;

            objStr = RedisLockHelper.Get(redisKey);

            if (objStr.IsNullOrWhiteSpace()) return default;

            var s = objStr.JsonDeserialize<CacheObject<T>>();
            if (s != null)
                return s.Result;
            return default;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="expiration">秒</param>
        public static void SetCache<T>(string key, T obj, int? expiration = null)
        {
            RedisManager.Init();
            var redisKey = "RedisCache-" + key;
            var cacheobj = new CacheObject<T> { Result = obj };
            var json = cacheobj.JsonSerialize();
            expiration = expiration ?? RedisManager.MemoryCacheTimeout;

            RedisLockHelper.Set(redisKey, json, expiration.Value);
        }
    }
}