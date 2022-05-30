using Newtonsoft.Json.Linq;
using OpenWeatherMap.Enums;
using OpenWeatherMap.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class DailyForecast
    {
        public DateTime ForecastedDateTime { get; set; }
        public DateTime Sunsrise { get; set; }
        public DateTime Sunset { get; set; }
        public DateTime Moonrise { get; set; }
        public DateTime Moonset { get; set; }
        public Temperature Temperature { get; set; }
        public FeelsLike FeelsLike { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
        public decimal DewPoint { get; set; }
        public Wind Wind { get; set; }
        public decimal Clouds { get; set; }
        public decimal UVI { get; set; }
        public decimal POP { get; set; }
        public decimal Rain { get; set; }
        public decimal Snow { get; set; }
        public List<Weather> Weather { get; set; } = new List<Weather>();
        public DailyForecast(JToken jToken, Units? unit)
        {
            if (jToken != null)
            {
                this.ForecastedDateTime = DateTimeHelper.ConvertUnixToDateTime(double.Parse(jToken.SelectToken("dt").ToString()));
                this.Sunsrise = DateTimeHelper.ConvertUnixToDateTime(double.Parse(jToken.SelectToken("sunrise").ToString()));
                this.Sunset = DateTimeHelper.ConvertUnixToDateTime(double.Parse(jToken.SelectToken("sunset").ToString()));
                this.Moonrise = DateTimeHelper.ConvertUnixToDateTime(double.Parse(jToken.SelectToken("moonrise").ToString()));
                this.Moonset = DateTimeHelper.ConvertUnixToDateTime(double.Parse(jToken.SelectToken("moonset").ToString()));
                this.Temperature = new Temperature(jToken.SelectToken("temp"), unit);
                this.FeelsLike = new FeelsLike(jToken.SelectToken("feels_like"), unit);
                this.Pressure = decimal.Parse(jToken.SelectToken("pressure").ToString());
                this.Humidity = decimal.Parse(jToken.SelectToken("humidity").ToString());
                this.DewPoint = decimal.Parse(jToken.SelectToken("dew_point").ToString());
                this.Wind = new Wind(jToken);
                this.Clouds = decimal.Parse(jToken.SelectToken("clouds").ToString());
                this.UVI = decimal.Parse(jToken.SelectToken("uvi").ToString());
                this.POP = decimal.Parse(jToken.SelectToken("pop").ToString());
                this.Rain = decimal.Parse(jToken.SelectToken("rain") != null ? jToken.SelectToken("rain").ToString() : "0");
                this.Snow = decimal.Parse(jToken.SelectToken("snow") != null ? jToken.SelectToken("snow").ToString() : "0");

                foreach (JToken weather in jToken.SelectToken("weather"))
                    this.Weather.Add(new Weather(weather));
            }
        }
    }
}
