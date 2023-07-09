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
        private Launcher launcher;
        
        public HotKey Launcher_hotKey;
        public HotKey Screenshot_All_hotKey;
        public HotKey Screenshot_Active_hotKey;

        public MainWindow()
        {
            InitializeComponent();

            launcher = new Launcher();

            Launcher_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Enter);
            Launcher_hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);

            Screenshot_All_hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.ALT, Keys.S);
            Screenshot_All_hotKey.HotKeyPush += new EventHandler(Screenshot_HotKeyPush);
        }

        private void hotKey_HotKeyPush(object sender, EventArgs e)
        {
            if (IsVisible) HideWindow();
            else ShowWindow();
        }

        private void Screenshot_HotKeyPush(object sender, EventArgs e)
        {
            
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public Launcher GetLauncher()
        {
            return launcher;
        }

        private void Console_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if(Console.Text == "setting")
            {
                new Settings().Show();

                HideWindow();

                return;
            }

            launcher.RunProcess(Console.Text);

            HideWindow();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideWindow();
        }
    }
}
