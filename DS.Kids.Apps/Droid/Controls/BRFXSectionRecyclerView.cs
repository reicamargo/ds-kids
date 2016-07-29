using Android.Content;
using Android.Util;

using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Droid.RecyclerView;

namespace DS.Kids.Apps.Droid.Controls
{

	public class BRFXSectionRecyclerView : MvxRecyclerView
    {
		public BRFXSectionRecyclerView(Context context, IAttributeSet attrs)
            : base(context, attrs, new BRFXSectionRecyclerAdapter(context))
        {
			BRFXSectionRecyclerAdapter.GetAttributes(Adapter as BRFXSectionRecyclerAdapter, attrs);
        }

		public BRFXSectionRecyclerView(Context context, IAttributeSet attrs, BRFXSectionRecyclerAdapter sectionListAdapter)
            : base(context, attrs, sectionListAdapter)
        {
			BRFXSectionRecyclerAdapter.GetAttributes(sectionListAdapter, attrs);
        }
    }
}
