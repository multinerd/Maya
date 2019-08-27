using System;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Windows.Forms;
using Maya.CrashReporter.Helpers;
using Maya.CrashReporter.Properties;

namespace Maya.CrashReporter
{
    internal partial class CrashReport : Form
    {
        private readonly Model.CrashReport _crashReport;

        private ProgressDialog _progressDialog;
        

        #region Form Events

        public CrashReport(Model.CrashReport crashReportObject)
        {
            _crashReport = crashReportObject;

            InitializeComponent();


            Text = $"{_crashReport.ApplicationTitle} {_crashReport.ApplicationVersion} crashed.";

            saveFileDialog.FileName = $"{_crashReport.ApplicationTitle} {_crashReport.ApplicationVersion} Crash Report";
            saveFileDialog.Filter = @"HTML files(*.html)|*.html";

            if (File.Exists(_crashReport.ScreenShot))
            {
                checkBoxIncludeScreenshot.Checked = _crashReport.IncludeScreenshot;
                pictureBoxScreenshot.ImageLocation = _crashReport.ScreenShot;
                pictureBoxScreenshot.Show();
            }

        }

        [Localizable(false)]
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void CrashReportLoad(object sender, EventArgs e)
        {
            textBoxApplicationName.Text = _crashReport.ApplicationTitle;
            textBoxApplicationVersion.Text = _crashReport.ApplicationVersion;
            textBoxException.Text = _crashReport.Exception.GetType().ToString();
            textBoxExceptionMessage.Text = _crashReport.Exception.Message;
            textBoxMessage.Text = _crashReport.Exception.Message;
            textBoxTime.Text = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            textBoxSource.Text = _crashReport.Exception.Source;
            textBoxStackTrace.Text = $@"{_crashReport.Exception.InnerException}\n{_crashReport.Exception.StackTrace}";
        }

        private void CrashReport_Shown(object sender, EventArgs e)
        {
            Activate();
        }

        private void CrashReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!File.Exists(_crashReport.ScreenShot)) return;

