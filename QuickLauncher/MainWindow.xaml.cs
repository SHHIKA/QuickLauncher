using System;
using System.Windows;
using QuickLauncher.Lib;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuickLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Launcher launcher;
        private HotKey hotKey;

        public MainWindow()
        {
            InitializeComponent();

            launcher = new Launcher();
            hotKey = new HotKey(MOD_KEY.CONTROL | MOD_KEY.SHIFT, Keys.Enter);
            hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);
        }

        private void hotKey_HotKeyPush(object sender, EventArgs e)
        {
            if (IsVisible) HideWindow();
            else ShowWindow();
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

        public HotKey GetHotKey()
        {
            return hotKey;
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
