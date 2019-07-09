using System.Diagnostics;
using System.IO;
using NginxManager.Constants;
using NginxManager.Services.Interfaces;

namespace NginxManager.Services
{
    public class NginxProcessWrapper : INginxProcessWrapper
    {
        private readonly INginxLocationHelper _nginxLocationHelper;

        public NginxProcessWrapper(INginxLocationHelper nginxLocationHelper)
        {
            _nginxLocationHelper = nginxLocationHelper;
        }

        private ProcessStartInfo CreateBaseStartInfo()
        {
            return new ProcessStartInfo(_nginxLocationHelper.NginxExe)
            {
                WorkingDirectory = _nginxLocationHelper.NginxDirectory
            };
        }

        private void SendSignal(string signal)
        {
            var startInfo = CreateBaseStartInfo();
            startInfo.Arguments = MakeSignalArgument(signal);
            Process.Start(startInfo);
        }

        private string MakeSignalArgument(string signal) => $"-s {signal}";

        public void Reload() => SendSignal(NginxSignal.Reload);

        public void Stop() => SendSignal(NginxSignal.Stop);

        public void Start()
        {
            var startInfo = CreateBaseStartInfo();
            Process.Start(startInfo);
        }

        public void OpenConfig()
        {
            Process.Start(_nginxLocationHelper.NginxConfig);
        }
    }
}