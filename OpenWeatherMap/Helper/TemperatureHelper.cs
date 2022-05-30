using OpenWeatherMap.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWeatherMap.Helper
{
    public static class TemperatureHelper
    {
		public static double ConvertToFahrenheit(double kelvin)
		{
			return Math.Round(((9.0 / 5.0) * (kelvin - 273.15)) + 32, 3);
		}

		public static double ConvertToCelsius(double kelvin)
		{
			return Math.Round(kelvin - 273.15, 3);
		}


		public static double ConvertKelvin(double kelvin, Units? unit)
		{
			switch (unit)
			{
				case Units.celsius:
					return ConvertToCelsius(kelvin);
				case Units.fahrenheit:
					return ConvertToFahrenheit(kelvin);
				default:
					return kelvin;
			}

			
		}

	}
}
