using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using OpenWeatherMap.Models;
using OpenWeatherMap.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.BusinessLogic
{
    public class WeatherBusinessLogic
    {
        public async Task<List<WeatherSummary>> GetLocationsNextDayForecast(string unit, decimal? temperature, string locations)
        {
            if (String.IsNullOrWhiteSpace(unit)) throw new ArgumentNullException("Unit is required.");
            else
            {
                bool success = Enum.IsDefined(typeof(Units), unit.ToLower());
                if (!success) throw new Exception("Unit value is invalid.");
            }

            if (temperature == null) throw new ArgumentNullException("Temperature is required.");
            if (String.IsNullOrWhiteSpace(locations)) throw new ArgumentNullException("Location(s) is required.");
            if (!StringHelper.IsValidCommaSeparatedString(locations)) throw new ArgumentException("Location(s) value is incorrect format.");

            var weatherSummaries = new List<WeatherSummary>();
            
            string[] locationList = locations.Split(',').Distinct().ToArray();

            Units unitValue = (Units)Enum.Parse(typeof(Units), unit.ToLower());

            foreach (var loc in locationList)
            {               
                var summary = await new OpenWeatherAPIServices().GetWeatherForecastAsync(decimal.Parse(loc), unitValue);
                if (summary != null)
                {
                    var forecast = summary.DailyForecasts.Where(e => e.ForecastedDateTime.Date == DateTime.UtcNow.AddDays(1).Date && e.Temperature.MaxDaily > temperature).ToList();

                    if (forecast.Any())
                    {
                        var weatherSummary = summary;
                        weatherSummary.DailyForecasts = forecast;
                        weatherSummaries.Add(weatherSummary);
                    }
                }
            }

            return weatherSummaries;
        }

        public async Task<WeatherSummary> GetLocWeatherForTheNext5DaysAsync(decimal locationId)
        {
            return await GetLocWeatherForTheNextNDaysAsync((decimal)locationId, 5);
        }

        public async Task<WeatherSummary> GetLocWeatherForTheNextNDaysAsync(decimal locationId, int numDays)
        {
            var weatherSummary = await new OpenWeatherAPIServices().GetWeatherForecastAsync(locationId);
            if (weatherSummary != null)
            {
                weatherSummary.DailyForecasts = weatherSummary.DailyForecasts.Where(e => e.ForecastedDateTime.Date > DateTime.UtcNow.Date).Take(numDays).ToList();
                return weatherSummary;
            }
            return null;            
        }
    }
}
