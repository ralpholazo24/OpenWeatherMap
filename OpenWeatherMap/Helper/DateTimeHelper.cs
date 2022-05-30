﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertUnixToDateTime(double unixTime)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(unixTime).ToLocalTime();
        }
    }
}