            try
            {
                File.Delete(_crashReport.ScreenShot);
            }
            catch (Exception exception)
            {
                Debug.Write(exception.Message);
            }
        }

        #endregion

        #region Control Events

        private void ButtonSendReportClick(object sender, EventArgs e)
        {
            SendEmail();

            _progressDialog = new ProgressDialog();
            _progressDialog.ShowDialog();
        }

        public void SendEmail()
        {
            var fromAddress = new MailAddress(_crashReport.FromEmail);
            var toAddress = new MailAddress(_crashReport.ToEmail);
            var subject = $"{_crashReport.ApplicationTitle} {_crashReport.ApplicationVersion} Crash Report";

            var smtpClient = new SmtpClient
            {
                Host = _crashReport.SmtpHost,
                Port = _crashReport.Port,
                EnableSsl = _crashReport.EnableSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_crashReport.UserName, _crashReport.Password),
            };

            var message = new MailMessage(fromAddress, toAddress)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = GenerateCrashReport(),
            };

            if (File.Exists(_crashReport.ScreenShot) && checkBoxIncludeScreenshot.Checked)
                message.Attachments.Add(new Attachment(_crashReport.ScreenShot));

            smtpClient.SendCompleted += SmtpClientSendCompleted;
            smtpClient.SendAsync(message, "Crash Report");
        }

        private void SmtpClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _progressDialog.Close();
                MessageBox.Show(
                    e.Error.Message,
                    e.Error.ToString(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                _progressDialog.Close();
                MessageBox.Show(
                    string.Format(Resources.SentSuccessMessage, _crashReport.ApplicationTitle, _crashReport.ApplicationVersion),
                    Resources.SendSuccessCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void SaveFileDialogFileOk(object sender, CancelEventArgs e)
        {
            File.WriteAllText(saveFileDialog.FileName, GenerateCrashReport());
        }

        private void LinkLabelViewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(_crashReport.ScreenShot);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(Resources.ScreenshotNotFoundMessage, Resources.ScreenshotNotFoundCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ScreenshotErrorMessage, Resources.ScreenshotErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region HTML Report Generator

        private string GenerateCrashReport()
        {
            return
                $@"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html xmlns='http://www.w3.org/1999/xhtml' xmlns='http://www.w3.org/1999/xhtml' style='--blue: #007bff; --indigo: #6610f2; --purple: #6f42c1; --pink: #e83e8c; --red: #dc3545; --orange: #fd7e14; --yellow: #ffc107; --green: #28a745; --teal: #20c997; --cyan: #17a2b8; --white: #fff; --gray: #868e96; --gray-dark: #343a40; --primary: #007bff; --secondary: #868e96; --success: #28a745; --info: #17a2b8; --warning: #ffc107; --danger: #dc3545; --light: #f8f9fa; --dark: #343a40; --breakpoint-xs: 0; --breakpoint-sm: 576px; --breakpoint-md: 768px; --breakpoint-lg: 992px; --breakpoint-xl: 1200px; --font-family-sans-serif: -apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'; --font-family-monospace: 'SFMono-Regular',Menlo,Monaco,Consolas,'Liberation Mono','Courier New',monospace; font-family: sans-serif; line-height: 1.15; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; -ms-overflow-style: scrollbar; -webkit-tap-highlight-color: transparent;'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no' />
                    <title>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} v{HttpUtility.HtmlEncode(_crashReport.ApplicationVersion)} Crash Report</title>
                </head>

                <body style='font-family: -apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol'; font-size: 1rem; font-weight: 400; line-height: 1.5; color: #212529; text-align: left; background-color: #fff; margin: 0;' bgcolor='#fff'>

                <div class='container-fluid' style='width: 100%; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto;'>
                    <div class='jumbotron' style='margin-bottom: 2rem; border-radius: .3rem; background-color: #e9ecef; padding: 4rem 2rem;'>
                        <hr class='my-4' style='box-sizing: content-box; height: 0; overflow: visible; margin-top: 1.5rem !important; margin-bottom: 1.5rem !important; border-top-style: solid; border-width: 1px 0 0;' />
                        <h1 class='display-3' style='margin-top: 0; margin-bottom: .5rem; font-family: inherit; font-weight: 300; line-height: 1.2; color: inherit; font-size: 4.5rem;'>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} Crashed! </h1>
                        <p style='orphans: 3; widows: 3; margin-top: 0; margin-bottom: 1rem;'>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} (v{HttpUtility.HtmlEncode(_crashReport.ApplicationVersion)}) has crashed. See below for more details.</p>
                    </div>
                    <div class='row' style='display: flex; -ms-flex-wrap: wrap; flex-wrap: wrap; margin-right: -15px; margin-left: -15px;'>
                        <div class='col-12' style='position: relative; width: 100%; min-height: 1px; padding-right: 15px; padding-left: 15px; -ms-flex: 0 0 100%; flex: 0 0 100%; max-width: 100%;'>
                            <hr class='my-4' style='box-sizing: content-box; height: 0; overflow: visible; margin-top: 1.5rem !important; margin-bottom: 1.5rem !important; border-top-style: solid; border-width: 1px 0 0;' />
                            <h3 style='orphans: 3; widows: 3; page-break-after: avoid; margin-top: 0; margin-bottom: .5rem; font-family: inherit; font-weight: 500; line-height: 1.2; color: inherit; font-size: 1.75rem;'>Details</h3>
                            <table class='table table-bordered' style='border-collapse: collapse !important; width: 100%; max-width: 100%; margin-bottom: 1rem; background-color: transparent; border: 1px solid #e9ecef;' bgcolor='transparent'>
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Windows Version</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(OSHelper.GetWindowsVersion())}</td>
                    
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Username</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(UserPrincipal.Current.UserPrincipalName)}</td>
                                </tr>

                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>CLR Version</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(Environment.Version.ToString())}</td>
                    
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Display Name</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(UserPrincipal.Current.DisplayName)}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Developer Message</th>
                                    <td colspan='4' style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(_crashReport.DeveloperMessage.Trim())}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>User's Comment</th>
                                    <td colspan='4' style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(textBoxUserMessage.Text.Trim())}</td>
                                </tr>
                            </table>
                        </div>
        
                        <div class='col-12' style='position: relative; width: 100%; min-height: 1px; padding-right: 15px; padding-left: 15px; -ms-flex: 0 0 100%; flex: 0 0 100%; max-width: 100%;'>
                            <hr class='my-4' style='box-sizing: content-box; height: 0; overflow: visible; margin-top: 1.5rem !important; margin-bottom: 1.5rem !important; border-top-style: solid; border-width: 1px 0 0;' />
                            <h3 style='orphans: 3; widows: 3; page-break-after: avoid; margin-top: 0; margin-bottom: .5rem; font-family: inherit; font-weight: 500; line-height: 1.2; color: inherit; font-size: 1.75rem;'>Exception</h3>
                            {GenerateExceptionReport(_crashReport.Exception)}
                        </div>
                    </div>
                </div>
                </body>
                </html>";

            var htmlString = string.Empty;

            htmlString += $@"
            <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
            <html xmlns='http://www.w3.org/1999/xhtml'>
                <head>
                    <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>
                    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>
                    <title>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} v{HttpUtility.HtmlEncode(_crashReport.ApplicationVersion)} Crash Report</title>
                    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css' integrity='sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb' crossorigin='anonymous'>
                </head>
                <body>
                <div class='container-fluid'>
                    <div class='jumbotron'>
                        <h1 class='display-3'>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} Crashed! </h1>
                        <hr class='my-4'>
                        <p>{HttpUtility.HtmlEncode(_crashReport.ApplicationTitle)} (v{HttpUtility.HtmlEncode(_crashReport.ApplicationVersion)}) has crashed. See below for more details.</p>
                    </div>
                    <div class='row'>
                        <div class='col-12'>
                            <table class='table table-bordered'>
                                <tr>
                                    <th class='bg-dark text-white'>Windows Version</th>
                                    <td>{HttpUtility.HtmlEncode(OSHelper.GetWindowsVersion())}</td>

                                    <th class='bg-dark text-white'>Username</th>
                                    <td>{HttpUtility.HtmlEncode(UserPrincipal.Current.UserPrincipalName)}</td>
                                </tr>

                                <tr>
                                    <th class='bg-dark text-white'>CLR Version</th>
                                    <td>{HttpUtility.HtmlEncode(Environment.Version.ToString())}</td>

                                    <th class='bg-dark text-white'>Display Name</th>
                                    <td>{HttpUtility.HtmlEncode(UserPrincipal.Current.DisplayName)}</td>
                                </tr>

                                <tr>
                                    <th class='bg-dark text-white'>Developer Message</th>
                                    <td colspan='4'>{HttpUtility.HtmlEncode(_crashReport.DeveloperMessage.Trim())}</td>
                                </tr>

                                <tr>
                                    <th class='bg-dark text-white'>User's Comment</th>
                                    <td colspan='4'>{HttpUtility.HtmlEncode(textBoxUserMessage.Text.Trim())}</td>
                                </tr>
                            </table>
                        </div>

                        <div class='col-12'>
                            <h3>Exception</h3>
                            {GenerateExceptionReport(_crashReport.Exception)}
                        </div>
                    </div>
                </div>
                </body>
                </html>";

            return htmlString;
        }

        private string GenerateExceptionReport(Exception e)
        {
            if (e == null)
                return string.Empty;

            return
                $@"<table class='table table-bordered' style='border-collapse: collapse !important; width: 100%; max-width: 100%; margin-bottom: 1rem; background-color: transparent; border: 1px solid #e9ecef;' bgcolor='transparent'>
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Type</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(e.GetType().ToString())}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Source</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(e.Source ?? "No source")}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Message</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(e.Message)}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Stacktrace</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{HttpUtility.HtmlEncode(e.StackTrace ?? "No stack trace").Replace("\r\n", "<br/>")}</td>
                                </tr>
                
                                <tr style='page-break-inside: avoid; box-sizing: border-box;'>
                                    <th style='background-color: #fff !important; text-align: inherit; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' align='inherit' bgcolor='#fff !important' valign='top'>Inner Exception</th>
                                    <td style='background-color: #fff !important; vertical-align: top; padding: .75rem; border: 1px solid #ddd;' bgcolor='#fff !important' valign='top'>{GenerateExceptionReport(e.InnerException)}</td>
                                </tr>
                            </table>";

            return $@"<table class='table table-bordered'>
                        <tr>
                            <th class='bg-dark text-white'>Type</th>
                            <td>{HttpUtility.HtmlEncode(e.GetType().ToString())}</td>
                        </tr>

                        <tr>
                            <th class='bg-dark text-white'>Source</th>
                            <td>{HttpUtility.HtmlEncode(e.Source ?? "No source")}</td>
                        </tr>

                        <tr>
                            <th class='bg-dark text-white'>Message</th>
                            <td>{HttpUtility.HtmlEncode(e.Message)}</td>
                        </tr>

                        <tr>
                            <th class='bg-dark text-white'>Stacktrace</th>
                            <td>{HttpUtility.HtmlEncode(e.StackTrace ?? "No stack trace").Replace("\r\n", "<br/>")}</td>
                        </tr>

                        <tr>
                            <th class='bg-dark text-white'>Inner Exception</th>
                            <td>{GenerateExceptionReport(e.InnerException)}</td>
                        </tr>
                    </table>";

        }

        #endregion

    }
}
