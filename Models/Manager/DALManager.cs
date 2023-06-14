using Npgsql;
using RestAPIExample.Models.POCO;

namespace RestAPIExample.Models.Manager
{
    public class DALManager : IDALManager
    {
        private readonly IConfiguration _configuration;
        public DALManager(IConfiguration config)
        {
            _configuration = config;
        }
        public IEnumerable<ModelWeatherForecast?>? FetchWeatherForecasts()
        {
            List<ModelWeatherForecast> weatherForecasts = new();

            using (NpgsqlConnection conn = new(_configuration.GetConnectionString("Weather_Database")))
            {
                try
                {
                    conn.Open();
                    using NpgsqlCommand command = new("select * from read_all_weather_data()", conn);
                    using NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        weatherForecasts.Add(new ModelWeatherForecast()
                        {
                            Date = (DateTime)reader["received_date"],
                            TemperatureC = (int)reader["temp_celcius"],
                            TemperatureF = (int)reader["temp_farenheit"],
                            Summary = (string)reader["summary"]
                        });
                    }

                }
                finally
                {
                    conn.Close();
                }
               

            }
            return weatherForecasts;
        }
    }
}