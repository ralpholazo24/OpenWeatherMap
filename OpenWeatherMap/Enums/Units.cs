using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenWeatherMap.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Units
    {       
        celsius,
        fahrenheit,
    }
}
