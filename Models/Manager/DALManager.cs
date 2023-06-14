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

            using(NpgsqlConnection conn = new NpgsqlConnection(_configuration.GetConnectionString("Database")))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("select * from read_all_weather_data()", conn))
                    {
                        using(NpgsqlDataReader reader = command.ExecuteReader())
                        {
                         
                            while(reader.Read())
                            {
                                weatherForecasts.Add(new ModelWeatherForecast() {   
                                    Date = (DateTime)reader["received_date"],
                                    TemperatureC = (int)reader["temp_celcius"],
                                    TemperatureF = (int)reader["temp_farenheit"],
                                    Summary = (string)reader["summary"]
                                });
                            }
                        }
                    }

                }
                finally
                {
                    conn.Close();
                }
               

            }
            /*
            return Enumerable.Range(1, 1).Select(index => new ModelWeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Cold"
            })
            .ToArray();
            */
            return weatherForecasts;
        }
    }
}