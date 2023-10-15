﻿using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccuWeatherService accuWeatherService;
        public MainWindow()
        {
            InitializeComponent();
            accuWeatherService = new AccuWeatherService();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            City[] cities= await accuWeatherService.GetLocations(txtCity.Text);
            //IpAddressInfo ipInfo = await accuWeatherService.GetIpAddressInfo(txtCity.Text);
            //Console.Write(ipInfo.EnglishName + " - " + ipInfo.AdministrativeArea.LocalizedName + " - " + ipInfo.Country.LocalizedName + " - " +
              //  ipInfo.Region.LocalizedName);

            // standardowy sposób dodawania elementów
            //lbData.Items.Clear();
            //foreach (var c in cities)
            //    lbData.Items.Add(c.LocalizedName);

            // teraz musimy skorzystac z bindowania danych bo chcemy w naszej kontrolce przechowywac takze id miasta 
            lbData.ItemsSource = cities;
        }

        private async void btnSearchIpAddr_Click(object sender, RoutedEventArgs e)
        {
            IpAddressInfo ipInfo = await accuWeatherService.GetIpAddressInfo(txtCity.Text);
            if (ipInfo != null)
            {
                lblIpAddrCityName.Content = ipInfo.LocalizedName ;
                lblIpAddrRegionName.Content = ipInfo.AdministrativeArea.LocalizedName;
            }
        }

        private async void btnSearchGeoPos_Click(object sender, RoutedEventArgs e)
        {
            Geoposition geoPos = await accuWeatherService.GetGeopositionInfo(txtGeoPos.Text);
            if (geoPos != null)
            {
                lblGeoPosCountryName.Content = geoPos.Country.LocalizedName;
                lblGeoPosRegionName.Content = geoPos.Region.LocalizedName;
            }
        }

        private async void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCity= (City)lbData.SelectedItem;
            if(selectedCity != null)
            {
                var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                lblCityName.Content = selectedCity.LocalizedName;
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValue.Content = Convert.ToString(tempValue);
                lbData.ItemsSource = null;
            }
        }
    }
}
