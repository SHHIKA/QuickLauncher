using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace QuickLauncher.Lib
{
    /// <summary>
    /// settings.xaml の相互作用ロジック
    /// </summary>
    public partial class Settings : Window
    {
        public ObservableCollection<LProcess> processes { get; set; } = null;

        public Settings()
        {
            InitializeComponent();

            Load();
        }

        private void Load()
        {
            Launcher launcher = App.GetInstance().GetLauncher();

            processes = new ObservableCollection<LProcess>();

            foreach (string process_name in launcher.dic.Keys)
            {
                processes.Add(new LProcess(process_name, launcher.dic[process_name]));
            }

            dataGrid.ItemsSource = processes;
        }

        private void AddContent_Click(object sender, RoutedEventArgs e)
        {
            processes.Add(new LProcess("", ""));
            dataGrid.ItemsSource = processes;

            SaveContetnt_Click(sender, e);
        }

        private void RemoveContent_Click(object sender, RoutedEventArgs e)
        {
            processes.Remove((LProcess) dataGrid.SelectedItem);
            dataGrid.ItemsSource = processes;

            SaveContetnt_Click(sender, e);
        }

        private void SaveContetnt_Click(object sender, RoutedEventArgs e)
        {
            Launcher launcher = App.GetInstance().GetLauncher();
            Dictionary<string, string> LPdic = new Dictionary<string, string>();

            foreach (LProcess lProcess in processes)
            {
                LPdic.Add(lProcess.Name, lProcess.Path);
            }

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
