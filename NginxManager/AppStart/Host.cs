using System.Windows.Forms;

namespace NginxManager.AppStart
{
    public class Host
    {
        private readonly TrayApplicationContext _appContext;

        public Host(TrayApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public void Run()
        {
            Application.Run(_appContext);
        }
    }
}