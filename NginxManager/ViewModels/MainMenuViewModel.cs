using System.Drawing;
using System.Reactive.Subjects;

namespace NginxManager.ViewModels
{
    public class MainMenuViewModel
    {
        public MainMenuViewModel(bool startNginxMenuItemVisible, bool stopNginxMenuItemVisible, Icon trayIcon)
        {
            StartNginxMenuItemVisible = new BehaviorSubject<bool>(startNginxMenuItemVisible);
            StopNginxMenuItemVisible = new BehaviorSubject<bool>(stopNginxMenuItemVisible);
            TrayIcon = new BehaviorSubject<Icon>(trayIcon);
        }

        public BehaviorSubject<bool> StartNginxMenuItemVisible { get; }
        public BehaviorSubject<bool> StopNginxMenuItemVisible { get; }
        public BehaviorSubject<Icon> TrayIcon { get; }
    }
}