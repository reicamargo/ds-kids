using System;

using Android.Content;
using Android.Runtime;
using Android.Util;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.RecyclerView;

namespace DS.Kids.Apps.Droid.Controls
{

	[Register("ds.kids.apps.droid.controls.HighlightRecyclerView")]
	public class HighlightRecyclerView : MvxRecyclerView
	{
		#region Constructors and Destructors

		public HighlightRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new MvxRecyclerAdapter())
		{
		}

		public HighlightRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new MvxRecyclerAdapter())
		{
		}

		public HighlightRecyclerView(Context context, IAttributeSet attrs, int defStyle, HighlightRecyclerViewAdapter adapter)
			: base(context, attrs, defStyle, adapter)
		{
			var finder = Mvx.Resolve<IMvxAppResourceTypeFinder>();
			var resourceType = finder.Find();
			var styleable = resourceType.GetNestedType("Styleable");

			var groupId = (int[])styleable.GetField("MvxRecyclerView").GetValue(null);

			var highlightItemTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs, groupId,
				(int)styleable.GetField("MvxRecyclerView_HighlightItemTemplate").GetValue(null));

			adapter.HighlightItemTemplateId = highlightItemTemplateId;
		}

		protected HighlightRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion
	}

}
