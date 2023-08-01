using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using QuickLauncher.Lib.ProcessManager;

namespace QuickLauncher.Lib
{
    public partial class Settings : Window
    {
        public ObservableCollection<LProcess> Processes { get; set; }

        public Settings()
        {
            InitializeComponent();

            Processes = new ObservableCollection<LProcess>();

            Load();
        }

        private void Load()
        {
            Launcher launcher = App.GetInstance().GetLauncher();

            foreach (string process_name in launcher.dic.Keys) Processes.Add(new LProcess(process_name, launcher.dic[process_name]));

            dataGrid.ItemsSource = Processes;
        }

        private void AddContent_Click(object sender, RoutedEventArgs e)
        {
            Processes.Add(new LProcess("", ""));
            dataGrid.ItemsSource = Processes;

            SaveContetnt_Click(sender, e);
        }

        private void RemoveContent_Click(object sender, RoutedEventArgs e)
        {
            Processes.Remove((LProcess) dataGrid.SelectedItem);
            dataGrid.ItemsSource = Processes;

            SaveContetnt_Click(sender, e);
        }

        private void SaveContetnt_Click(object sender, RoutedEventArgs e)
        {
            Launcher launcher = App.GetInstance().GetLauncher();
            Dictionary<string, string> LPdic = new();

            foreach (LProcess lProcess in Processes) LPdic.Add(lProcess.Name, lProcess.Path);

            launcher.dic = LPdic;
            launcher.Save();
        }

        public void ShowWindow()
        {
            Show();
            Activate();
        }

        private void Window_Deactivated(object sender, EventArgs e) => Hide();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }

    public class LProcess
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public LProcess(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
