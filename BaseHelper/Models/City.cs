using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string? Timezone { get; set; }
        public int Population { get; set; }
        public string Admin1 { get; set; } = string.Empty;
        public string Admin2 { get; set; } = string.Empty;
        public string Admin3 { get; set; } = string.Empty;
        public string Admin4 { get; set; } = string.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
