using Microsoft.AspNetCore.Http;
using RunUI.SysModels;
using System.Security.Claims;

namespace RunUI
{
    public class DefaultTenantProvider : ITenantProvider
    {
        private readonly HttpContext httpContext;
        private readonly IFreeSql orm;

        public DefaultTenantProvider(IHttpContextAccessor httpContextAccessor, IFreeSql freeSql)
        {
            httpContext = httpContextAccessor.HttpContext;
            orm = freeSql;
        }
        public async Task<Tenant> GetTenant()
        {
            var user = httpContext.User;
            if (user != null || user.Identity.IsAuthenticated)
            {
                var claims = httpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.GroupSid);
                if (claims != null)
                {

                    var tenant = new Tenant
                    {
                        Id = claims.Value,
                    };

                    var sys_tenant = await $"Tenant-{tenant.Id}".RedisCacheValue(async k =>
                     {
                         return await orm.Select<TTenant>().Where(i => i.Id == tenant.Id).FirstAsync();
                     }, DateTime.Now.AddSeconds(200));
                    tenant.Name = sys_tenant.Name;
                    return tenant;
                }
            }
            return null;
        }

        public string GetTenantId()
        {
            var user = httpContext.User;
            if (user != null || user.Identity.IsAuthenticated)
            {
                var claims = httpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.GroupSid);
                if (claims != null)
                {
                    return claims.Value;
                }
            }
            return "1";
        }
    }
}
