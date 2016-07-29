using System;

using System.Collections;
using System.Collections.Specialized;
using System.Windows.Input;

using Android.Support.V7.Widget;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Droid.RecyclerView;

namespace DS.Kids.Apps.Droid.Controls
{

	public abstract class ArrayRecyclerAdapter : RecyclerView.Adapter, IList, IMvxRecyclerAdapter
	{
		#region Fields

		private readonly object _lock = new object();

		private ICommand _itemClick;

		private ICommand _itemLongClick;

		private ICommand _headerItemClick;

		private ICommand _headerItemLongClick;

		private ICommand _footerItemClick;

		private ICommand _footerItemLongClick;

		private IEnumerable _itemsSource;

		private int _itemTemplateId;

		private IDisposable _subscription;

		#endregion

		#region Public Events

		public event EventHandler DataSetChanged;

		#endregion

		#region Public Properties

		public int Count
		{
			get
			{
				return List.Count;
			}
		}

		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		public ICommand ItemClick
		{
			get
			{
				return _itemClick;
			}
			set
			{
				if(ReferenceEquals(_itemClick, value))
				{
					return;
				}

				if(_itemClick != null)
				{
					MvxTrace.Warning("Changing ItemClick may cause inconsistencies where some items still call the old command.");
				}

				_itemClick = value;
			}
		}

