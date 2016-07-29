using System;

using Android.Content;
using Android.Runtime;
using Android.Util;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.ResourceHelpers;
using Cirrious.MvvmCross.Binding.Droid.Views;
using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Controls
{

	[Register("ds.kids.apps.droid.controls.DiarioRefeicaoRecyclerView")]
	public class DiarioRefeicaoRecyclerView : HeaderFooterRecyclerView<RefeicaoGrupo>
	{
		#region Constructors and Destructors

		public DiarioRefeicaoRecyclerView(Context context, IAttributeSet attrs)
			: this(context, attrs, 0, new DiarioRefeicaoRecyclerViewAdapter())
		{
		}

		public DiarioRefeicaoRecyclerView(Context context, IAttributeSet attrs, int defStyle)
			: this(context, attrs, defStyle, new DiarioRefeicaoRecyclerViewAdapter())
		{
		}

		public DiarioRefeicaoRecyclerView(Context context, IAttributeSet attrs, int defStyle, DiarioRefeicaoRecyclerViewAdapter adapter)
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

		protected DiarioRefeicaoRecyclerView(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		#endregion
	}

}
