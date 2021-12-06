using Microsoft.Extensions.Configuration;

namespace RunUI
{
    /// <summary>
    /// 缓存相关的操作类
    /// </summary>
    public static class AppConfigHelper
    {
        private static IConfiguration Configuration
        {
            get
            {
                if (configurationRoot == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true);
                    //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//这里采用appsettings.{env.EnvironmentName}.json根据当前的运行环境来加载相应的appsettings文件
                    //.AddEnvironmentVariables();
                    configurationRoot = builder.Build();
                }
                return configurationRoot;
            }
        }

        /// <summary>
        /// 设置新的ConfigurationRoot
        /// </summary>
        /// <param name="cr"></param>
        public static void SetConfigurationRoot(IConfiguration cr)
        {
            configurationRoot = cr;
        }

        private static IConfiguration configurationRoot;

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <returns></returns>
        public static IConfigurationSection AppSettings => Configuration.GetSection("AppSettings");

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string AppSetting(string key, string defaultValue = null)
        {
            return GetAppSettingsString(key, defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetAppSettingsBool(string key, bool defaultValue = false)
        {
            return GetConfigBool(key, defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetAppSettingsDecimal(string key, decimal defaultValue = 0)
        {
            return GetConfigDecimal(key, defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetAppSettingsInt(string key, int defaultValue = 0)
        {
            return GetConfigInt(key, defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetAppSettingsString(string key, string defaultValue = null)
        {
            var appSettings = Configuration.GetSection("AppSettings");
            var value = appSettings[key];

            if (value.IsNullOrWhiteSpace() && defaultValue.HasValue()) return defaultValue;

            return value;
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetConfigBool(string key, bool defaultValue = false)
        {
            var cfgVal = GetAppSettingsString(key);
            return cfgVal.ToBoolOrDefault(defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key, decimal defaultValue = 0)
        {
            var cfgVal = GetAppSettingsString(key);
            return cfgVal.ToDecimalOrDefault(defaultValue);
        }

        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetConfigInt(string key, int defaultValue = 0)
        {
            var cfgVal = GetAppSettingsString(key);
            return cfgVal.ToIntOrDefault(defaultValue);
        }

        /// <summary>
        /// 得到 ConnectionStrings 中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetConnectionString(string key, string defaultValue = null)
        {
            return Configuration.GetConnectionString(key).NullWhiteSpaceForDefault(defaultValue);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }
    }
}