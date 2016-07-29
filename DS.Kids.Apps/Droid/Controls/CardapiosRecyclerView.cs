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

	[Register("ds.kids.apps.droid.controls.CardapiosRecyclerView")]
	public class CardapiosRecyclerView : MvxRecyclerView
	{
		#region Constructors and Destructors

		public CardapiosRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new CardapiosRecyclerViewAdapter())
		{
		}

		public CardapiosRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new CardapiosRecyclerViewAdapter())
		{
		}

		public CardapiosRecyclerView(Context context, IAttributeSet attrs, int defStyle, CardapiosRecyclerViewAdapter adapter)
			: base(context, attrs, defStyle, adapter)
		{
			var finder = Mvx.Resolve<IMvxAppResourceTypeFinder>();
			var resourceType = finder.Find();
			var styleable = resourceType.GetNestedType("Styleable");

			var groupId = (int[])styleable.GetField("MvxRecyclerView").GetValue(null);

			var headerItemTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs, groupId,
				(int)styleable.GetField("MvxRecyclerView_HeaderItemTemplate").GetValue(null));
			var footerItemTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs, groupId,
				(int)styleable.GetField("MvxRecyclerView_FooterItemTemplate").GetValue(null));

			adapter.HeaderItemTemplateId = headerItemTemplateId;
			adapter.FooterItemTemplateId = footerItemTemplateId;
		}

		protected CardapiosRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion
	}

}
