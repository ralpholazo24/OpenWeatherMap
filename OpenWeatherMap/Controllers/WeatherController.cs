using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OpenWeatherMap.BusinessLogic;
using OpenWeatherMap.Enums;
using OpenWeatherMap.Interface;
using OpenWeatherMap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        public WeatherController(IDistributedCache distributedCache)
        {
            CacheProvider.RegisterCacheProvider(distributedCache);
        }

        [HttpGet("Summary")]
        [Produces("application/json")]
        public async Task<List<WeatherSummary>> Summary(string unit, double? temperature, string locations)
        {
            try
            {
                return await new WeatherBusinessLogic().GetLocationsNextDayForecast(unit, temperature, locations);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Location")]
        [Produces("application/json")]
        public async Task<WeatherSummary> Location(decimal loc_id)
        {
            try
            {
                return await new WeatherBusinessLogic().GetLocWeatherForTheNext5DaysAsync(loc_id);
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }   
}
