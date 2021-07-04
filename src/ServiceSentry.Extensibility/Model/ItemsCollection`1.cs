using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ServiceSentry.Extensibility
{
    [DataContract]
    public abstract class ItemsCollection<T> : NotifyPropertyChanged where T : NotifyPropertyChanged
    {
        private ObservableCollection<T> _items;
        private ObservableCollection<T> _modifiedItems;

        protected ItemsCollection()
        {
            Items = new ObservableCollection<T>();
            ModifiedItems = new ObservableCollection<T>();

            Items.CollectionChanged += OnCollectionChanged;
        }

        [DataMember]
        public ObservableCollection<T> Items
        {
            get { return _items; }
            set
            {
                if (_items == value) return;
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ObservableCollection<T> ModifiedItems
        {
            get { return _modifiedItems; }
            set
            {
                if (_modifiedItems == value) return;
                _modifiedItems = value;
                OnPropertyChanged("ModifiedItems");
            }
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _modifiedItems.Clear();

            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    _modifiedItems.Add(item);
                    item.PropertyChanged += OnItemPropertyChanged;
                }
            }

            if (e.OldItems == null) return;
            foreach (T item in e.OldItems)
            {
                _modifiedItems.Add(item);
                item.PropertyChanged -= OnItemPropertyChanged;
            }
        }

        public virtual void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }
}