using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public class GeolocationViewModel
    {
        public string Label { get; set; } = "Country is {0} and region is {1}";
        public GeolocationViewModel(string country, string region)
        {
            Label = string.Format(Label, country, region);
        }
    }
}
