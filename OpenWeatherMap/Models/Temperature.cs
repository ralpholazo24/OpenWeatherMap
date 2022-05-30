using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class Temperature
    {
        public decimal Morning { get; set; }
        public decimal Day { get; set; }
        public decimal Evening { get; set; }
        public decimal Night { get; set; }
        public decimal MinDaily { get; set; }
        public decimal MaxDaily { get; set; }


        public Temperature(JToken jToken)
        {
            this.Morning = decimal.Parse(jToken.SelectToken("morn").ToString());
            this.Day = decimal.Parse(jToken.SelectToken("day").ToString());
            this.Evening = decimal.Parse(jToken.SelectToken("eve").ToString());
            this.Night = decimal.Parse(jToken.SelectToken("night").ToString());
            this.MinDaily = decimal.Parse(jToken.SelectToken("min").ToString());
            this.MaxDaily = decimal.Parse(jToken.SelectToken("max").ToString());
        }
    }
}
