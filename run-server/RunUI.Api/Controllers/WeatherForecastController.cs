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

        [HttpGet(Name ="asdf")]
        public IEnumerable<int> Get()
        {
            _logger.LogInformation("asdfadsfasdf");
            return new[] { 0 };
        }
    }
}