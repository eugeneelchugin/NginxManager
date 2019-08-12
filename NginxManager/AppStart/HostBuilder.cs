using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NginxManager.AppStart
{
    public class HostBuilder
    {
        private Action<IConfigurationBuilder> _configureConfig;
        private Action<IServiceCollection> _configureServices;
        private Action<IServiceCollection> _registerExceptionBoundary;

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
            services.AddSingleton(_appConfig);
            services.AddSingleton<Host>();
            if (_registerExceptionBoundary == null)
            {
                _registerExceptionBoundary =
                    srvs => srvs.AddSingleton<IExceptionBoundary>(serviceProvider => null);
            }
            _registerExceptionBoundary(services);
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

        public HostBuilder ConfigureExceptionBoundary<TBoundary>()
            where TBoundary : class, IExceptionBoundary
        {
            _registerExceptionBoundary = services => services.AddSingleton<IExceptionBoundary, TBoundary>();
            return this;
        }
    }
}
