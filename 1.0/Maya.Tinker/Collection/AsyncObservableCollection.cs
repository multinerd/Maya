using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace Maya.Windows.Collection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAsyncContext
    {
        /// 
        /// Get the context of the creator thread
        /// 
        SynchronizationContext AsynchronizationContext { get; }

        /// 
        /// Test if the current executing thread is the creator thread
        /// 
        bool IsAsyncCreatorThread { get; }

        /// 
        /// Post a call to the specified method on the creator thread
        /// 
        /// Method that is to be called
        /// Method parameter/state
        void AsyncSend(SendOrPostCallback callback, object state);
    }

    /// <summary>
    /// 
    /// </summary>
    public class AsyncContext : IAsyncContext
    {
        private readonly SynchronizationContext _asynchronizationContext;

        /// 
        /// Constructor - Save the context of the creator/current thread
        /// 
        public AsyncContext()
        {
            _asynchronizationContext = SynchronizationContext.Current;
        }

        /// 
        /// Get the context of the creator thread
        /// 
        public SynchronizationContext AsynchronizationContext => _asynchronizationContext;

        /// 
        /// Test if the current executing thread is the creator thread
        /// 
        public bool IsAsyncCreatorThread => SynchronizationContext.Current == AsynchronizationContext;

        /// 
        /// Post a call to the specified method on the creator thread
        /// 
        /// Method that is to be called
        /// Method parameter/state
        public void AsyncSend(SendOrPostCallback callback, object state)
        {
            if (IsAsyncCreatorThread)
                callback(state); // Call the method directly
            else
                AsynchronizationContext.Send(callback, state);  // Post on creator thread
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncObservableCollection<T> : ObservableCollection<T>, IAsyncContext
    {
        private readonly AsyncContext _asyncContext = new AsyncContext();

        #region IAsyncContext Members
        /// <summary>
        /// 
        /// </summary>
        public SynchronizationContext AsynchronizationContext => _asyncContext.AsynchronizationContext;

        /// <summary>
        /// 
        /// </summary>
        public bool IsAsyncCreatorThread => _asyncContext.IsAsyncCreatorThread;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public void AsyncSend(SendOrPostCallback callback, object state)
        {
            _asyncContext.AsyncSend(callback, state);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public AsyncObservableCollection() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public AsyncObservableCollection(IEnumerable<T> list) : base(list) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            AsyncSend(RaiseCollectionChanged, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            AsyncSend(RaisePropertyChanged, e);
        }


        private void RaiseCollectionChanged(object param)
        {
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        private void RaisePropertyChanged(object param)
        {
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}
