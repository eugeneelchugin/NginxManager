using System;
using System.Windows.Forms;
using NginxManager.Resources;
using NginxManager.Resources.Icons;
using NginxManager.Services.Interfaces;

namespace NginxManager
{
    internal class TrayApplicationContext : ApplicationContext
    {
        private readonly INginxProcessWrapper _nginx;
        private NotifyIcon _trayIcon;

        public TrayApplicationContext(INginxProcessWrapper nginx)
        {
            _nginx = nginx;
            InitializeContext();
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

            var stopNginx = new ToolStripMenuItem(
                text: Strings.StopNginxMenuItemText,
                onClick: HandleStopNginxMenuItem,
                image: null
            );

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
                Icon =  Icons.MainIcon,
                ContextMenuStrip = mainContextMenu,
                Visible = true
            };
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
        }

        private void HandleStartNginxMenuItem(object sender, EventArgs e)
        {
            _nginx.Start();
        }

        private void HandleExitMenuItemClick(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}