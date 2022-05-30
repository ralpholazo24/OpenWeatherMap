using Newtonsoft.Json.Linq;
using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class Temperature
    {
        public double Morning { get; set; }
        public double Day { get; set; }
        public double Evening { get; set; }
        public double Night { get; set; }
        public double MinDaily { get; set; }
        public double MaxDaily { get; set; }


        public Temperature(JToken jToken, Units? unit)
        {
            this.Morning = double.Parse(jToken.SelectToken("morn").ToString());
            this.Day = double.Parse(jToken.SelectToken("day").ToString());
            this.Evening = double.Parse(jToken.SelectToken("eve").ToString());
            this.Night = double.Parse(jToken.SelectToken("night").ToString());
            this.MinDaily = double.Parse(jToken.SelectToken("min").ToString());
            this.MaxDaily = double.Parse(jToken.SelectToken("max").ToString());
            this.ConvertTemperatureUnit(unit);
        }

        private void ConvertTemperatureUnit(Units? unit)
        {
            this.Morning = TemperatureHelper.ConvertKelvin(this.Morning, unit);
            this.Day = TemperatureHelper.ConvertKelvin(this.Day, unit);
            this.Evening = TemperatureHelper.ConvertKelvin(this.Evening, unit);
            this.Night = TemperatureHelper.ConvertKelvin(this.Night, unit);
            this.MinDaily = TemperatureHelper.ConvertKelvin(this.MinDaily, unit);
            this.MaxDaily = TemperatureHelper.ConvertKelvin(this.MaxDaily, unit);
        }


    }
}
