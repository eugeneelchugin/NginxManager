namespace NginxManager.AppStart
{
    public interface IExceptionBoundary
    {
        void HandleException(TrayApplicationContext appContext, System.Exception exception);
    }
}