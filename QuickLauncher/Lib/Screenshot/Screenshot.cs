using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuickLauncher.Lib.Screenshot
{
    public class Screenshot
    {
        private static Bitmap CaptureActiveWindow()
        {
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            IntPtr winDC = NativeMethods.GetWindowDC(hWnd);

            NativeMethods.RECT winRect = new();

            _ = NativeMethods.DwmGetWindowAttribute(
                hWnd,
                NativeMethods.DWMWA_EXTENDED_FRAME_BOUNDS,
                out var bounds,
                Marshal.SizeOf(typeof(NativeMethods.RECT)));

            _ = NativeMethods.GetWindowRect(hWnd, ref winRect);

            var offsetX = bounds.left - winRect.left;
            var offsetY = bounds.top - winRect.top;
            
            Bitmap bmp = new(bounds.right - bounds.left, bounds.bottom - bounds.top);

            using (var g = Graphics.FromImage(bmp))
            {
                IntPtr hDC = g.GetHdc();

                _ = NativeMethods.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, winDC, offsetX, offsetY, NativeMethods.SRCCOPY);
                g.ReleaseHdc(hDC);
            }
            _ = NativeMethods.ReleaseDC(hWnd, winDC);

            return bmp;
        }

        public static void ScreenShot_Active()
        {
            using var bmp = CaptureActiveWindow();
            bmp.Save(SaveFilePath(), ImageFormat.Png);
        }

        public static void ScreenShot_All()
        {
            int left = SystemInformation.VirtualScreen.Left;
            int top = SystemInformation.VirtualScreen.Top;
            int width = SystemInformation.VirtualScreen.Width;
            int hight = SystemInformation.VirtualScreen.Height;

            Rectangle rect = new(left, top, width, hight);
            using var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp)) g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
            bmp.Save(SaveFilePath(), ImageFormat.Png);
        }

        private static string SaveFilePath()
        {
            string UserPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (!Directory.Exists(@$"{UserPictures}\Screenshots")) Directory.CreateDirectory(@$"{UserPictures}\Screenshots");

            return @$"{UserPictures}\Screenshots\{time}.png";
        }
    }
}
