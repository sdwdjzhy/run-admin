using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using RunUI.SysModels;

namespace RunUI
{
    public abstract class BaseCrudTenantController<T> : BaseController where T : BaseTenantModel
    {
        protected IFreeSql Orm { get; private set; }
        protected IBaseRepository<T> Repository { get; private set; }
        protected ITenantProvider TenantProvider { get; private set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Orm = this.GetService<IFreeSql>();
            TenantProvider = this.GetService<ITenantProvider>();

            var tenantId = await TenantProvider.GetTenantId();
            Repository = Orm.GetRepository<T>(i => i.TenantId == tenantId && i.Flag >= 0);
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
