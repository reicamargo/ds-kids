using System;
using System.Collections;
using System.Windows.Input;

using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;

using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace Cirrious.MvvmCross.Droid.RecyclerView
{

	[Register("cirrious.mvvmcross.droid.recyclerview.MvxRecyclerView")]
	public class MvxRecyclerView : Android.Support.V7.Widget.RecyclerView
	{
		#region Public Properties

		public new IMvxRecyclerAdapter Adapter
		{
			get
			{
				return GetAdapter() as IMvxRecyclerAdapter;
			}
			set
			{
				var existing = Adapter;

				if(existing == value)
				{
					return;
				}

				// Support lib doesn't seem to have anything similar to IListAdapter yet
				// hence cast to Adapter.

				if(value != null && existing != null)
				{
					value.ItemsSource = existing.ItemsSource;
					value.ItemTemplateId = existing.ItemTemplateId;
					value.ItemClick = existing.ItemClick;
					value.ItemLongClick = existing.ItemLongClick;

					SwapAdapter((Adapter)value, false);
				}
				else
				{
					SetAdapter((Adapter)value);
				}
			}
		}

		public ICommand ItemClick
		{
			get
			{
				return Adapter.ItemClick;
			}
			set
			{
				Adapter.ItemClick = value;
			}
		}

		public ICommand ItemLongClick
		{
			get
			{
				return Adapter.ItemLongClick;
			}
			set
			{
				Adapter.ItemLongClick = value;
			}
		}

		[MvxSetToNullAfterBinding]
		public IEnumerable ItemsSource
		{
			get
			{
				return Adapter.ItemsSource;
			}
			set
			{
				Adapter.ItemsSource = value;
			}
		}

		public int ItemTemplateId
		{
			get
			{
				return Adapter.ItemTemplateId;
			}
			set
			{
				Adapter.ItemTemplateId = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		public override sealed void SetLayoutManager(LayoutManager layout)
		{
			base.SetLayoutManager(layout);
		}

		#endregion

		#region ctor

		protected MvxRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public MvxRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new MvxRecyclerAdapter())
		{
		}

		public MvxRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new MvxRecyclerAdapter())
		{
		}

		public MvxRecyclerView(Context context, IAttributeSet attrs, int defStyle, IMvxRecyclerAdapter adapter)
			: base(context, attrs, defStyle)
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

		public event EventHandler<ScrolledEventArgs> ScrollChanged;

		public override void OnScrolled (int p0, int p1)
		{
			base.OnScrolled (p0, p1);
		}

		protected override void OnScrollChanged (int l, int t, int oldl, int oldt)
		{
			base.OnScrollChanged (l, t, oldl, oldt);
			var h = ScrollChanged;
			if (h != null) {
				h (this, new ScrolledEventArgs(l, t, oldl, oldt));
			}
		}

		#endregion
	}

	public class ScrolledEventArgs : EventArgs
	{
		public int CurrentX { get; private set; }
		public int CurrentY { get; private set; }
		public int OldX { get; private set; }
		public int OldY { get; private set; }

		public ScrolledEventArgs(int currentX, int currentY, int oldX, int oldY)
		{
			CurrentX = currentX;
			CurrentY = currentY;
			OldX = oldX;
			OldY = oldY;
		}

	}
}
