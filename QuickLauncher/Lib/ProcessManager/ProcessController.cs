using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace QuickLauncher.Lib.ProcessManager
{
    public class ProcessController
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MINIMIZE = 6;

        public static Process? GetActiveProcess()
        {
            IntPtr activeWindowHandle = GetForegroundWindow();

            _ = GetWindowThreadProcessId(activeWindowHandle, out uint activeProcessId);

            if (activeProcessId == 0) return null;

            return Process.GetProcessById((int)activeProcessId);
        }

        public static void KillProcess() => GetActiveProcess()?.Kill();

        public static void ProcessMinimized()
        {
            Process? process = GetActiveProcess();
            if (process != null) ShowWindow(process.MainWindowHandle, SW_MINIMIZE);
        }
    }
}
