using System;

using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;

using Cirrious.MvvmCross.Droid.RecyclerView;

namespace DS.Kids.Apps.Droid.Controls
{

	[Register("ds.kids.apps.droid.controls.DicaRecyclerView")]
	public class DicaRecyclerView : MvxRecyclerView
	{
		#region Constructors and Destructors

		public DicaRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new DicaRecyclerViewAdapter())
		{
		}

		public DicaRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new DicaRecyclerViewAdapter())
		{
		}

		public DicaRecyclerView(Context context, IAttributeSet attrs, int defStyle, DicaRecyclerViewAdapter adapter)
			: base(context, attrs, defStyle, adapter)
		{
			SetLayoutManager(new MyLinearLayoutManager(context, LinearLayoutManager.Vertical, false));
		}

		protected DicaRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion
	}

}
