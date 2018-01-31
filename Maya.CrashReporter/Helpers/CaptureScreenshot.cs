using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Maya.CrashReporter.Helpers
{
    internal static class CaptureScreenshot
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;
        }


        public static void CaptureScreen(string location, ImageFormat imageFormat)
        {
            var screenLeft = SystemInformation.VirtualScreen.Left;
            var screenTop = SystemInformation.VirtualScreen.Top;
            var screenWidth = SystemInformation.VirtualScreen.Width;
            var screenHeight = SystemInformation.VirtualScreen.Height;

            var bounds = new Rectangle(screenLeft, screenTop, screenWidth, screenHeight);
            Capture(bounds, location, imageFormat);
        }


        public static void CaptureWindow(string location, ImageFormat imageFormat)
        {
            var foregroundWindowHandle = GetForegroundWindow();
            var rect = new Rect();
            GetWindowRect(foregroundWindowHandle, ref rect);

            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            Capture(bounds, location, imageFormat);
        }


        private static void Capture(Rectangle bounds, string location, ImageFormat imageFormat)
        {
            using (var bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save(location, imageFormat);
            }
        }
    }
}