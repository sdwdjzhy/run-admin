using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RunUI
{
    /// <summary>
    /// linux静态文件忽略大小写中间件实现，需要放静态文件中间件前
    /// </summary>
    internal class IgnoreCaseUrlMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IEnumerable<string> _wwwrootDirs;
        private readonly ConcurrentDictionary<string, string> _wwwrootDirs;

        public IgnoreCaseUrlMiddleware(RequestDelegate next, IEnumerable<string> wwwrootDirs)
        {
            var dict = wwwrootDirs.Distinct().ToDictionary(t => t.ToLower(), t => t);
            _wwwrootDirs = new ConcurrentDictionary<string, string>(dict);

            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();
            if (_wwwrootDirs.TryGetValue(path, out var serverPath))
            {
                context.Request.Path = new PathString(serverPath);
            }
            return _next(context);
        }
    }
}
