using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickLauncher.Lib
{
    public class Fruit
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public  HotKeyProperty Hotkey { get; set; }
    }

    public class HotKeyProperty
    {
        public MOD_KEY _MOD_KEY { get; set; }
        public Keys _Key { get; set; }
    }
}
