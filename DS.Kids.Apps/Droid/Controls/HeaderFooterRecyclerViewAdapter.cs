using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Android.Support.V7.Widget;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Droid.RecyclerView;

using Object = Java.Lang.Object;
using System.Collections.Specialized;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;

namespace DS.Kids.Apps.Droid.Controls
{

	public abstract class HeaderFooterRecyclerViewAdapter<T> : ArrayRecyclerAdapter
	{
		#region Fields

		private readonly IMvxAndroidBindingContext _bindingContext;

		private int _footerItemTemplateId;

		private int _headerItemTemplateId;

		#endregion

		#region Constructors and Destructors

		public HeaderFooterRecyclerViewAdapter()
			: this(MvxAndroidBindingContextHelpers.Current())
		{
		}

		public HeaderFooterRecyclerViewAdapter(IMvxAndroidBindingContext bindingContext)
		{
			_bindingContext = bindingContext;
		}

		#endregion

		#region Public Properties

		public virtual int FooterItemTemplateId
		{
			get
			{
				return _footerItemTemplateId;
			}
			set
			{
				if(_footerItemTemplateId == value)
				{
					return;
				}

				_footerItemTemplateId = value;

				// since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
				if(ItemsSource != null)
				{
					NotifyAndRaiseDataSetChanged();
				}
			}
		}

		public virtual int HeaderItemTemplateId
		{
			get
			{
				return _headerItemTemplateId;
			}
			set
			{
				if(_headerItemTemplateId == value)
				{
					return;
				}

				_headerItemTemplateId = value;

				// since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
				if(ItemsSource != null)
				{
					NotifyAndRaiseDataSetChanged();
				}
			}
		}

		public override int ItemCount
		{
			get
			{
				if(ItemsSource == null)
				{
					return 0;
				}
				return ItemsSource.Count() * 2 + Items.Sum(g => GetInternalList.Invoke(g).Count());
			}
		}

		public IEnumerable<T> Items
		{
			get
			{
				return (IEnumerable<T>)ItemsSource;
			}
		}

		#endregion

		#region Properties

		protected IMvxAndroidBindingContext BindingContext
		{
			get
			{
				return _bindingContext;
			}
		}

		protected abstract Func<T, IEnumerable> GetInternalList { get; }

		#endregion

		#region Public Methods and Operators

		public override int GetItemViewType(int position)
		{
			int groupIndex;
			int itemGroupIndex;
			var itemType = GeItemType(position, out groupIndex, out itemGroupIndex);

			return (int)itemType;
		}

		public void NotifyContentItemChanged(int position)
		{
			NotifyItemChanged(position);
		}

		public void NotifyContentItemInserted(int position)
		{
			NotifyItemInserted(position);
		}

		public void NotifyContentItemMoved(int fromPosition, int toPosition)
		{
			NotifyItemMoved(fromPosition, toPosition);
		}

		public void NotifyContentItemRangeChanged(int positionStart, int itemCount)
		{
			NotifyItemRangeChanged(positionStart, itemCount);
		}

		public void NotifyContentItemRangeInserted(int positionStart, int itemCount)
		{
			NotifyItemRangeInserted(positionStart, itemCount);
		}

		public void NotifyContentItemRangeRemoved(int positionStart, int itemCount)
		{
			NotifyItemRangeRemoved(positionStart, itemCount);
		}

		public void NotifyContentItemRemoved(int position)
		{
			NotifyItemRemoved(position);
		}

		public void NotifyFooterItemChanged(int position)
		{
			NotifyItemChanged(position);
		}

		public void NotifyFooterItemInserted(int position)
		{
			NotifyItemInserted(position);
		}

		public void NotifyFooterItemMoved(int fromPosition, int toPosition)
		{
			NotifyItemMoved(fromPosition, toPosition);
		}

		public void NotifyFooterItemRangeChanged(int positionStart, int itemCount)
		{
			NotifyItemRangeChanged(positionStart, itemCount);
		}

		public void NotifyFooterItemRangeInserted(int positionStart, int itemCount)
		{
			NotifyItemRangeInserted(positionStart, itemCount);
		}

		public void NotifyFooterItemRangeRemoved(int positionStart, int itemCount)
		{
			NotifyItemRangeRemoved(positionStart, itemCount);
		}

		public void NotifyFooterItemRemoved(int position)
		{
			NotifyItemRemoved(position);
		}

		public void NotifyHeaderItemChanged(int position)
		{
			NotifyItemChanged(position);
		}

		public void NotifyHeaderItemInserted(int position)
		{
			NotifyItemInserted(position);
		}

		public void NotifyHeaderItemMoved(int fromPosition, int toPosition)
		{
			NotifyItemMoved(fromPosition, toPosition);
		}

		public void NotifyHeaderItemRangeChanged(int positionStart, int itemCount)
		{
			NotifyItemRangeChanged(positionStart, itemCount);
		}

		public void NotifyHeaderItemRangeInserted(int positionStart, int itemCount)
		{
			NotifyItemRangeInserted(positionStart, itemCount);
		}

