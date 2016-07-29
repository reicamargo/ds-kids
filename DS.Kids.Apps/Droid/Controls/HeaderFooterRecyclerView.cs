using System;
using Cirrious.MvvmCross.Droid.RecyclerView;
using System.Windows.Input;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Android.Support.V7.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace DS.Kids.Apps.Droid.Controls
{
	public class HeaderFooterRecyclerView<T> : MvxRecyclerView
	{
		#region Constructors and Destructors

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs)
			: base(context, attrs) { }

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle) { }

		public HeaderFooterRecyclerView(Context context, IAttributeSet attrs, int defStyle, HeaderFooterRecyclerViewAdapter<T> adapter)
			: base(context, attrs, defStyle, adapter) 
		{
			// Note: Any calling derived class passing a null adapter is responsible for setting
			// it's own itemTemplateId
			if(adapter == null)
			{
				return;
			}

			SetLayoutManager(new LinearLayoutManager(context));

			var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
			adapter.ItemTemplateId = itemTemplateId;
			Adapter = adapter;
		}

		protected HeaderFooterRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer) { }

		#endregion
		public virtual new HeaderFooterRecyclerViewAdapter<T> Adapter
		{
			get
			{
				return GetAdapter() as HeaderFooterRecyclerViewAdapter<T>;
			}
			set
			{
				var existing = Adapter;

				if (existing == value) 
				{
					value.HeaderItemClick = existing.HeaderItemClick;
					value.HeaderItemLongClick = existing.HeaderItemLongClick;
					value.FooterItemClick = existing.FooterItemClick;
					value.FooterItemLongClick = existing.FooterItemLongClick;

					SetAdapter((Adapter)value);
				} 
				else 
				{
					// Support lib doesn't seem to have anything similar to IListAdapter yet
					// hence cast to Adapter.
					if(value != null && existing != null)
					{
						value.ItemsSource = existing.ItemsSource;
						value.ItemTemplateId = existing.ItemTemplateId;
						value.ItemClick = existing.ItemClick;
						value.ItemLongClick = existing.ItemLongClick;

						value.HeaderItemClick = existing.HeaderItemClick;
						value.HeaderItemLongClick = existing.HeaderItemLongClick;
						value.FooterItemClick = existing.FooterItemClick;
						value.FooterItemLongClick = existing.FooterItemLongClick;

						SwapAdapter((Adapter)value, false);
					}
					else
					{
						SetAdapter((Adapter)value);
					}
				}
			}
		}

		public ICommand HeaderItemClick
		{
			get
			{
				return Adapter.HeaderItemClick;
			}
			set
			{
				Adapter.HeaderItemClick = value;
			}
		}

		public ICommand HeaderItemLongClick
		{
			get
			{
				return Adapter.HeaderItemLongClick;
			}
			set
			{
				Adapter.HeaderItemLongClick = value;
			}
		}

		public ICommand FooterItemClick
		{
			get
			{
				return Adapter.FooterItemClick;
			}
			set
			{
				Adapter.FooterItemClick = value;
			}
		}

		public ICommand FooterItemLongClick
		{
			get
			{
				return Adapter.FooterItemLongClick;
			}
			set
			{
				Adapter.FooterItemLongClick = value;
			}
		}
	}
}

