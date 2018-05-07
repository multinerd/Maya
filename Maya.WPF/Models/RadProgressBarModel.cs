using System;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

// ReSharper disable All
namespace Multinerd.Telerik.Helpers
{
    public class RadProgressBarModel : ViewModelBase
    {
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(() => Message);
            }
        }

        public float TotalValue
        {
            get { return _totalValue; }
            set
            {
                _totalValue = value;
                OnPropertyChanged(() => TotalValue);
            }
        }

        public float CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                OnPropertyChanged(() => CurrentValue);
            }
        }


        public RadProgressBarModel() { }

        public RadProgressBarModel(float totalValue)
        {
            TotalValue = totalValue;
        }


        public void DoWork(string message, Action callback)
        {
            Message = message;
            callback?.Invoke();
        }

        public void DoWorkIncrement(string message, Action callback, bool sleep = true, int sleeptime = 50)
        {
            Message = message;
            callback?.Invoke();
            CurrentValue++;
            if (sleep) Thread.Sleep(sleeptime);
        }

        public void Complete(string message)
        {
            Message = message;
            CurrentValue = 0;

            Task.Delay(5000).ContinueWith(t => { Message = string.Empty; });
        }


        #region Private Properties

        private string _message;
        private float _totalValue;
        private float _currentValue = 1;

        #endregion

    }
}
