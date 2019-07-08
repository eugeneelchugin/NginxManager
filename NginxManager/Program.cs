using System;
using System.Windows.Forms;
using Autofac;
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

            var container = CompositionRoot.Container;
            var appContext = container.Resolve<TrayApplicationContext>();
            Application.Run(appContext);
        }
    }
}