		public ICommand ItemLongClick
		{
			get
			{
				return _itemLongClick;
			}
			set
			{
				if(ReferenceEquals(_itemLongClick, value))
				{
					return;
				}

				if(_itemLongClick != null)
				{
					MvxTrace.Warning("Changing ItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_itemLongClick = value;
			}
		}

		public ICommand HeaderItemClick
		{
			get
			{
				return _headerItemClick;
			}
			set
			{
				if(ReferenceEquals(_headerItemClick, value))
				{
					return;
				}

				if(_headerItemClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemClick may cause inconsistencies where some items still call the old command.");
				}

				_headerItemClick = value;
			}
		}

		public ICommand HeaderItemLongClick
		{
			get
			{
				return _headerItemLongClick;
			}
			set
			{
				if(ReferenceEquals(_headerItemLongClick, value))
				{
					return;
				}

				if(_headerItemLongClick != null)
				{
					MvxTrace.Warning("Changing HeaderItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_headerItemLongClick = value;
			}
		}

		public ICommand FooterItemClick
		{
			get
			{
				return _footerItemClick;
			}
			set
			{
				if(ReferenceEquals(_footerItemClick, value))
				{
					return;
				}

				if(_footerItemClick != null)
				{
					MvxTrace.Warning("Changing FooterItemClick may cause inconsistencies where some items still call the old command.");
				}

				_footerItemClick = value;
			}
		}

		public ICommand FooterItemLongClick
		{
			get
			{
				return _footerItemLongClick;
			}
			set
			{
				if(ReferenceEquals(_footerItemLongClick, value))
				{
					return;
				}

				if(_footerItemLongClick != null)
				{
					MvxTrace.Warning("Changing FooterItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_footerItemLongClick = value;
			}
		}

		[MvxSetToNullAfterBinding]
		public virtual IEnumerable ItemsSource
		{
			get
			{
				return _itemsSource;
			}
			set
			{
				SetItemsSource(value);
			}
		}

		public virtual int ItemTemplateId
		{
			get
			{
				return _itemTemplateId;
			}
			set
			{
				if(_itemTemplateId == value)
				{
					return;
				}

				_itemTemplateId = value;

				// since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
				if(_itemsSource != null)
				{
					NotifyAndRaiseDataSetChanged();
				}
			}
		}

		private IList List
		{
			get
			{
				return (IList)ItemsSource;
			}
		}

		public bool ReloadOnAllItemsSourceSets { get; set; }

		public object SyncRoot
		{
			get
			{
				return _lock;
			}
		}

		#endregion

		#region Public Indexers

		public object this[int index]
		{
			get
			{
				return List[index];
			}
			set
			{
				List[index] = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public int Add(object obj)
		{
			lock(_lock)
			{
				int lastIndex = List.Count - 1;
				var result = List.Add(obj);
				NotifyItemInserted(lastIndex);
				return result;
			}
		}

		public void AddRange(ICollection collection)
		{
			lock(_lock)
			{
				int lastIndex = List.Count;
				foreach(var obj in collection)
				{
					List.Add(obj);
				}
				NotifyItemRangeInserted(lastIndex, collection.Count);
			}
		}

		public void Clear()
		{
			lock(_lock)
			{
				int size = List.Count;
				if(size > 0)
				{
					List.Clear();
					NotifyItemRangeRemoved(0, size);
				}
			}
		}

		public bool Contains(object obj)
		{
			return List.Contains(obj);
		}

		public void CopyTo(Array array, int index)
		{
			List.CopyTo(array, index);
		}

		public object Get(int location)
		{
			return List[location];
		}

		public IEnumerator GetEnumerator()
		{
			return List.GetEnumerator();
		}

		public virtual object GetItem(int position)
		{
			return _itemsSource.ElementAt(position);
		}

		public IList GetList()
		{
			return List;
		}

		public int IndexOf(object obj)
		{
			return List.IndexOf(obj);
		}

		public void Insert(int location, object obj)
		{
			lock(_lock)
			{
				List.Insert(location, obj);
				NotifyItemInserted(location);
			}
		}

		public bool IsEmpty()
		{
			return List.Count == 0;
		}

		public virtual void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				switch(e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Move:
						for(int i = 0; i < e.NewItems.Count; i++)
						{
							var oldItem = e.OldItems[i];
							var newItem = e.NewItems[i];

							NotifyItemMoved(ItemsSource.GetPosition(oldItem), ItemsSource.GetPosition(newItem));
							RaiseDataSetChanged();
						}
						break;
					case NotifyCollectionChangedAction.Replace:
						NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Remove:
						NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
						RaiseDataSetChanged();
						break;
					case NotifyCollectionChangedAction.Reset:
						NotifyAndRaiseDataSetChanged();
						break;
				}
			}
			catch(Exception exception)
			{
				Mvx.Warning("Exception masked during Adapter RealNotifyDataSetChanged {0}", exception.ToLongString());
			}
		}

		public void Remove(object obj)
		{
			lock(_lock)
			{
				int index = IndexOf(obj);
				List.Remove(obj);
				NotifyItemRemoved(index);
			}
		}

		public bool RemoveAll(ICollection collection)
		{
			bool modified = false;

			for(int i = List.Count - 1; i >= 0; i--)
			{
				var obj = List[i];
				if(Contains(collection, obj))
				{
					lock(_lock)
					{
						int index = IndexOf(obj);
						Remove(obj);
						NotifyItemRemoved(index);
					}

					modified = true;
				}
			}

			return modified;
		}

		public void RemoveAt(int location)
		{
			lock(_lock)
			{
				List.RemoveAt(location);
				NotifyItemRemoved(location);
			}
		}

		public void ReplaceWith(IList data)
		{
			if(List.Count == 0 && data.Count == 0)
			{
				return;
			}

			if(List.Count == 0)
			{
				AddRange(data);
				return;
			}

			if(data.Count == 0)
			{
				Clear();
				return;
			}

			RetainAll(data);

			if(List.Count == 0)
			{
				AddRange(data);
				return;
			}

			for(int indexNew = 0; indexNew < data.Count; indexNew++)
			{
				var item = data[indexNew];

				int indexOld = IndexOf(item);

				if(indexOld == -1)
				{
					Insert(indexNew, item);
				}
				else if(indexOld == indexNew)
				{
					Set(indexNew, item);
				}
				else
				{
					List.RemoveAt(indexOld);
					List.Insert(indexNew, item);
					NotifyItemMoved(indexOld, indexNew);
				}
			}
		}

		private static bool Contains(ICollection collection, object obj)
		{
			foreach(var collectionItem in collection)
			{
				if(collectionItem.Equals(obj))
				{
					return true;
				}
			}

			return false;
		}

		public bool RetainAll(ICollection collection)
		{
			bool modified = false;

			for(int i = List.Count - 1; i >= 0; i--)
			{
				var obj = List[i];
				if(!Contains(collection, obj))
				{
					lock(_lock)
					{
						int index = IndexOf(obj);
						Remove(obj);
						NotifyItemRemoved(index);
					}

					modified = true;
				}
			}

			return modified;
		}

		public object Set(int location, object obj)
		{
			lock(_lock)
			{
				var origin = List[location];
				List[location] = obj;
				NotifyItemChanged(location);
				return origin;
			}
		}

		#endregion

		#region Explicit Interface Methods

		IEnumerator IEnumerable.GetEnumerator()
		{
			return List.GetEnumerator();
		}

		#endregion

		#region Methods

		protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyDataSetChanged(e);
		}

		protected virtual void SetItemsSource(IEnumerable value)
		{
			if(ReferenceEquals(_itemsSource, value) && !ReloadOnAllItemsSourceSets)
			{
				return;
			}

			if(_subscription != null)
			{
				_subscription.Dispose();
				_subscription = null;
			}

			_itemsSource = value;

			if(_itemsSource != null && !(_itemsSource is IList))
			{
				MvxBindingTrace.Trace(MvxTraceLevel.Warning,
					"Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
			}

			var newObservable = _itemsSource as INotifyCollectionChanged;
			if(newObservable != null)
			{
				// TODO
				newObservable.CollectionChanged += OnItemsSourceCollectionChanged;
				//_subscription = newObservable.WeakSubscribe(OnItemsSourceCollectionChanged);
			}

			NotifyAndRaiseDataSetChanged();
		}

		protected void NotifyAndRaiseDataSetChanged()
		{
			RaiseDataSetChanged();
			NotifyDataSetChanged();
		}

		protected void RaiseDataSetChanged()
		{
			var handler = DataSetChanged;
			if(handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		#endregion
	}

}
