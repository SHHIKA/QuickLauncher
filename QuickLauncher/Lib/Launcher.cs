using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickLauncher.Lib
{
    public class Launcher
    {
        private Dictionary<string, string> dic = new Dictionary<string, string>();
        private string filePath = "data.txt";

        public Launcher()
        {
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            StreamReader reader = new StreamReader(filePath);
            string line;
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

            ProcessStartInfo app = new ProcessStartInfo();
            app.FileName = processName;
            app.UseShellExecute = true;
            Process.Start(app);
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            foreach (string write in dic.Keys)
            {
                writer.WriteLine(write + "!!" + dic[write]);
            }
            writer.Close();
        }
    }
}
