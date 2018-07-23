using JetBrains.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Maya.Core.Collections
{
    /// <summary>
    /// A collection of extensions that target <see cref="T:System.Collections.Generic.IEnumerable`1"/>
    /// </summary>
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class EnumerableExtensions
    {
        /// <summary>Creates a <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <returns>A <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> that contains elements from the input sequence.</returns>
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }
    }
}
