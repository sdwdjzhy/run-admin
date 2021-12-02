using Microsoft.AspNetCore.Builder;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class NLogAspNetCoreExtensions
    { 
        /// <summary>
        /// 设置默认的日志配置
        /// <para>
        /// 如果使用asp.net mvc,需引用NLog.Web,并在Global中，执行此方法之前加入代码
        /// </para>
        /// </summary>
        public static LoggingConfiguration GetDefaultNLogAspNetCoreSetting()
        {
            var config = new LoggingConfiguration();

            AspNetLayoutRendererBase.Register("logfilepath",
                  (logEventInfo, httpContext, loggingConfiguration) =>
                  {
                      var area = httpContext.GetAreaName();
                      var controller = httpContext.GetControlerName();
                      var action = httpContext.GetActionName();
                      var list = new List<string>
                      {
                          area,
                          controller,
                          action
                      };
                      list = list.Where(i => string.IsNullOrWhiteSpace(i) == false).ToList();

                      var path = string.Join("/", list);
                      return path;
                  });

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile")
            {
                FileName = "${basedir}/app_data/${shortdate}/${logfilepath}-${level}-${date:format=HH}.log",
                Layout = @"
<-- log start (用于计数) ${longdate}|${logger} -->
url:${aspnet-request-url}
controller-action:${aspnet-mvc-controller}/${aspnet-mvc-action}
querystring:${aspnet-request-querystring}
body:${aspnet-request-posted-body}
identity:${aspnet-user-identity}
useragent:${aspnet-request-useragent}
callsite:${callsite}
filename:${callsite-filename}
linenumber:${callsite-linenumber}
message:【${message}】
exception:【${exception}】
stacktrace:【${stacktrace}】
<-- log end -->",
                Encoding = Encoding.UTF8
            };

            // Rules for mapping loggers to targets
            config.AddRuleForAllLevels(logfile);
            NLogBuilder.ConfigureNLog(config);
            return config;
        }
    }
}


