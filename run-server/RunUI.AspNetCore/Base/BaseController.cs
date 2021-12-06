using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace RunUI
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : Controller
    {

        protected Logger CurrLogger { get; } = LogManager.GetCurrentClassLogger();


        protected IPathProvider Server { get; private set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Server = this.GetService<IPathProvider>();
            base.OnActionExecuting(context);
        }
    }
}