		public void NotifyHeaderItemRangeRemoved(int positionStart, int itemCount)
		{
			NotifyItemRangeRemoved(positionStart, itemCount);
		}

		public void NotifyHeaderItemRemoved(int position)
		{
			NotifyItemRemoved(position);
		}

		public override void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				int internalStartIndex;
				int groupCount;
				switch(e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						internalStartIndex = Items.Take(e.NewStartingIndex).Sum(g => GetInternalList.Invoke(g).Count()) + e.NewStartingIndex * 2;
						groupCount = ((IEnumerable)e.NewItems[0]).Count() + 2;

						NotifyItemRangeInserted(internalStartIndex, groupCount);
						RaiseDataSetChanged();
						NotifyAndRaiseDataSetChanged();
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
						internalStartIndex = Items.Take(e.OldStartingIndex).Sum(g => GetInternalList.Invoke(g).Count()) + e.OldStartingIndex * 2;
						groupCount = ((IEnumerable)e.OldItems[0]).Count() + 2;

						NotifyItemRangeRemoved(internalStartIndex, groupCount);
						RaiseDataSetChanged();
						NotifyAndRaiseDataSetChanged();
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

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			int groupIndex;
			int itemGroupIndex;
			var itemType = GeItemType(position, out groupIndex, out itemGroupIndex);

			object dataContext;

			if(itemType == ItemListType.Header || itemType == ItemListType.Footer)
			{
				dataContext = ItemsSource.ElementAt(groupIndex);
			}
			else
			{
				dataContext = GetInternalList.Invoke(Items.ElementAt(groupIndex)).ElementAt(itemGroupIndex);
			}

			((MvxRecyclerViewHolder)holder).DataContext = dataContext;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemBindingContext = new MvxAndroidBindingContext(parent.Context, _bindingContext.LayoutInflater);

			var itemType = (ItemListType)viewType;

			var itemTemplate = ItemTemplateId;
			if(itemType == ItemListType.Header)
			{
				itemTemplate = HeaderItemTemplateId;
			}
			else if(itemType == ItemListType.Footer)
			{
				itemTemplate = FooterItemTemplateId;
			}

			var viewHolder = new MvxRecyclerViewHolder(InflateViewForHolder(parent, itemTemplate, itemBindingContext), itemBindingContext);
			if (itemType == ItemListType.Item) 
			{
				viewHolder.Click = ItemClick;
				viewHolder.LongClick = ItemLongClick;
			} 
			else if (itemType == ItemListType.Header) 
			{
				viewHolder.Click = HeaderItemClick;
				viewHolder.LongClick = HeaderItemLongClick;
			}
			else if (itemType == ItemListType.Footer)
			{
				viewHolder.Click = FooterItemClick;
				viewHolder.LongClick = FooterItemLongClick;
			}

			return viewHolder;
		}

		public override void OnViewAttachedToWindow(Object holder)
		{
			base.OnViewAttachedToWindow(holder);

			var viewHolder = holder as IMvxRecyclerViewHolder;
			if(viewHolder != null)
			{
				viewHolder.OnAttachedToWindow();
			}
		}

		public override void OnViewDetachedFromWindow(Object holder)
		{
			base.OnViewDetachedFromWindow(holder);

			var viewHolder = holder as IMvxRecyclerViewHolder;
			if(viewHolder != null)
			{
				viewHolder.OnDetachedFromWindow();
			}
		}

		#endregion

		#region Methods

		protected ItemListType GeItemType(int position, out int groupIndex, out int itemGroupIndex)
		{
			var counts = Items.Select(g => GetInternalList.Invoke(g).Count()).ToList();
			var upperBounds = counts.ToList();
			for(int i = 1; i < upperBounds.Count(); i++)
			{
				upperBounds[i] += upperBounds[i - 1] + 2;
			}

			int itemIndex = 0;

			if(position == 0)
			{
				groupIndex = itemIndex;
				itemGroupIndex = -1;
				return ItemListType.Header;
			}

			foreach(var upperBound in upperBounds)
			{
				if(position <= upperBound)
				{
					break;
				}
				if(position == upperBound + 1)
				{
					groupIndex = itemIndex;
					itemGroupIndex = -1;
					return ItemListType.Footer;
				}

				itemIndex++;

				if(position == upperBound + 2)
				{
					groupIndex = itemIndex;
					itemGroupIndex = -1;
					return ItemListType.Header;
				}
			}

			groupIndex = itemIndex;
			if(groupIndex == 0)
			{
				itemGroupIndex = position - 1;
			}
			else
			{
				itemGroupIndex = position - (upperBounds[itemIndex] - counts[itemIndex]) - 1;
			}
			return ItemListType.Item;
		}

		protected virtual View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
		{
			return bindingContext.BindingInflate(viewType, parent, false);
		}

		#endregion
	}

	public enum ItemListType
	{

		Header,

		Footer,

		Item

	}

}
