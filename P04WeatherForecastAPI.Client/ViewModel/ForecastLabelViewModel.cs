using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public class ForecastLabelViewModel
    {
        public string Label { get; set; } = "Temperature in next {0} {1}";
        public ForecastLabelViewModel(string unit, int amountOfTime)
        {
            Label = string.Format(Label, amountOfTime, unit);
        }
    }
}
