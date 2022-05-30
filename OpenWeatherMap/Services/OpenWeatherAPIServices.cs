using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using OpenWeatherMap.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherMap.Services
{
    public class OpenWeatherAPIServices : IDisposable
    {
        private readonly string apiKey = "**API-KEY**"; 
        private readonly HttpClient httpClient;
        private bool _disposed;
        public OpenWeatherAPIServices()
        {
            httpClient = new HttpClient();
        }

        private List<Location> GetLocations()
        {
            var result = JsonHelper.JsonFileReader<List<Location>>("city.list.json");
            return result;
        }

        private Location GetLocation(decimal Id)
        {
            List<Location> locations = GetLocations();
            return locations.FirstOrDefault(e => e.Id == Id);
        }

        private Uri GenerateOneCallURL(decimal latitude, decimal longitude, Units? unit)
        {
            string unitName = GetOpenWeatherMapUnitName(unit);
            return new Uri($"https://api.openweathermap.org/data/2.5/onecall?units={unitName}&lat={latitude}&lon={longitude}&exclude=current,minutely,hourly,alerts&appid={apiKey}");                        
        }

        public string GetOpenWeatherMapUnitName(Units? unit)
        {
            switch(unit)
            {
                case Units.celsius:
                    return "metric";
                case Units.fahrenheit:
                    return "imperial";
                default:
                    return string.Empty;                    
            }
        }

        public async Task<WeatherSummary> GetWeatherForecastAsync(decimal locationId, Units? unit = null)
        {
            var location = GetLocation(locationId);

            if (location != null)
            {
                var response = await httpClient.GetStringAsync(GenerateOneCallURL(location.Coord.Lat, location.Coord.Lon, unit));
                var summary = new WeatherSummary(response);
                summary.Location = location;
                return summary;
            }

            return null;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.
            httpClient.Dispose();
            _disposed = true;
        }
    }
}
