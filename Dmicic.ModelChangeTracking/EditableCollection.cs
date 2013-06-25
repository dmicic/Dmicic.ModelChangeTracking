using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public class EditableCollection<T> : ObservableCollection<T>, ITrackableObject where T : IEditTracking
    {
        private List<T> DeletedItems { get; set; }

        public IEnumerable<T> NotChanged { get { return this.GetByState(EEditState.NotChanged); } }
        public IEnumerable<T> Changed { get { return this.GetByState(EEditState.Changed); } }
        public IEnumerable<T> New { get { return this.GetByState(EEditState.New); } }
        public IEnumerable<T> Deleted { get { return this.GetByState(EEditState.Deleted); } }

        #region ITrackableObject
        public bool Track { get; set; }
        #endregion

        public EditableCollection()
        {
            this.Init();
        }

        public EditableCollection(IEnumerable<T> initList)
        {
            foreach (var item in initList)
            {
                item.Track = true;
                this.Add(item);
            }

            this.Init();
        }

        private void Init()
        {
            this.DeletedItems = new List<T>();
            this.Track = true;
            this.CollectionChanged += EditableCollection_CollectionChanged;
        }

        public void AcceptAll()
        {
            foreach (var item in this.Changed.Union(this.New))
                ((IEditTracking)item).AcceptChanges();

            this.DeletedItems.Clear();
        }

        private IEnumerable<T> GetByState(EEditState state)
        {
            if (state == EEditState.Deleted)
                return this.DeletedItems;
            else
                return this.Where(x => ((IEditTracking)x).State == state);
        }

        private void EditableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                this.HandleCollectionChange(e.NewItems.Cast<T>(), EEditState.New);

            if (e.OldItems != null)
                this.HandleCollectionChange(e.OldItems.Cast<T>(), EEditState.Deleted);
        }

        private void HandleCollectionChange(IEnumerable<T> changeSet, EEditState state)
        {
            if (changeSet == null)
                return;

            if (state == EEditState.New)
            {
                foreach (var item in changeSet)
                {
                    item.Track = true;
                    if(this.Track)
                        item.State = EEditState.New;
                }
            }
            else if (state == EEditState.Deleted && this.Track)
            {
                foreach (var item in changeSet)
                {
                    if (item.State != EEditState.New)
                        this.DeletedItems.Add(item);

                    this.Remove(item);
                    item.State = EEditState.Deleted;
                }
            }
        }
    }
}
