using QuickLauncher.Lib;
using System;
using System.Windows;

namespace QuickLauncher
{
    public partial class App : Application
    {
        private static MainWindow? _Instance = null;
        private static Settings? _setting = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var icon = GetResourceStream(new Uri("icon.ico", UriKind.Relative)).Stream;
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click);
            
            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "クイックランチャー",
                ContextMenuStrip = menu
            };
            
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);

            _ = GetInstance();
            _ = GetSettings();
        }

        private void NotifyIcon_Click(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var window = GetInstance();

                if (window.IsVisible) window.HideWindow();
                else GetInstance().ShowWindow();
            }
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            GetInstance().HotKeyDispose();
            Shutdown();
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
