using RestAPIExample.Models.POCO;

namespace RestAPIExample.Models.Manager
{
    public class MockupDALManager : IDALManager
    {
        public IEnumerable<ModelWeatherForecast?>? FetchWeatherForecasts()
        {
            List<ModelWeatherForecast?> wfcs = new()
            {
                new ModelWeatherForecast() { Date = DateTime.Now, TemperatureC = 38, TemperatureF = 72, Summary = "Cold" },
                new ModelWeatherForecast() { Date = DateTime.Now, TemperatureC = 43, TemperatureF = 129, Summary = "Phweew" },
                new ModelWeatherForecast() { Date = DateTime.Now, TemperatureC = 54, TemperatureF = 132, Summary = "Ewww!" },
                new ModelWeatherForecast() { Date = DateTime.Now, TemperatureC = 18, TemperatureF = 65, Summary = "Nice" },
                new ModelWeatherForecast() { Date = DateTime.Now, TemperatureC = -10, TemperatureF = 45, Summary = "Freezing" }
            };

            return wfcs;
        }
    }
}
