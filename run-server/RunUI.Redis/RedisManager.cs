using System;
using CSRedis;

namespace RunUI
{
    /// <summary>
    /// </summary>
    public static class RedisManager
    {
        private static bool HasInit;

        /// <summary>
        /// 哨兵默认端口
        /// </summary>
        public static readonly string DefaultSentinelPort = AppConfigHelper.GetAppSettingsString("redis.defaultport", "6379");

        /// <summary>
        /// Redis默认失效时间
        /// </summary>
        public static readonly int MemoryCacheTimeout = AppConfigHelper.GetConfigInt("MemoryCacheTimeout", 10);

        /// <summary>
        /// Redis配置字符串
        /// </summary>
        public static readonly string RedisConnectionString = AppConfigHelper.AppSetting("redislock.ConnectionString");

        /// <summary>
        /// Redis哨兵Master地址
        /// </summary>
        public static readonly string RedisMasterName = AppConfigHelper.AppSetting("redis.masterName");

        /// <summary>
        /// Redis密码
        /// </summary>
        public static readonly string RedisPassword = AppConfigHelper.AppSetting("redis.password");

        /// <summary>
        /// Redis哨兵配置地址
        /// </summary>
        public static readonly string RedisSentinelAddresses = AppConfigHelper.AppSetting("redis.sentinelAddresses");

        /// <summary>
        /// RedisSentinelManager实例
        /// </summary>
        public static void Init()
        {
            if (HasInit)
                return;

            lock (LockHelper.GetLockObject("redis Sentinel"))
            {
                if (HasInit)
                    return;

                if (RedisConnectionString.HasValue())
                {
                    RedisLockHelper.Initialization(new CSRedisClient(RedisConnectionString));
                }
                else
                {
                    if (string.IsNullOrEmpty(RedisSentinelAddresses)) throw new Exception("没有配置Redis的哨兵地址");

                    var server = RedisSentinelAddresses;

                    if (DefaultSentinelPort.HasValue()) server += ":" + DefaultSentinelPort;
                    if (RedisPassword.HasValue()) server += ",password=" + RedisPassword;
                    server += ",testcluster=false,preheat=5,idleTimeout=15000";

                    RedisLockHelper.Initialization(new CSRedisClient(server));
                }

                HasInit = true;
            }
        }
    }
}