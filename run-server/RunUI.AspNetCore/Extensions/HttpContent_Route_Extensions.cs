using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public static class HttpContent_Route_Extensions
    {

        /// <summary>
        /// 获取当前动作名称
        /// </summary>
        /// <returns></returns>
        public static string? GetActionName(this HttpContext context)
        {
            if (context == null) return "";
            var route = context.GetRouteData();
            var actionobj = route.Values["action"];
            return actionobj == null ? "" : actionobj.ToStringExt();
        }

        /// <summary>
        /// 获取当前区域名称
        /// </summary>
        /// <returns></returns>
        public static string? GetAreaName(this HttpContext context)
        {
            if (context == null) return "";
            var route = context.GetRouteData();

            var areaobj = route.Values["area"];

            return areaobj == null ? "Default" : areaobj.ToStringExt();
        }

        /// <summary>
        /// 获取当前的控制器名称
        /// </summary>
        /// <returns></returns>
        public static string GetControlerName(this HttpContext context)
        {
            if (context == null) return "";
            var route = context.GetRouteData();
            var controllerobj = route.Values["controller"];

            return controllerobj == null ? "" : controllerobj.ToStringExt();
        }
    }
}
