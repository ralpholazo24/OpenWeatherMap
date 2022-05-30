using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenWeatherMap.Helper
{
    public static class StringHelper
    {
        public static bool IsValidCommaSeparatedString(string inputString)
        {
            var regex = new Regex(@"^\d+(?:,\d+)*$");
            return regex.IsMatch(inputString);
        }

    }
}
