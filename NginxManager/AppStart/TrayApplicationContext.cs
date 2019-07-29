using System;
using System.Windows.Forms;
using NginxManager.Resources;
using NginxManager.Resources.Icons;
using NginxManager.Services.Interfaces;
using NginxManager.ViewModels;

namespace NginxManager.AppStart
{
    public class TrayApplicationContext : ApplicationContext
    {
        private readonly INginxProcessWrapper _nginx;
        private NotifyIcon _trayIcon;

        private AppState _appState;
        private MainMenuViewModel _menuViewModel;

        public TrayApplicationContext(INginxProcessWrapper nginx)
        {
            _nginx = nginx;
            InitializeState();
            InitializeContext();
        }

        private void InitializeState()
        {
            var nginxIsStarted = _nginx.IsStarted;
            _appState = new AppState(nginxIsStarted);
            _menuViewModel = new MainMenuViewModel(
                startNginxMenuItemVisible: !nginxIsStarted,
                stopNginxMenuItemVisible: nginxIsStarted,
                trayIcon: Icons.Nginx
            );

            _appState.NginxIsStarted.Subscribe(isStarted =>_menuViewModel.StartNginxMenuItemVisible.OnNext(!isStarted));
            _appState.NginxIsStarted.Subscribe(isStarted => _menuViewModel.StopNginxMenuItemVisible.OnNext(isStarted));
            _appState.NginxIsStarted.Subscribe(isStarted => _menuViewModel.TrayIcon.OnNext(isStarted ? Icons.NginxStarted : Icons.NginxStoped));
        }

        protected override void ExitThreadCore()
        {
            base.ExitThreadCore();
            _trayIcon.Visible = false;
        }

        private void InitializeContext()
        {
            var container = new System.ComponentModel.Container();

            var startNginx = new ToolStripMenuItem(
                text: Strings.StartNginxMenuItemText,
                onClick: HandleStartNginxMenuItem,
                image: null
            );
            _menuViewModel.StartNginxMenuItemVisible.Subscribe(isVisible => startNginx.Visible = isVisible);

            var stopNginx = new ToolStripMenuItem(
                text: Strings.StopNginxMenuItemText,
                onClick: HandleStopNginxMenuItem,
                image: null
            );
            _menuViewModel.StopNginxMenuItemVisible.Subscribe(isVisible => stopNginx.Visible = isVisible);

            var editConfig = new ToolStripMenuItem(
                text: Strings.EditConfigMenuItemText,
                onClick: HandleEditConfigMenuItem,
                image: null
            );

            var reloadConfig = new ToolStripMenuItem(
                text: Strings.ReloadConfigMenuItemText,
                onClick: HandleReloadConfigMenuItemText,
                image: null
            );

            var exitMenuItem = new ToolStripMenuItem(
                text: Strings.ExitMenuItemText,
                onClick: HandleExitMenuItemClick,
                image: null
            );

            var  mainContextMenu = new ContextMenuStrip
            {
                Items =
                {
                    startNginx,
                    stopNginx,
                    editConfig,
                    reloadConfig,
                    new ToolStripSeparator(),
                    exitMenuItem
                }
            };

            _trayIcon = new NotifyIcon(container)
            {
                Text = Strings.TrayIconText,
                Icon =  Icons.Nginx,
                ContextMenuStrip = mainContextMenu,
                Visible = true
            };
            _menuViewModel.TrayIcon.Subscribe(icon => _trayIcon.Icon = icon);
        }

        private void HandleEditConfigMenuItem(object sender, EventArgs e)
        {
            _nginx.OpenConfig();
        }

        private void HandleReloadConfigMenuItemText(object sender, EventArgs e)
        {
            _nginx.Reload();
        }

        private void HandleStopNginxMenuItem(object sender, EventArgs e)
        {
            _nginx.Stop();
            _appState.NginxIsStarted.OnNext(false);
        }

        private void HandleStartNginxMenuItem(object sender, EventArgs e)
        {
            _nginx.Start();
            _appState.NginxIsStarted.OnNext(true);
        }

        private void HandleExitMenuItemClick(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}