using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class WebApplicationExtenstions
    {
        /// <summary>
        /// 使用代理报文头转发
        /// </summary>
        /// <param name="app"></param>
        private static void UseRunUIForwardedHeaders(this WebApplication app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });
        }

        /// <summary>
        /// 启用Linux静态文件大小写不敏感
        /// </summary>
        /// <param name="app"></param>
        private static void UseRunUIIgnoreCaseUrl(this WebApplication app)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var env = app.Environment;
                var rootPath = env.WebRootPath ?? env.ContentRootPath;
                var wwwrootDirs = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories)
                    .Select(i => i.Replace(rootPath, "").Replace("\\", "/"))
                    .ToList();

                app.UseMiddleware<IgnoreCaseUrlMiddleware>(wwwrootDirs);
            }
        }

        /// <summary>
        /// 启用Linux静态文件大小写不敏感
        /// </summary>
        /// <param name="app"></param>
        private static void UseRunUICors(this WebApplication app)
        {
            app.UseCors(builder =>
            {
                builder.SetIsOriginAllowed(
                    k =>
                    {
                        return true;
                    }).AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        }

        public static void UseRunUI(this WebApplication app)
        {
            app.UseRunUIForwardedHeaders();
            app.UseRunUIIgnoreCaseUrl();
            app.UseRunUICors();
            
        }
    }
}
