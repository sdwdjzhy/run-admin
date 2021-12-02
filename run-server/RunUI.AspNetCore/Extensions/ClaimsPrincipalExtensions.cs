using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetRunUIUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return "";
            }

            var id = claimsPrincipal.Claims?.FirstOrDefault(i => i.Type == "id")?.Value;

            if (id.HasValue())
            {
                return id;
            }
            else
            {
                id = claimsPrincipal.Claims?.FirstOrDefault(i => i.Type == ClaimTypes.Sid)?.Value;

                return id.NullWhiteSpaceForDefault(claimsPrincipal.Identity.Name);
            }
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static string GetRunUIUserName(this ClaimsPrincipal claimsPrincipal)
        {
            if (!claimsPrincipal.Identity.IsAuthenticated)
            {
                return "";
            }
            var userName = claimsPrincipal.Claims?.FirstOrDefault(i => i.Type == "name")?.Value;
            if (userName.HasValue())
            {
                return userName;
            }
            else
            {
                return claimsPrincipal.Claims?.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value;
            }
        }
    }
}
