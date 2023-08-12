using QuickLauncher.Lib;
using System;
using System.Windows;
using System.Windows.Forms;

namespace QuickLauncher
{
    public partial class App : System.Windows.Application
    {
        private static NotifyIcon? notifyIcon;

        private static MainWindow? _Instance = null;
        private static Settings? _setting = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var icon = GetResourceStream(new ("icon.ico", UriKind.Relative)).Stream;
            var menu = new ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click);
            
            notifyIcon = new NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "クイックランチャー",
                ContextMenuStrip = menu
            };
            
            notifyIcon.MouseClick += new MouseEventHandler(NotifyIcon_Click);

            _ = GetInstance();
            _ = GetSettings();
        }

        private void NotifyIcon_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var window = GetInstance();

                if (window.IsVisible) window.HideWindow();
                else GetInstance().ShowWindow();
            }
        }

        private void Exit_Click(object? sender, EventArgs e) => AppShutdown();

        public static void AppShutdown()
        {
            notifyIcon?.Dispose();
            GetInstance().HotKeyDispose();
            Current.Shutdown();
        }

        public static MainWindow GetInstance()
        {
            _Instance ??= new MainWindow();
            return _Instance;
        }

        public static Settings GetSettings()
        {
            _setting ??= new Settings();
            return _setting;
        }
    }
}
