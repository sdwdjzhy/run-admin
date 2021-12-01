using Microsoft.AspNetCore.Mvc;

namespace RunUI.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name ="asdf")]
        public IEnumerable<int> Get()
        {
            _logger.LogInformation("adsfasdfasdf");
            return new[] { 0 };
        }
    }
}