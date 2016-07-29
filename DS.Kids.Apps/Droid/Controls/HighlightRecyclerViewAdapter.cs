using System.Collections.Specialized;

using Android.Support.V7.Widget;
using Android.Views;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Exceptions;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Droid.RecyclerView;

using Java.Lang;

using Exception = System.Exception;

namespace DS.Kids.Apps.Droid.Controls
{

	public abstract class HighlightRecyclerViewAdapter : ArrayRecyclerAdapter
	{
		#region Fields

		private readonly IMvxAndroidBindingContext _bindingContext;

		private int _highlightItemTemplateId;

		#endregion

		#region Constructors and Destructors

		public HighlightRecyclerViewAdapter()
			: this(MvxAndroidBindingContextHelpers.Current())
		{
		}

		public HighlightRecyclerViewAdapter(IMvxAndroidBindingContext bindingContext)
		{
			_bindingContext = bindingContext;
		}

		#endregion

		#region Enums

		protected enum ItemListType
		{

			Normal,

			Highlighted

		}

		#endregion

		#region Public Properties

		public virtual int HighlightItemTemplateId
		{
			get
			{
				return _highlightItemTemplateId;
			}
			set
			{
				if(_highlightItemTemplateId == value)
				{
					return;
				}

				_highlightItemTemplateId = value;

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
				return ItemsSource.Count();
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

		#endregion

		#region Public Methods and Operators

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

		public override void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				switch(e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
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
						NotifyItemRangeRemoved(e.NewStartingIndex, e.NewItems.Count);
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

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			//Check full span here
			((MvxRecyclerViewHolder)holder).DataContext = ItemsSource.ElementAt(position);
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

		protected abstract ItemListType GetItemType(int position);

		protected virtual View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
		{
			return bindingContext.BindingInflate(viewType, parent, false);
		}

		#endregion
	}

}
