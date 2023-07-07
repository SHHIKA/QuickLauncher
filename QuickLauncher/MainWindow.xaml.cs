using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using QuickLauncher.Lib;
using System.Windows.Forms;

namespace QuickLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HotKey hotKey;

        public MainWindow()
        {
            InitializeComponent();

            hotKey = new HotKey(MOD_KEY.ALT, Keys.F);
            hotKey.HotKeyPush += new EventHandler(hotKey_HotKeyPush);
        }

        void hotKey_HotKeyPush(object sender, EventArgs e)
        {
            label.Content = "押された";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
