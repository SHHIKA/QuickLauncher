using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace QuickLauncher.Lib.ProcessManager
{
    public class Launcher
    {
        public Dictionary<string, string> dic = new();
        private readonly string filePath = "data.txt";

        public Launcher()
        {
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            StreamReader reader = new(filePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] ss = line.Split("!!");
                dic[ss[0]] = ss[1];
            }
            reader.Close();
        }

        public void RunProcess(string processName)
        {
            if (!dic.ContainsKey(processName))
            {
                MessageBox.Show($"無効なプロセス名です : {processName}");
                return;
            }

            ProcessStartInfo app = new()
            {
                FileName = dic[processName],
                UseShellExecute = true
            };
            Process.Start(app);
        }

        public void Save()
        {
            StreamWriter writer = new(filePath);
            foreach (string write in dic.Keys) writer.WriteLine(write + "!!" + dic[write]);
            writer.Close();
        }
    }
}
