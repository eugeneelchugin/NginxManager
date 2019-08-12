using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace NginxManager.AppStart
{
    public class Host
    {
        private readonly IExceptionBoundary _exceptionBoundary;
        private readonly TrayApplicationContext _appContext;

        public Host(IServiceProvider services)
        {
            _exceptionBoundary = services.GetService<IExceptionBoundary>();
            if (_exceptionBoundary != null)
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            }

            _appContext = services.GetService<TrayApplicationContext>();
        }

        public void Run()
        {
            if (_exceptionBoundary != null)
            {
                Application.ThreadException += (sender, args) => _exceptionBoundary.HandleException(_appContext, args.Exception);
                AppDomain.CurrentDomain.UnhandledException += (sender, args) => _exceptionBoundary.HandleException(_appContext, args.ExceptionObject as Exception);
            }
            Application.Run(_appContext);
        }
    }

    
}