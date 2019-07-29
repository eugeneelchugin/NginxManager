using System;
using Microsoft.Extensions.Configuration;
using NginxManager.Configuration.Interfaces;
using NginxManager.Configuration.Models;

namespace NginxManager.Configuration
{
    public class AppConfig : IAppConfig
    {
        private readonly IConfiguration _appConfig;

        private readonly AppConfigModel _configModel;
        public AppConfig(IConfiguration appConfig)
        {
            _appConfig = appConfig;
            _configModel = new AppConfigModel();
            BindConfig();
        }

        private void BindConfig()
        {
            _appConfig.Bind(_configModel);
            _appConfig.GetReloadToken().RegisterChangeCallback(obj =>
            {
                BindConfig();
            }, null);
        }

        public string NginxLocation => _configModel.NginxLocation;
    }
}