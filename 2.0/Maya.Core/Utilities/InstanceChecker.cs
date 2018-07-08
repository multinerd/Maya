using JetBrains.Annotations;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Maya.Core.Utilities
{
    /// <summary>
    /// Use to determine if an application (based on process name) is currently running.
    /// </summary>
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class InstanceChecker
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindowAsync(HandleRef windowHandle, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr windowHandle);

        private static Mutex _instanceMutex;

        /// <summary>
        /// Use to check if another instance of the current application is running. <para />
        /// WARNING: USE ONLY IN WPF/WINFORMS. The <see cref="T:System.Windows.MessageBox" /> will be shown and your
        /// web server will be waiting (hung) until someone responds to the <see cref="T:System.Windows.MessageBox" /> on your web server.
        /// </summary>
        /// <example>
        /// protected override void OnStartup(StartupEventArgs e)
        /// {
        ///     base.OnStartup(e);
        ///     if (InstanceChecker.IsAppRunning(Process.GetCurrentProcess(), true))
        ///         Current.Shutdown(0);
        ///
        ///     new MainWindow().Show();
        /// }
        /// </example>
        public static bool IsAppRunning(Process app, bool bringToFront)
        {
            _instanceMutex = new Mutex(true, $@"Global\{app.ProcessName}", out var createdNew);
            if (createdNew) return false;

            MessageBox.Show($"{app.ProcessName} is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            _instanceMutex = null;

            if (bringToFront)
            {
                var objProcesses = Process.GetProcessesByName(app.ProcessName);
                if (objProcesses.Length > 0)
                {
                    var hWnd = objProcesses[0].MainWindowHandle;
                    ShowWindowAsync(new HandleRef(null, hWnd), 3);
                    SetForegroundWindow(objProcesses[0].MainWindowHandle);
                }
            }
            return true;
        }


        /// <summary> Use on OnExit override </summary>
        /// <example>
        /// protected override void OnExit(ExitEventArgs e)
        /// {
        ///     InstanceChecker.Cleanup();
        ///     base.OnExit(e);
        /// }
        /// </example>
        public static void Cleanup()
        {
            _instanceMutex?.ReleaseMutex();
        }
    }
}
