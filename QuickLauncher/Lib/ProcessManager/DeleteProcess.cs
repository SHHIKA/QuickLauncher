using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace QuickLauncher.Lib.ProcessManager
{
    public class DeleteProcess
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static Process? GetActiveProcess()
        {
            IntPtr activeWindowHandle = GetForegroundWindow();

            _ = GetWindowThreadProcessId(activeWindowHandle, out uint activeProcessId);

            if (activeProcessId == 0) return null;

            return Process.GetProcessById((int)activeProcessId);
        }

        public static void KillProcess() => GetActiveProcess()?.Kill();
    }
}
