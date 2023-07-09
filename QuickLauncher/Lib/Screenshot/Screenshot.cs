using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace QuickLauncher.Lib.Screenshot
{
    public class Screenshot
    {
        private static Bitmap CaptureActiveWindow()
        {
            //アクティブなウィンドウのデバイスコンテキストを取得
            IntPtr hWnd = NativeMethods.GetForegroundWindow();
            IntPtr winDC = NativeMethods.GetWindowDC(hWnd);
            
            //ウィンドウの大きさを取得
            NativeMethods.RECT winRect = new NativeMethods.RECT();
            
            NativeMethods.DwmGetWindowAttribute(
                hWnd,
                NativeMethods.DWMWA_EXTENDED_FRAME_BOUNDS,
                out var bounds,
                Marshal.SizeOf(typeof(NativeMethods.RECT)));

            NativeMethods.GetWindowRect(hWnd, ref winRect);
            
            //Bitmapの作成
            var offsetX = bounds.left - winRect.left;
            var offsetY = bounds.top - winRect.top;
            Bitmap bmp = new Bitmap(bounds.right - bounds.left, bounds.bottom - bounds.top);

            //Graphicsの作成
            using (var g = Graphics.FromImage(bmp))
            {
                //Graphicsのデバイスコンテキストを取得
                IntPtr hDC = g.GetHdc();
                //Bitmapに画像をコピーする
                Console.WriteLine(winRect);
                NativeMethods.BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, winDC, offsetX, offsetY, NativeMethods.SRCCOPY);
                //解放
                g.ReleaseHdc(hDC);
            }
            NativeMethods.ReleaseDC(hWnd, winDC);

            return bmp;
        }

        private static void ScreenShot_Active()
        {
            using (var bmp = CaptureActiveWindow())
            {
                bmp.Save(@"hoge\image.png", ImageFormat.Png);
            }
        }

        private static void ScreenShot_All()
        {
            int left = SystemInformation.VirtualScreen.Left;
            int top = SystemInformation.VirtualScreen.Top;
            int width = SystemInformation.VirtualScreen.Width;
            int hight = SystemInformation.VirtualScreen.Height;

            Rectangle rect = new Rectangle(left, top, width, hight);
            using (var bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);
                }
                bmp.Save(@"hoge\image.png", ImageFormat.Png);
            }
        }
    }
}
