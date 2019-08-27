using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Maya.CrashReporter.Helpers;

namespace Maya.CrashReporter.Model
{
    /// <summary>
    /// Set SMTP server details and receiver email fields of this class instance to send crash reports directly in your inbox.
    /// </summary>
    /// <example>
    /// protected override void OnStartup(StartupEventArgs e)
    /// {
    ///     base.OnStartup(e);
    /// 
    ///     if (!Debugger.IsAttached)
    ///     {
    ///         AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    ///         Application.Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
    ///         TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    ///         System.Windows.Forms.Application.ThreadException += Application_ThreadException;
    ///     }
    /// }
    /// private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
    /// {
    ///     ReportCrash(e.Exception);
    ///     Environment.Exit(0);
    /// }
    /// 
    /// private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    /// {
    ///     ReportCrash(e.Exception);
    ///     Environment.Exit(0);
    /// }
    /// 
    /// private void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    /// {
    ///     ReportCrash(e.Exception);
    ///     Environment.Exit(0);
    /// }
    /// 
    /// private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    /// {
    ///     ReportCrash((Exception)e.ExceptionObject);
    ///     Environment.Exit(0);
    /// }
    /// 
    /// public static void ReportCrash(Exception exception, string developerMessage = "")
    /// {
    ///     var crashReport = new CrashReport
    ///     {
    ///         FromEmail = SENDER_EMAIL,
    ///         ToEmail = RECIEVER_EMAIL,
    /// 
    ///         UserName = SENDER_EMAIL,
    ///         Password = SENDER_PASSWORD",
    ///         SmtpHost = SENDER_SMTP_HOST,
    ///         Port = SENDER_PORT,
    ///         EnableSSL = true,
    /// 
    ///         UserEmailRequired = false,
    /// 
    ///         IncludeScreenshot = true,
    ///         CaptureType = CaptureType.Screen,
    ///         DeveloperMessage = developerMessage,
    ///         Exception = exception,
    ///     };
    ///     crashReport.Send(exception);
    /// }
    /// </example>
    public class CrashReport
    {
        /// <summary>
        /// Gets or Sets name or IP address of the Host used for SMTP transactions.
        /// </summary>
        public string SmtpHost;

        /// <summary>
        /// Specify whether the SMTP client uses the Secure Socket Layer (SSL) to encrypt the connection.
        /// </summary>
        public bool EnableSSL;

        /// <summary>
        /// Gets or Sets the port used for SMTP transactions.
        /// </summary>
        public int Port = 25;

        /// <summary>
        /// Gets or Sets the username used for SMTP transactions.
        /// </summary>
        public string UserName = "";

        /// <summary>
        /// Gets or Sets the password used for SMTP transactions. 
        /// </summary>
        public string Password = "";

        /// <summary>
        /// Gets or Sets email address where you want to receive crash reports.
        /// </summary>
        public string ToEmail;

        /// <summary>
        /// Gets or Sets email address used by crash reporter if user don't provide her email address.
        /// </summary>
        public string FromEmail;

        /// <summary>
        /// Gets or Sets exception that occur during application execution.
        /// </summary>
        public Exception Exception;

        /// <summary>
        /// Specify whether CrashReporter.NET should take screen shot of whole screen or just the application window.
        /// </summary>
        public CaptureType CaptureType = CaptureType.Screen;

        /// <summary>
        /// Gets or Sets custom message developer wants to send. It can be something like value of variables or other details you want to send.
        /// </summary>
        public string DeveloperMessage = "";


        /// <summary>
        /// Gets or Sets "Include screenshot" start value.
        /// </summary>
        public bool IncludeScreenshot = true;



        internal string ApplicationTitle;

        internal string ApplicationVersion;

        internal string ScreenShot;


        private void ProcessException(Exception exception)
        {
            Exception = exception;

            if (string.IsNullOrEmpty(ToEmail) || (string.IsNullOrEmpty(FromEmail) || string.IsNullOrEmpty(SmtpHost)))
                return;

            if (!Application.MessageLoop)
                Application.EnableVisualStyles();

            // Setup Application Name and Version
            var mainAssembly = Assembly.GetEntryAssembly();
            string appTitle = null;

            var attributes = mainAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attributes.Length > 0)
                appTitle = ((AssemblyTitleAttribute)attributes[0]).Title;

            ApplicationTitle = string.IsNullOrEmpty(appTitle) ? mainAssembly.GetName().Name : appTitle;
            ApplicationVersion = ApplicationDeployment.IsNetworkDeployed
                ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                : mainAssembly.GetName().Version.ToString();

            // Capture Screenshot
            try
            {
                ScreenShot = $@"{Path.GetTempPath()}\{ApplicationTitle} Crash Screenshot.png";

                switch (CaptureType)
                {
                    case CaptureType.Screen:
                        CaptureScreenshot.CaptureScreen(ScreenShot, ImageFormat.Png);
                        break;

                    case CaptureType.Window:
                        CaptureScreenshot.CaptureWindow(ScreenShot, ImageFormat.Png);
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
            }
        }

        /// <summary>
        /// Sends exception report directly to receiver email address provided in ToEmail.
        /// </summary>
        /// <param name="exception">Exception object that contains details of the exception.</param>
        public void Send(Exception exception)
        {
            ProcessException(exception);

            // Present Dialog
            CrashReporter.CrashReport crashReport = new CrashReporter.CrashReport(this);
            if (Thread.CurrentThread.GetApartmentState().Equals(ApartmentState.MTA))
            {
                var thread = new Thread(() => crashReport.ShowDialog()) { IsBackground = false };
                thread.CurrentCulture = thread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
            else
            {
                crashReport.ShowDialog();
            }
        }

        /// <summary>
        /// Sends exception report directly to receiver email address provided in ToEmail.
        /// </summary>
        /// <param name="exception">Exception object that contains details of the exception.</param>
        public void SendQuietly(Exception exception)
        {
            ProcessException(exception);

            CrashReporter.CrashReport crashReport = new CrashReporter.CrashReport(this);
            crashReport.SendEmail();
        }

    }
}