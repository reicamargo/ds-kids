using System;
using Android.Views;
using Android.Util;
using Android.Runtime;
using Android.Content;

namespace DS.Kids.Apps.Droid.Controls
{
	[Register("ds.kids.apps.droid.controls.BgBindableView")]
	public class BgBindableView : View
	{

		#region Fields

		private int _backgroundResource = 0;

		#endregion


		#region Constructors and Destructors

		public BgBindableView(Context context)
			: base(context) { }

		public BgBindableView(Context context, IAttributeSet attrs)
			: base(context, attrs) { }

		public BgBindableView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle) { }
	
		#endregion

		#region Public Properties

		public int BackgroundResource
		{
			get
			{
				return _backgroundResource;
			}
			set
			{
				var changed = _backgroundResource != value;
				_backgroundResource = value;
				if(changed)
				{
					var t = this.PaddingTop;
					var l = this.PaddingLeft;
					var r = this.PaddingRight;
					var b = this.PaddingBottom;

					this.SetBackgroundResource(_backgroundResource);
					this.SetPadding(l, t, r, b);
				}

			}
		}

		#endregion
	}
}

