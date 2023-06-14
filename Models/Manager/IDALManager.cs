using RestAPIExample.Models.POCO;

namespace RestAPIExample.Models.Manager
{
    public interface IDALManager
    {
        public IEnumerable<ModelWeatherForecast?>? FetchWeatherForecasts();
    }
}
