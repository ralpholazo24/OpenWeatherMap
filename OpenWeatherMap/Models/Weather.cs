using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public Weather(JToken jToken)
        {
            this.Id = int.Parse(jToken.SelectToken("id").ToString());
            this.Main = jToken.SelectToken("main").ToString();
            this.Description = jToken.SelectToken("description").ToString();
            this.Icon = jToken.SelectToken("icon").ToString();
        }
    }
}
