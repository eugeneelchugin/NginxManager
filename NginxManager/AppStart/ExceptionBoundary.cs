using System;

namespace NginxManager.AppStart
{
    public class ExceptionBoundary : IExceptionBoundary
    {
        public ExceptionBoundary()
        {
        }

        public void HandleException(TrayApplicationContext appContext, Exception exception)
        {
            appContext.ExitThread();
        }
    }
}