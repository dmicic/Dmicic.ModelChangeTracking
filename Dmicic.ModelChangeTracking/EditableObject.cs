using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public abstract class EditableObject<T> : PropertyChangedBase, IEditableObject, IEditTracking
    {
        private static readonly IEnumerable<string> FieldsToIgnore = new List<string> { "Memory", "IsChanged", "Track", "State" };
        private Memento<T> Memory { get; set; }

        public EditableObject()
        {
            this.Memory = new Memento<T>();
            this.PropertyChanged += EditableObject_PropertyChanged;
        }

        #region IEditTracking

        public EEditState State { get; set; }
        public bool Track { get; set; }

        public bool IsChanged
        {
            get { return this.State != EEditState.NotChanged; }
        }

        public void AcceptChanges()
        {
            this.State = EEditState.NotChanged;
            this.Memory.Dispose();
        }

        #endregion

        #region IEditableObject

        public void BeginEdit()
        {
            if (this.Track)
            {
                this.Memory.Save(this);
            }
        }

        public void CancelEdit()
        {
            if (this.Track)
            {
                this.Memory.Restore();
                this.Memory.Dispose();
            }
        }

        public void EndEdit()
        {
            if (this.Track)
            {
                this.Memory.Dispose();
            }
        }

        #endregion

        private void EditableObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.Track && !EditableObject<T>.FieldsToIgnore.Contains(e.PropertyName))
            {
                if (this.State != EEditState.New && this.State != EEditState.Deleted)
                {
                    this.State = EEditState.Changed;
                }
            }
        }
    }
}
