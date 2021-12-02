using Microsoft.AspNetCore.Mvc;

namespace RunUI.Api.Controllers
{
    [Area("Default")]
    [Route("[area]/[controller]/[action]")]
    public class WeatherForecastController : BaseController
    {
        private readonly ILogger<WeatherForecastController> _logger;


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "asdf")]
        public string[] Get()
        {
            var id = OrderIdHelper.ObjecId();
            var id1 = OrderIdHelper.NewId();
            var id2 = OrderIdHelper.GuidString();

            return new []{ id,id1,id2 };
        }
    }
}