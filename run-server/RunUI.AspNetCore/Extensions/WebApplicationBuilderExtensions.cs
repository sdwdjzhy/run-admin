using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder UseRunUI(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IPathProvider, DefaultPathProvider>();

            return builder;
        }
    }
}
