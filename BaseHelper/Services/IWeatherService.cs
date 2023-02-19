using BaseHelper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseHelper.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<City>> GetCities(string cityName);
        Task<CurrentWeather?> GetWeatherFromCity(City city);
    }
}