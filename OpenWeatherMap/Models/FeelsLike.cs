using Newtonsoft.Json.Linq;
using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class FeelsLike
    {
        public double Morning { get; set; }
        public double Day { get; set; }
        public double Eve { get; set; }
        public double Night { get; set; }

        public FeelsLike(JToken jToken, Units? unit)
        {
            this.Morning = double.Parse(jToken.SelectToken("morn").ToString());
            this.Day = double.Parse(jToken.SelectToken("day").ToString());
            this.Eve = double.Parse(jToken.SelectToken("eve").ToString());
            this.Night = double.Parse(jToken.SelectToken("night").ToString());
            this.ConvertTemperatureUnit(unit);
        }

        private void ConvertTemperatureUnit(Units? unit)
        {
            this.Morning = TemperatureHelper.ConvertKelvin(this.Morning, unit);
            this.Day = TemperatureHelper.ConvertKelvin(this.Day, unit);
            this.Eve = TemperatureHelper.ConvertKelvin(this.Eve, unit);
            this.Night = TemperatureHelper.ConvertKelvin(this.Night, unit);
        }
    }
}
