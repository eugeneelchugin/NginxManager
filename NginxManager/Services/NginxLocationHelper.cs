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
        }


        private string _nginxExe;
        public string NginxExe => _nginxExe ?? (_nginxExe = Path.Combine(_appConfig.NginxLocation, Strings.NginxExe));
    }
}