using Microsoft.AspNetCore.Builder;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class NLogExtensions
    {

        /// <summary>
        /// 设置默认的日志配置
        /// <para>
        /// 如果使用asp.net mvc,需引用NLog.Web,并在Global中，执行此方法之前加入代码
        /// </para>
        /// </summary>
        public static LoggingConfiguration GetDefaultNLogSetting()
        {
            var config = new LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile")
            {
                FileName = "${basedir}/app_data/${shortdate}/${logger}-${level}-${date:format=HH}点.log",
                Layout = @"
<--log start (用于计数) ${longdate}|${logger} -->
${message}
<--log end -->",
                Encoding = Encoding.UTF8
            };

            // Rules for mapping loggers to targets
            config.AddRuleForAllLevels(logfile);
            LogManager.Configuration = config;
            return config;
        }
    }
}
