using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QuickLauncher.Lib
{
    public class HotKey : IDisposable
    {
        private readonly HotKeyForm form;

        public event EventHandler? HotKeyPush;

        public HotKey(MOD_KEY modKey, Keys key) => form = new HotKeyForm(modKey, key, RaiseHotKeyPush);

        private void RaiseHotKeyPush() => HotKeyPush?.Invoke(this, EventArgs.Empty);

        public void Dispose() => form.Dispose();

        private class HotKeyForm : Form
        {
            [DllImport("user32.dll")]
            extern static int RegisterHotKey(IntPtr HWnd, int ID, MOD_KEY MOD_KEY, Keys KEY);

            [DllImport("user32.dll")]
            extern static int UnregisterHotKey(IntPtr HWnd, int ID);

            const int WM_HOTKEY = 0x0312;
            readonly int id;
            readonly ThreadStart proc;

            public HotKeyForm(MOD_KEY modKey, Keys key, ThreadStart proc)
            {
                this.proc = proc;
                for (int i = 0x0000; i <= 0xbfff; i++)
                {
                    if (RegisterHotKey(Handle, i, modKey, key) != 0)
                    {
                        id = i;
                        break;
                    }
                }
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_HOTKEY && (int)m.WParam == id) proc();
            }

            protected override void Dispose(bool disposing)
            {
                _ = UnregisterHotKey(Handle, id);
                base.Dispose(disposing);
            }
        }
    }

    public enum MOD_KEY : int
    {
        ALT = 0x0001,
        CONTROL = 0x0002,
        SHIFT = 0x0004,
    }
}
