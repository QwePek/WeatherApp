using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public class TemperatureViewModel
    {
        public string Temperature { get; set; }

        public TemperatureViewModel(SingleUnitTemperature temperature)
        {
            Temperature = string.Format("{0} {1}", temperature.Value, temperature.Unit);
        }
    }
}
