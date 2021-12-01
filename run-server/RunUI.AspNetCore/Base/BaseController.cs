using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunUI
{
    public abstract class BaseController : ControllerBase
    {

        private readonly ILogger<BaseController> Logger;

        public BaseController() { 
        
        }
    }
}
