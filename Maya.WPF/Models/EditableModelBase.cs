using System;
using System.ComponentModel;
using Telerik.Windows.Controls;

// https://web.archive.org/web/20160202215105/http://indepthdev.azurewebsites.net:80/2010/10/reusable-editablemodelbase-using-ieditableobject/
namespace Multinerd.Windows.MVVM
{
    public abstract class EditableModelBase<T> : ViewModelBase, IEditableObject
    {
        private T Cache { get; set; }

        private object CurrentModel => this;



        #region IEditableObject Members

        public void BeginEdit()
        {
            Cache = Activator.CreateInstance<T>();
            foreach (var info in CurrentModel.GetType().GetProperties())
            {
                if (!info.CanRead || !info.CanWrite) continue;
                var oldValue = info.GetValue(CurrentModel, null);
                Cache.GetType().GetProperty(info.Name)?.SetValue(Cache, oldValue, null);
            }
        }

        public void EndEdit()
        {
            Cache = default(T);
        }

        public void CancelEdit()
        {
            foreach (var info in CurrentModel.GetType().GetProperties())
            {
                if (!info.CanRead || !info.CanWrite) continue;
                var oldValue = info.GetValue(Cache, null);
                CurrentModel.GetType().GetProperty(info.Name)?.SetValue(CurrentModel, oldValue, null);
            }
        }

        #endregion
    }
}
