using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel selectedCity;

        [ObservableProperty]
        private IpAddressViewModel ipAddressView;
        [ObservableProperty]
        private GeolocationViewModel geolocationView;
        [ObservableProperty]
        private ForecastLabelViewModel hourlyForecastLabel;
        [ObservableProperty]
        private TemperatureViewModel hourlyTemperatureView;
        [ObservableProperty]
        private TemperatureViewModel currentTemperatureView;

        [ObservableProperty]
        private bool canShowNextForecast = false;

        public IAccuWeatherService AccuWeatherService { get; set; }
        public ObservableCollection<CityViewModel> Cities { get; set; }

        private int currentHour = 0;
        private List<HourlyForecast>? hourlyForecasts;

        public CityViewModel SelectedCity { get => selectedCity; set
            {
                selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
                LoadFirstForecasts();
            }
        }

        public MainViewModel(IAccuWeatherService accuWeatherService)
        {
            AccuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>();
            HourlyForecastLabel = new ForecastLabelViewModel("Hour", 1);
        }

        [RelayCommand]
        public async Task LoadCities(string locationName)
        {
            var cities = await AccuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(new CityViewModel(city));
        }

        [RelayCommand]
        public async Task ShowNextHourlyForecast(string locationName)
        {
            currentHour++;
            if (currentHour == 12)
            {
                currentHour = 0;
            }
            HourlyForecastLabel = new ForecastLabelViewModel("Hours", currentHour + 1);
            if (hourlyForecasts == null)
            {
                hourlyForecasts = new List<HourlyForecast>(await AccuWeatherService.GetForecastFor12Hours(SelectedCity.Key));
            }
            HourlyTemperatureView = new TemperatureViewModel(hourlyForecasts[currentHour].Temperature);
        }

        [RelayCommand]
        public async Task LoadIpLocation(string ipAddress)
        {
            var ip = await AccuWeatherService.GetIpAddressInfo(ipAddress);
            IpAddressView = new IpAddressViewModel(ip.LocalizedName, ip.AdministrativeArea.LocalizedName);
        }

        [RelayCommand]
        public async Task LoadGeoLocation(string getPos)
        {
            var geo = await AccuWeatherService.GetGeopositionInfo(getPos);
            GeolocationView = new GeolocationViewModel(geo.Country.LocalizedName, geo.Region.LocalizedName);
        }

        private async void LoadWeather()
        {
            if (SelectedCity != null)
            {
                ResetForecasts();
                var temperature = (await AccuWeatherService.GetCurrentConditions(SelectedCity.Key)).Temperature;
                CurrentTemperatureView = new TemperatureViewModel(new SingleUnitTemperature { UnitType = temperature.UnitType, Value = temperature.Imperial.Value });
            }
        }

        private void ResetForecasts()
        {
            hourlyForecasts = null;
            currentHour = 0;
        }

        private async void LoadFirstForecasts()
        {
            if (SelectedCity != null)
            {
                CanShowNextForecast = true;
                SingleUnitTemperature houryTemperature = (await AccuWeatherService.GetForecastFor1Hour(SelectedCity.Key)).Temperature;
                HourlyTemperatureView = new TemperatureViewModel(new SingleUnitTemperature { UnitType = houryTemperature.UnitType, Value = houryTemperature.Value });
            }
        }
    }
}
