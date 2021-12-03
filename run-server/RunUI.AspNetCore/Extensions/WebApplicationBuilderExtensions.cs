using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace RunUI
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddRunUI(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPathProvider, DefaultPathProvider>();
            // Add services to the container.
            var mvcBuilder = builder.Services.AddControllers(option =>
            {
                // 设置当前的缓存策略
                option.CacheProfiles.Add("default", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 10,  // 10 s
                    VaryByQueryKeys = new string[] { "*" }
                });
                option.CacheProfiles.Add("longDefault", new Microsoft.AspNetCore.Mvc.CacheProfile
                {
                    Duration = 60 * 60,  //  1 hour
                    VaryByQueryKeys = new string[] { "*" }
                });


            });
            mvcBuilder
                // 设置Asp.netCore内置的Json工具的使用
                .AddJsonOptions(options =>
                {
            //添加对datetime的格式化支持
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            //添加对datetime?的格式化支持
            options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());

            //解决json的key被改成驼峰写法
            //options.JsonSerializerOptions.PropertyNamingPolicy = null;
            // 解决中文被编码的问题
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                });
            //启用服务器端缓存功能
            builder.Services.AddResponseCaching();
            builder.Services.Configure<WebEncoderOptions>(
                        options =>
                        {
                            options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
                        });


            builder.Services.AddHttpContextAccessor();

            return builder;
        }
    }
}
