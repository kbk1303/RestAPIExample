using Microsoft.AspNetCore.Mvc;
using RestAPIExample.Models.Manager;
using RestAPIExample.Models.POCO;
using System.Linq;

namespace RestAPIExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDALManager _dalManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDALManager manager)
        {
            _logger = logger;
            _dalManager = manager;
        }
        
        [HttpGet] 
        [Route("forecast/{summary}", Name = "FetchForecast")]
        public ActionResult<WeatherForecast>? FetchForecast(string summary)
        {
            List<WeatherForecast?>? forecasts = AllForecasts();

            return forecasts!.Exists(i => i?.Summary?.ToLower() == summary.ToLower()) ? Ok(forecasts.Where(p => p?.Summary?.ToLower() == summary.ToLower()).FirstOrDefault()): NoContent();
        }
        
        
        [HttpGet]
        [Route ("all")]
        public ActionResult<IEnumerable<WeatherForecast?>>? FetchForecasts()
        {
            
            IEnumerable<WeatherForecast?>? forecasts = AllForecasts();
            return forecasts!.Any() ? Ok(forecasts) : NoContent();
        }

        private List<WeatherForecast?>? AllForecasts()
        {
            List<WeatherForecast?>? forecasts = new();
            foreach (ModelWeatherForecast? mfc in _dalManager.FetchWeatherForecasts()!)
            {
                forecasts.Add(new WeatherForecast() { Date = mfc?.Date, Summary = mfc?.Summary, TemperatureC = mfc?.TemperatureC, TemperatureF = mfc?.TemperatureF });
            }
            return forecasts;
        }
    }
}