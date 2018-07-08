using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows;

namespace Maya.Core.Application
{
    /// <summary>
    /// Use to determine if an application (based on process name) is currently running.
    /// </summary>
    [Obsolete("Switch to the 'Maya.Core' 2.0", true)]
    public static class InstanceChecker
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindowAsync(HandleRef windowHandle, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetForegroundWindow(IntPtr windowHandle);

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
            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);

            _instanceMutex = new Mutex(true, $@"Global\{app.ProcessName}", out var createdNew, securitySettings);
            if (!createdNew)
            {
                MessageBox.Show($"{app.ProcessName} is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _instanceMutex = null;
                if (bringToFront)
                {
                    Process[] objProcesses = Process.GetProcessesByName(app.ProcessName);
                    if (objProcesses.Length > 0)
                    {
                        var hWnd = objProcesses[0].MainWindowHandle;
                        ShowWindowAsync(new HandleRef(null, hWnd), 3);
                        SetForegroundWindow(objProcesses[0].MainWindowHandle);
                    }
                }
                return true;
            }
            return false;
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
