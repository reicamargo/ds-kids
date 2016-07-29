using System;
using Android.Views;
using Android.Runtime;
using Android.Widget;
using Android.Util;
using Android.Content;

namespace DS.Kids.Apps.Droid
{
	[Register("ds.kids.apps.droid.SquareLinearLayout")]
	public class SquareLinearLayout : LinearLayout
	{
		public SquareLinearLayout(Context context)
			: base(context) { }


		public SquareLinearLayout(Context context, IAttributeSet attrs)
			: base(context, attrs) { }

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
		}
	}
}

