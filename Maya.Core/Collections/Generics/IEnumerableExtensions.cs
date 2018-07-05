using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maya.Core.Collections.Generics
{
    /// <summary>
    /// A collection of extensions that target <see cref="IEnumerable"/>
    /// </summary>
    [UsedImplicitly]
    public static class IEnumerableExtensions
    {
        /// <summary>Creates a <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>A <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> that contains elements from the input sequence.</returns>
        [UsedImplicitly]
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }
    }
}
