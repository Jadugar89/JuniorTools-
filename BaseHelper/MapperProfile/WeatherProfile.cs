using AutoMapper;
using BaseHelper.ModelDtos;
using BaseHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.MapperProfile
{
    public class WeatherProfile:Profile
    {
        public WeatherProfile()
        {
            CreateMap<CityDto, City>()
                .ForMember(x => x.CountryCode, y => y.MapFrom(s => s.Country_Code));
            CreateMap<CurrentWeatherDto, CurrentWeather>();
        }
    }
}
