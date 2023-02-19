using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Models
{
    public class CurrentWeather
    {
        public int Temperature { get; set; }
        public int WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public int WeatherCode { get; set; }
    }
}
