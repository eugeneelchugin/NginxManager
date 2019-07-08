using System.IO;
using Newtonsoft.Json;
using NginxManager.Configuration.Interfaces;
using NginxManager.Configuration.Models;

namespace NginxManager.Configuration
{
    public class AppConfig : IAppConfig
    {
        private readonly AppConfigJsonModel _jsonConfig;
        public AppConfig()
        {
            _jsonConfig = JsonConvert.DeserializeObject<AppConfigJsonModel>(File.ReadAllText("./config.json"));
        }

        public string NginxLocation => _jsonConfig.NginxLocation;
    }
}