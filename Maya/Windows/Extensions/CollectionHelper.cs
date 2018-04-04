using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Maya.Windows.Extensions
{
    public static class CollectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }




        public static void Sort<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = new List<T>(collection);
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="source"></param>
        ///// <typeparam name="TSource"></typeparam>
        ///// <returns></returns>
        //public static ICollection ToSortedList<TSource>(this IEnumerable<TSource> source)
        //{
        //    return new SortedColl<TSource>(source);
        //}
    }
}
