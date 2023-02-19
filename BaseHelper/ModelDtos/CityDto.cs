using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.ModelDtos
{
    public class CityDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float? Elevation { get; set; }
        public string? Feature_Code { get; set; }
        public string? Country_Code   { get; set; }
        public int? Country_Id { get; set; }
        public string? Country { get; set; }
        public string? Timezone { get; set; }
        public int Population { get; set; }
        public string? Admin1 { get; set; }
        public string? Admin2 { get; set; }
        public string? Admin3 { get; set; }
        public string? Admin4 { get; set; }
        public int Admin1_Id { get; set; }
        public int Admin2_Id { get; set; }
        public int Admin3_Id { get; set; }
        public int Admin4_Id { get; set; }
        IEnumerable<string>? PostCodes { get; set; }

    }
}
