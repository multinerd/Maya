using System;
using System.ComponentModel;

// https://web.archive.org/web/20160202215105/http://indepthdev.azurewebsites.net:80/2010/10/reusable-editablemodelbase-using-ieditableobject/
namespace Maya.WPF.Models
{
    /// <inheritdoc />
    public abstract class EditableModelBase<T> : /*BindableBase,*/ IEditableObject
    {
        private T Cache { get; set; }

        private object CurrentModel => this;



        #region IEditableObject Members

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void EndEdit()
        {
            Cache = default(T);
        }

        /// <inheritdoc />
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
