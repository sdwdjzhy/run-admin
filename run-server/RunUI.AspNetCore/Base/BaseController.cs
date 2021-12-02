using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : Controller
    {

        protected Logger CurrLogger { get; } = LogManager.GetCurrentClassLogger();

        protected IFreeSql Orm { get; private set; }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Orm = this.GetService<IFreeSql>();
            base.OnActionExecuting(context);
        }
    }
}
