using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class FeelsLike
    {
        public decimal Morning { get; set; }
        public decimal Day { get; set; }
        public decimal Eve { get; set; }
        public decimal Night { get; set; }

        public FeelsLike(JToken jToken)
        {
            this.Morning = decimal.Parse(jToken.SelectToken("morn").ToString());
            this.Day = decimal.Parse(jToken.SelectToken("day").ToString());
            this.Eve = decimal.Parse(jToken.SelectToken("eve").ToString());
            this.Night = decimal.Parse(jToken.SelectToken("night").ToString());
        }
    }
}
