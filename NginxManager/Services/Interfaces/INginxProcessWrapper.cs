namespace NginxManager.Services.Interfaces
{
    public interface INginxProcessWrapper
    {
        void Reload();
        void Stop();
        void Start();
    }
}