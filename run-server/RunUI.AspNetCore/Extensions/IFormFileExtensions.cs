using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class IFormFileExtensions
    {
        public static async Task SaveAsAsync(this IFormFile file, string filename)
        {
            using (var stream = file.OpenReadStream())
            {
                var b = new byte[stream.Length];
                await stream.ReadAsync(b, 0, b.Length);

                FileHelper.WriteAllBytes(filename, b);
            }
        }
    }
}
