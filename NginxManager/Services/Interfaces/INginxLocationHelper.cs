namespace NginxManager.Services.Interfaces
{
    public interface INginxLocationHelper
    {
        string NginxExe { get; }
        string NginxDirectory { get; }
        string NginxConfig { get; }
    }
}