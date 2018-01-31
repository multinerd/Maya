using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace Maya.CrashReporter.Test
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            buttonCrash.Click += ButtonCrash_Click;
        }

        private void ButtonCrash_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(ThrowException);
            thread.Start();
        }

        private void ThrowException()
        {
            try
            {
                throw new ArgumentException();
            }
            catch (ArgumentException argumentException)
            {
                const string path = "test.txt";
                try
                {
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException("File Not found when trying to write argument exception to the file", argumentException);
                    }
                }
                catch (Exception exception)
                {
                    App.ReportCrash(exception, "");
                }
            }
        }
    }
}
