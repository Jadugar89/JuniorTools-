using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.ModelDtos
{
    public class CurrentWeatherDto
    {
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
        public float WindDirection { get;set; }
        public float WeatherCode  { get; set; }
        public string? Time { get; set; }
    }
}
