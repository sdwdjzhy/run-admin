using System;
using System.Diagnostics;
using CSRedis;

namespace RunUI
{
    /// <summary>
    /// Redis锁
    /// </summary>
    public sealed class RedisLock : IDisposable
    {
        private string Key { get; }
        private string Value { get; }
        private bool disposed;
        private bool IsLock;

        /// <summary>
        /// 锁
        /// </summary>
        public RedisLock(string lockKey, int timeout = 50000)
        {
            RedisManager.Init();
            var siteKey = AppConfigHelper.GetAppSettingsString("RedisLockKey");
            Key = $"Lock-{siteKey}-{lockKey}";
            Value = Guid.NewGuid().ToString();
            bool b;
            var sw = new Stopwatch();
            sw.Start();
            do
            {
                b = RedisLockHelper.Set(Key, Value, timeout * 60 / 1000, RedisExistence.Nx);

                if (b == false) ThreadHelper.Sleep(10);
            } while (b == false && sw.ElapsedMilliseconds < timeout);

            sw.Stop();
            if (b == false) throw new TimeoutException("获取redis分布锁失败");
            IsLock = true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposed) return;
            var f = false;
            lock (this)
            {
                if (IsLock)
                {
                    IsLock = false;
                    f = true;
                }
            }

            if (!f) return;
            const string lua = "if redis.call('get', KEYS[1]) == ARGV[1] then return redis.call('del', KEYS[1]) else return 0 end";

            RedisLockHelper.Eval(lua, Key, Value);

            disposed = true;
        }

        /// <summary>
        /// 获取redis分布式锁
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static RedisLock GetRedisLock(string lockKey, int timeout = 50000)
        {
            return new RedisLock(lockKey, timeout);
        }

        /// <summary>
        /// 解锁并释放redis链接
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}