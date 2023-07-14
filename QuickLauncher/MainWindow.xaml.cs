using System;
using System.Windows;
using QuickLauncher.Lib;
using System.Windows.Forms;
using System.Windows.Input;
using QuickLauncher.Lib.Screenshot;

namespace QuickLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Launcher launcher;
        
        public HotKey Launcher_hotKey;
        public HotKey Screenshot_All_hotKey;
        public HotKey Screenshot_Active_hotKey;

        public MainWindow()
        {
            InitializeComponent();

            launcher = new Launcher();

            Launcher_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Enter);
            Launcher_hotKey.HotKeyPush += new EventHandler(HotKey_HotKeyPush);

            Screenshot_All_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.ALT, Keys.S);
            Screenshot_All_hotKey.HotKeyPush += new EventHandler(ScreenshotAll_HotKeyPush);

            Screenshot_Active_hotKey = new HotKey(MOD_KEY.ALT, Keys.S);
            Screenshot_Active_hotKey.HotKeyPush += new EventHandler(ScreenshotActive_HotKeyPush);
        }

        public void ShowWindow()
        {
            Show();
            Activate();
            Console.Focus();
        }

        public void HideWindow()
        {
            Console.Text = "";
            Hide();
        }

        public Launcher GetLauncher()
        {
            return launcher;
        }

        private void HotKey_HotKeyPush(object? sender, EventArgs e)
        {
            if (IsVisible) HideWindow();
            else ShowWindow();
        }

        private void ScreenshotAll_HotKeyPush(object? sender, EventArgs e) => Screenshot.ScreenShot_All();

        private void ScreenshotActive_HotKeyPush(object? sender, EventArgs e) => Screenshot.ScreenShot_Active();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            HideWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e) => HideWindow();

        private void Console_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            switch (Console.Text)
            {
                case "/setting":
                case "/set":
                    new Settings().Show();

                    HideWindow();

                    return;

                case "/delete":
                case "/out":
                    Launcher_hotKey.Dispose();
                    Screenshot_All_hotKey.Dispose();
                    System.Windows.Application.Current.Shutdown();

                    return;
            }

            launcher.RunProcess(Console.Text);

            HideWindow();
        }
    }
}
