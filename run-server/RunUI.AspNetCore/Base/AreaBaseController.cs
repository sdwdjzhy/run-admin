using Microsoft.AspNetCore.Mvc;
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
    [Route("[area]/[controller]/[action]")]
    public abstract class AreaBaseController : BaseController
    {
    }
}
