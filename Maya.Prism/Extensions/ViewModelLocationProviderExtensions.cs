using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace Maya.Prism.Extensions
{
    public static class ViewModelLocationProviderExtensions
    {
        /// <summary>
        /// Bind a ViewModel to a View by name.
        /// https://stackoverflow.com/a/37045539/5434784
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        public static void RegisterViewModelForView<TView, TViewModel>(this IUnityContainer container)
        {
            ViewModelLocationProvider.Register(typeof(TView).ToString(), () => container.Resolve<TViewModel>());
        }
    }
}
