using System;
using System.Windows;
using QuickLauncher.Lib;
using System.Windows.Forms;
using System.Windows.Input;
using QuickLauncher.Lib.Screenshot;
using QuickLauncher.Lib.ProcessManager;

namespace QuickLauncher
{
    public partial class MainWindow : Window
    {
        private readonly Launcher launcher;

        #region HotKeyインスタンス
        private readonly HotKey Launcher_hotKey;
        private readonly HotKey Screenshot_All_hotKey;
        private readonly HotKey Screenshot_Active_hotKey;
        private readonly HotKey DeleteProcess_HotKey;
        private readonly HotKey MinimizedProcess_HotKey;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            launcher = new Launcher();

            Launcher_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Enter);
            Screenshot_All_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.ALT, Keys.S);
            Screenshot_Active_hotKey = new HotKey(MOD_KEY.ALT, Keys.S);
            DeleteProcess_HotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Delete);
            MinimizedProcess_HotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.ALT, Keys.D);

            Launcher_hotKey.HotKeyPush += new EventHandler(HotKey_HotKeyPush);
            Screenshot_All_hotKey.HotKeyPush += new EventHandler(ScreenshotAll_HotKeyPush);
            Screenshot_Active_hotKey.HotKeyPush += new EventHandler(ScreenshotActive_HotKeyPush);
            DeleteProcess_HotKey.HotKeyPush += new EventHandler(DeleteProsess_HotKeyPush);
            MinimizedProcess_HotKey.HotKeyPush += new EventHandler(MinimizedProcess_HotKeyPush);
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

        public void HotKeyDispose()
        {
            Launcher_hotKey.Dispose();
            Screenshot_All_hotKey.Dispose();
            Screenshot_Active_hotKey.Dispose();
            DeleteProcess_HotKey.Dispose();
            MinimizedProcess_HotKey.Dispose();
        }

        public Launcher GetLauncher() => launcher;

        #region ホットキーコールバック関数
        private void HotKey_HotKeyPush(object? sender, EventArgs e)
        {
            if (IsVisible) HideWindow();
            else ShowWindow();
        }

        private void ScreenshotAll_HotKeyPush(object? sender, EventArgs e) => Screenshot.ScreenShot_All();

        private void ScreenshotActive_HotKeyPush(object? sender, EventArgs e) => Screenshot.ScreenShot_Active();

        private void DeleteProsess_HotKeyPush(object? sender, EventArgs e) => ProcessController.KillProcess();

        private void MinimizedProcess_HotKeyPush(object? sender, EventArgs e) => ProcessController.ProcessMinimized();
        #endregion

        #region ウィンドウイベント関数
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            HideWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e) => HideWindow();

        private void Console_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (Console.Text.StartsWith("/l"))
            {
                launcher.RunProcess(Console.Text.Split(" ")[1]);
                return;
            }

            switch (Console.Text)
            {
                case "/setting":
                case "/s":
                    App.GetSettings().ShowWindow();

                    HideWindow();

                    return;

                case "/delete":
                case "/d":
                    App.AppShutdown();

                    return;
            }

            HideWindow();
        }
        #endregion
    }
}
