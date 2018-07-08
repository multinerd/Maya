using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Maya.CrashReporter.Model;
using System.Threading.Tasks;

namespace Maya.CrashReporter.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (!Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
                Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
                TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
                System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            }
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ReportCrash(e.Exception);
            Environment.Exit(0);
        }

        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ReportCrash(e.Exception);
            Environment.Exit(0);
        }

        private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ReportCrash(e.Exception);
            Environment.Exit(0);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ReportCrash((Exception)e.ExceptionObject);
            Environment.Exit(0);
        }

        public static void ReportCrash(Exception exception, string developerMessage = "")
        {
            var crashReport = new CrashReport
            {
                FromEmail = "bugreports@email.com",
                ToEmail = "developer@email.com",

                UserName = "bugreports@email.com",
                Password = "",
                SmtpHost = "",
                Port = 587,
                EnableSSL = true,

                IncludeScreenshot = true,
                CaptureType = CaptureType.Screen,
                DeveloperMessage = developerMessage,
                Exception = exception,
            };
            crashReport.Send(exception);
        }
    }
}
