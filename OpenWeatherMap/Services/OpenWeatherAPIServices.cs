using Microsoft.Extensions.Caching.Distributed;
using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMap.Services
{
    public class OpenWeatherAPIServices : IDisposable
    {
        private readonly string apiKey = "**API-KEY**"; 
        private readonly HttpClient httpClient;
        private bool _disposed;
        private readonly IDistributedCache distributedCache;
        public OpenWeatherAPIServices()
        {
            httpClient = new HttpClient();
            this.distributedCache = CacheProvider.GetCacheProvider();
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

        private Uri GenerateOneCallURL(decimal latitude, decimal longitude)
        {
            return new Uri($"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=current,minutely,hourly,alerts&appid={apiKey}");
        }

        public async Task<WeatherSummary> GetWeatherForecastAsync(decimal locationId, Units? unit = null)
        {
            var cacheKey = locationId.ToString();
            var location = GetLocation(locationId);
            var response = string.Empty;

            if (location != null)
            {
                var redisWeatherForecast = await distributedCache.GetAsync(cacheKey);

                if (redisWeatherForecast != null)
                {
                    response = Encoding.UTF8.GetString(redisWeatherForecast);
                }
                else
                {
                    response = await httpClient.GetStringAsync(GenerateOneCallURL(location.Coord.Lat, location.Coord.Lon));
                    redisWeatherForecast = Encoding.UTF8.GetBytes(response);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisWeatherForecast, options);
                }

                var summary = new WeatherSummary(response, unit);
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
