using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

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
