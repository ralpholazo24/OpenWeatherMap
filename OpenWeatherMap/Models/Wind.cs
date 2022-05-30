using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class Wind
    {
        public decimal Speed { get; set; }
        public decimal Gust { get; set; }
        public decimal Degrees { get; set; }

        public Wind(JToken jToken)
        {
            this.Speed = decimal.Parse(jToken.SelectToken("wind_speed").ToString());
            this.Gust = decimal.Parse(jToken.SelectToken("wind_gust") != null ? jToken.SelectToken("wind_gust").ToString() : "0");
            this.Degrees = decimal.Parse(jToken.SelectToken("wind_deg").ToString());
        }
    }
}
