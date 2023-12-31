﻿using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<IpAddressInfo> GetIpAddressInfo(string ipAddress);
        Task<Geoposition> GetGeopositionInfo(string geoPos);
        Task<HourlyForecast> GetForecastFor1Hour(string cityKey);
        Task<HourlyForecast[]> GetForecastFor12Hours(string cityKey);
    }
}
