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
using BaseHelper.Services;
using BaseHelper.Views;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace BaseHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        private IHost host;
        public App()
        {

            
            host =Host.CreateDefaultBuilder().ConfigureDefaults(ConfigurationManager.AppSettings.AllKeys)
             .ConfigureServices((context, services) =>
             {
                 ConfigureServices(context.Configuration, services);
             }).Build();
            

            ServiceProvider = host.Services;
            
        }
        
        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<NavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.Configure(nameof(MainWindow), typeof(MainWindow));
                navigationService.Configure(nameof(BaseHelperView), typeof(BaseHelperView));
                navigationService.Configure(nameof(XML_Reader), typeof(XML_Reader));

                return navigationService;
            });

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var test = ServiceProvider.GetService<NavigationService>();
            this.StartupUri = new Uri("Views/BaseHelperView.xaml", UriKind.Relative);
            base.OnStartup(e);
        }

    }
    
}
