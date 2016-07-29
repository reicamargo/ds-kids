using System;
using Android.Widget;
using Android.Runtime;
using Android.Content;
using Android.Util;

namespace DS.Kids.Apps.Droid.Controls
{
	[Register("ds.kids.apps.droid.controls.PagedHorizontalScrollView")]
	public class PagedHorizontalScrollView : HorizontalScrollView
	{
		public PagedHorizontalScrollView(Context context)
			: base(context) { }


		public PagedHorizontalScrollView(Context context, IAttributeSet attrs)
			: base(context, attrs) { }
		
		public override void ComputeScroll(){
			return;
		}
	}
}

