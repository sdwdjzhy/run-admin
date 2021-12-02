using Microsoft.AspNetCore.Mvc;

namespace RunUI.Api.Controllers
{
    public class WeatherForecastController : BaseController
    {

        [HttpGet]
        public string[] Get()
        {
            var id = OrderIdHelper.ObjecId();
            var id1 = OrderIdHelper.NewId();
            var id2 = OrderIdHelper.GuidString();

            return new[] { id, id1, id2 };
        }
    }
}