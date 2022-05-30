using Newtonsoft.Json.Linq;
using OpenWeatherMap.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class WeatherSummary
    {
        public Location Location { get; set; }        
        public string Timezone { get; set; }
        public decimal TimezoneOffset { get; set; }
        public List<DailyForecast> DailyForecasts { get; set; } = new List<DailyForecast>();
        public WeatherSummary(string response, Units? unit)
        {
            var json = JObject.Parse(response);

            if (json != null)
            {
                this.Timezone = json.SelectToken("timezone").ToString();
                this.TimezoneOffset = decimal.Parse(json.SelectToken("timezone_offset").ToString());
                foreach (JToken daily in json.SelectToken("daily"))
                    DailyForecasts.Add(new DailyForecast(daily, unit));
            }
        }

    }
}
