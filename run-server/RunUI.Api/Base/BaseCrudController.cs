using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using RunUI.SysModels;

namespace RunUI
{
    public abstract class BaseCrudController<T> : BaseController where T : BaseModel
    {
        protected IFreeSql Orm { get; private set; }
        protected IBaseRepository<T> Repository { get; private set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Orm = this.GetService<IFreeSql>();
            Repository = Orm.GetRepository<T>(i => i.IsDeleted == false);
        }
    }
}
