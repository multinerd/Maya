using Prism.Mvvm;

namespace Maya.Prism.Model
{
    /// <summary>
    /// A Model to assist with showing a loading screen.
    /// </summary>
    public class ActivityIndicator : BindableBase
    {
        #region Private Properties

        private bool _isBusy;
        private string _busyMessage;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string BusyMessage
        {
            get => _busyMessage;
            set => SetProperty(ref _busyMessage, value);

        }

        /// <summary>
        /// Shows the activity indicator with a message.
        /// </summary>
        /// <param name="message"> The message to show. </param>
        public void ShowActivityIndicator(string message)
        {
            BusyMessage = message;
            IsBusy = true;
        }

        /// <summary>
        /// Hides the activity indicator.
        /// </summary>
        public void HideActivityIndicator()
        {
            IsBusy = false;
            //BusyMessage = string.Empty;
        }
    }
}
