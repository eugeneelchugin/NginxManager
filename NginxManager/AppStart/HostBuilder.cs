using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NginxManager.AppStart
{
    public class HostBuilder
    {
        private Action<IConfigurationBuilder> _configureConfig;
        private Action<IServiceCollection> _configureServices;

        private IConfiguration _appConfig;
        private IServiceProvider _appServices;

        public Host Build()
        {
            BuildAppConfiguration();
            CreateServiceProvider();
            return _appServices.GetRequiredService<Host>();
        }

        private void CreateServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(_appConfig);
            services.AddSingleton<Host>();
            _configureServices(services);
            _appServices = services.BuildServiceProvider();
        }

        private void BuildAppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            _configureConfig(configBuilder);
            _appConfig = configBuilder.Build();
        }

        public HostBuilder ConfigureAppConfiguration(Action<IConfigurationBuilder> configureConfig)
        {
            _configureConfig = configureConfig;
            return this;
        }

        public HostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            _configureServices = configureServices;
            return this;
        }
    }
}
