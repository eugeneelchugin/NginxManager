using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using NginxManager.AppStart;

namespace NginxManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateHostBuilder().Build().Run();
        }

        private static HostBuilder CreateHostBuilder()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder
                .ConfigureAppConfiguration(config =>
                    {
                        config
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Config.json", optional: false, reloadOnChange: true);
                    }
                )
                .ConfigureServices(CompositionRoot.Configure);
            return hostBuilder;
        }
    }
}
