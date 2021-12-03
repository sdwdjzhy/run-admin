using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public class DefaultPathProvider : IPathProvider
    {
        private readonly IWebHostEnvironment env;

        /// <summary>
        /// </summary>
        /// <param name="environment"></param>
        public DefaultPathProvider(IWebHostEnvironment environment)
        {
            env = environment;
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string MapPath(string path)
        {
            var RootPath = env.ContentRootPath;
            Console.WriteLine($"RootPath:【{RootPath}】");
            if (path.IsNullOrWhiteSpace()) return RootPath;

            path = path.Trim();
            path = path.Replace('/', Path.DirectorySeparatorChar);

            if (path.StartsWithIgnoreCase("~"+ Path.DirectorySeparatorChar)) return RootPath + path[2..];

            if (path.StartsWithIgnoreCase(Path.DirectorySeparatorChar.ToString())) return Path.Combine(RootPath, path[1..]);

            return path;
        }
    }
}
