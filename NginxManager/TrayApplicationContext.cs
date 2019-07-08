using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NginxManager.Resources;
using NginxManager.Resources.Icons;
using NginxManager.Services.Interfaces;

namespace NginxManager
{
    internal class TrayApplicationContext : ApplicationContext
    {
        private readonly INginxLocationHelper _nginxLocationHelper;
        private NotifyIcon _trayIcon;

        public TrayApplicationContext(INginxLocationHelper nginxLocationHelper)
        {
            _nginxLocationHelper = nginxLocationHelper;
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

        private void HandleStopNginxMenuItem(object sender, EventArgs e)
        {
            var procInfo = new ProcessStartInfo(_nginxLocationHelper.NginxExe, "-s stop")
            {
                WorkingDirectory = Path.GetDirectoryName(_nginxLocationHelper.NginxExe)
            };
            Process.Start(procInfo);
        }

        private void HandleStartNginxMenuItem(object sender, EventArgs e)
        {
            //            Process.Start("CMD.EXE", $"{_nginxLocationHelper.NginxExe}");
            var procInfo = new ProcessStartInfo(_nginxLocationHelper.NginxExe)
            {
                WorkingDirectory = Path.GetDirectoryName(_nginxLocationHelper.NginxExe)
            };
            Process.Start(procInfo);
        }

        private void HandleExitMenuItemClick(object sender, EventArgs e)
        {
            ExitThread();
        }
    }
}