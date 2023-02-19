using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Xml;
using BaseHelper.Views;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using BaseHelper.ViewModels;
using BaseHelper.ViewModels.Factories;
using BaseHelper.Services;
using NLog.Fluent;
using Microsoft.Extensions.Logging;
using NLog.Web;
using BaseHelper.MapperProfile;

namespace BaseHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;


        public App()
        {
            host = new HostBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                    c.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAutoMapper(typeof(WeatherProfile));
                    services.AddSingleton<BaseHelperViewModel>();
                    services.AddSingleton<XmlReaderViewModel>();
                    services.AddSingleton<WeatherViewModel>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddScoped<IViewModelFactory, ViewModelFactory>();
                    services.AddScoped<IDataBaseService, DataBaseService>();
                    services.AddScoped<IMarkupReaderService, MarkupReaderService>();
                    services.AddScoped<ITableService, TableService>();
                    services.AddScoped<IWeatherService, WeatherService>();

                    services.AddHttpClient("OpenMeteo",client => {
                        client.BaseAddress = new Uri(hostContext.Configuration["OpenMeteo"]);
                    });
                    services.AddHttpClient("Geocoding", client => {
                        client.BaseAddress = new Uri(hostContext.Configuration["Geocoding"]);
                    });

                    services.AddSingleton<CreateViewModel<BaseHelperViewModel>>(services => () => services.GetRequiredService<BaseHelperViewModel>());
                    services.AddSingleton<CreateViewModel<XmlReaderViewModel>>(services => () => services.GetRequiredService<XmlReaderViewModel>());
                    services.AddSingleton<CreateViewModel<WeatherViewModel>>(services => () => services.GetRequiredService<WeatherViewModel>());

                    services.AddSingleton<MainWindowView>(s => new MainWindowView(s.GetRequiredService<MainWindowViewModel>()));
                })
                .ConfigureLogging(logBuilder=>
                {
                    logBuilder.ClearProviders();
                    logBuilder.SetMinimumLevel(LogLevel.Trace);
                    logBuilder.AddNLog("NLog.config");
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            Window window = host.Services.GetRequiredService<MainWindowView>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }

    }

}
