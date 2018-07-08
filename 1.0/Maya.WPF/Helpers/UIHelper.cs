using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Maya.WPF.Helpers
{
    /// <summary>
    /// http://stackoverflow.com/questions/636383/wpf-ways-to-find-controls
    /// </summary>
    public static class UIHelper
    {


        /// <summary> Finds a parent of a given item on the visual tree. This does not include hidden objects. </summary>
        /// <param name="child"> A direct or indirect child of the queried item. </param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <returns> The first parent item that matches the submitted type parameter. If not matching item can be found, a null reference is being returned. </returns>
        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            return FindParent<T>(child, true);
        }


        /// <summary> Finds a parent of a given item on the logical tree. This *does* include hidden objects.</summary>
        /// <param name="child"> A direct or indirect child of the queried item. </param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <returns> The first parent item that matches the submitted type parameter. If not matching item can be found, a null reference is being returned. </returns>
        public static T FindLogicalParent<T>(DependencyObject child) where T : DependencyObject
        {
            return FindParent<T>(child, false);
        }


        private static T FindParent<T>(DependencyObject child, bool IsVisual) where T : DependencyObject
        {
            // get parent item
            var parentObject = IsVisual ? VisualTreeHelper.GetParent(child) : LogicalTreeHelper.GetParent(child);

            // we’ve reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we’re looking for
            if (parentObject is T parent) return parent;

            // use recursion to proceed with next level
            return FindVisualParent<T>(parentObject);
        }



        /// <summary> Finds a Child of a given item in the visual tree. </summary>
        /// <param name="parent"> A direct parent of the queried item. </param>
        /// <param name="childName"> x:Name or Name of child. </param>
        /// <typeparam name="T"> The type of the queried item. </typeparam>
        /// <returns> The first parent item that matches the submitted type parameter. If not matching item can be found, a null parent is being returned. </returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                var childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }


        /// <summary>
        /// Search the DependencyObject's visual 
        /// </summary>
        /// <remarks> http://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    var child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T c)
                    {
                        yield return c;
                    }

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

 
        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                foreach (var child in LogicalTreeHelper.GetChildren(depObj))
                {
                    //Check if this children is a Dependency Object
                    if (child is DependencyObject doChild)
                    {
                        if (doChild is T c)
                        {
                            yield return c;
                        }

                        foreach (var childOfChild in FindLogicalChildren<T>(doChild))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

        public static T FindFirstChild<T>(DependencyObject depObj, bool b_IsVisual) where T : DependencyObject
        {
            if (depObj != null)
            {
                return b_IsVisual ? FindVisualChildren<T>(depObj).FirstOrDefault() : FindLogicalChildren<T>(depObj).FirstOrDefault();
            }
            return null;
        }

        public static T FindControlParent<T>(FrameworkElement control) where T : DependencyObject
        {
            var ctrlParent = control;
            while ((ctrlParent = ctrlParent.Parent as FrameworkElement) != null)
            {
                if (ctrlParent.GetType() == typeof(T))
                {
                    return ctrlParent as T;
                }
            }
            return null;
        }
    }
}
