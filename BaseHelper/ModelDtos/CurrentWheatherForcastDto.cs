using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.ModelDtos
{
    public class CurrentWheatherForcastDto
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Generationtime_ms { get; set; }
        public float Elevation { get; set; }
        public int Utc_Offset_Seconds { get; set; }
        public string? Timezone { get; set; }
        public string? Timezone_Abbreviation { get; set; }
        public CurrentWeatherDto Current_Weather { get; set; }



    }
}
