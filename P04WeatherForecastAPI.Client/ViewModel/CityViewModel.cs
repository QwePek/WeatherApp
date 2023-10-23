using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public class CityViewModel
    {
        public string LocalizedName { get; set; }
        public string Key { get; set; }

        public CityViewModel(City city)
        {
            LocalizedName = city.LocalizedName;
            Key = city.Key;
        }
    }
}
