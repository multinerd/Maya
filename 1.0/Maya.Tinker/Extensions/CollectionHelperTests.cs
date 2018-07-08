using System;
using System.Collections.ObjectModel;
using Maya.Windows.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maya.Test
{
    [TestClass]
    public class CollectionHelperTests
    {
        [TestMethod]
        public void TestObservableCollectionSortExtension()
        {
            var observableCollection = new ObservableCollection<int>();
            var maxValue = 10;

            // Populate the list in reverse mode [maxValue, maxValue-1, ..., 1, 0]
            for (int i = maxValue; i >= 0; i--)
            {
                observableCollection.Add(i);
            }

            // Assert the collection is in reverse mode
            for (int i = maxValue; i >= 0; i--)
            {
                Assert.AreEqual(i, observableCollection[maxValue - i]);
            }

            // Sort the observable collection
            observableCollection.Sort((a, b) => { return a.CompareTo(b); });

            // Assert element have been sorted
            for (int i = 0; i < maxValue; i++)
            {
                Assert.AreEqual(i, observableCollection[i]);
            }
        }
    }
}
