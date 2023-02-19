using BaseHelper.Models;
using BaseHelper.Services;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.ViewModels
{
    public class WeatherViewModel : ViewModelBase
    {
		private bool showCityComboBox=false;		
		public bool ShowCityComboBox
        {
			get { return showCityComboBox; }
			set { showCityComboBox = value;
				OnPropertyChanged(nameof(ShowCityComboBox));
			}
		}
		private ObservableCollection<City> cities;
		private readonly IWeatherService weatherService;

		public ObservableCollection<City> Cities
        {
			get { return cities; }
			set { cities = value;
				OnPropertyChanged(nameof(Cities));
			}
		}
		private City selectedCity;

		public City SelectedCity
        {
			get { return selectedCity; }
			set { selectedCity = value;
				GetWeather(value);
				OnPropertyChanged(nameof(SelectedCity));
            }
		}

		private CurrentWeather currentWeather;
		public CurrentWeather CurrentWeather
		{
			get { return currentWeather; }
			set { currentWeather = value;
				OnPropertyChanged(nameof(CurrentWeather));
			}
		}
        private bool showCurrentWeather = false;
        public bool ShowCurrentWeather
        {
            get { return showCurrentWeather; }
            set
            {
                showCurrentWeather = value;
                OnPropertyChanged(nameof(ShowCurrentWeather));
            }
        }
		public IRelayCommand BtnPressedEnter { get; set; }

        public WeatherViewModel(IWeatherService weatherService)
		{
			this.weatherService = weatherService;
			BtnPressedEnter = new RelayCommand<string>(PressedEnter);

        }
		private void PressedEnter(string textCity)
		{
            if (!String.IsNullOrEmpty(textCity))
                GetCities(textCity);
        }

        private async void GetCities(string textCity)
		{
           var citiesFromApi= await weatherService.GetCities(textCity);
			Cities = new ObservableCollection<City>(citiesFromApi);
				if(citiesFromApi.Any())
                {
                    ShowCityComboBox = true;
                    SelectedCity=citiesFromApi.First();	
                }
				else
				{
					ShowCityComboBox = false;
				}		
        }
		private async void GetWeather(City paramCity)
		{
			if(paramCity!=null)
			{
                var weatherFromCity = await weatherService.GetWeatherFromCity(paramCity);
                if (weatherFromCity != null)
                {
                    CurrentWeather = weatherFromCity;
                    ShowCurrentWeather = true;
                }
                else
                {
                    ShowCurrentWeather = false;
                }
            }
			else
			{
				CurrentWeather = new CurrentWeather();
                ShowCurrentWeather = false;
            }

        }
	}
}
