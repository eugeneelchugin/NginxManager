using System.IO;
using NginxManager.Configuration.Interfaces;
using NginxManager.Resources;
using NginxManager.Services.Interfaces;

namespace NginxManager.Services
{
    public class NginxLocationHelper : INginxLocationHelper
    {
        private readonly IAppConfig _appConfig;

        public NginxLocationHelper(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            SetNginxLocation(appConfig.NginxLocation);
        }

        private void SetNginxLocation(string nginxLocation)
        {
            NginxDirectory = nginxLocation;
            NginxExe = Path.Combine(nginxLocation, Strings.NginxExe);
            NginxConfig = Path.Combine(nginxLocation, Strings.RelativeConfigPath);
        }

        public string NginxExe { get; private set; }
        public string NginxDirectory { get; private set; }
        public string NginxConfig { get; private set; }
    }
}