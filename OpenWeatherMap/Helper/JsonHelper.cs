using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenWeatherMap.Helper
{
    public static class JsonHelper
    {
        public static T JsonFileReader<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<T>(text, options);
        }

    }
}
