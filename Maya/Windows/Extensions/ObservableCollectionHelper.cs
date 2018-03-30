using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace Maya.Windows.Extensions
{
    public static class EnumerableHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        [NotNull]
        public static ObservableCollection<TSource> ToObservableCollection<TSource>([NotNull] this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }
    }
}
