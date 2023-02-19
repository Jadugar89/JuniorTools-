using AutoMapper;
using BaseHelper.ModelDtos;
using BaseHelper.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMapper mapper;

        public WeatherService(ILogger<WeatherService> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<City>> GetCities(string cityName)
        {
            var httpClient=httpClientFactory.CreateClient("Geocoding");
            var request = $"v1/search?name={cityName}&count=100";
            var response = await httpClient.GetAsync(request);
            logger.LogInformation("Status" + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var citiesDtos = await response.Content.ReadFromJsonAsync<CitiesDTo>();
                    return mapper.Map<IEnumerable<City>>(citiesDtos.Results);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
            else
            {
                logger.LogInformation(await response.Content.ReadAsStringAsync());
            }
            return new List<City>();
        }
        public async Task<CurrentWeather?> GetWeatherFromCity(City city)
        {
            var httpClient = httpClientFactory.CreateClient("OpenMeteo");
            var request = $"v1/forecast?latitude={city.Latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)}&longitude={city.Longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)}&current_weather=1";

            var response = await httpClient.GetAsync(request);
            logger.LogInformation("Status" + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var currentWheatherForcastDto = await response.Content.ReadFromJsonAsync<CurrentWheatherForcastDto>();
                    return mapper.Map<CurrentWeather>(currentWheatherForcastDto.Current_Weather);

                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
            else
            {
                logger.LogInformation(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

    }
}
