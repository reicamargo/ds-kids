using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace DS.Kids.Apps.Droid.Views
{

	public class CrescimentoViewListFragment : BaseView
	{
		#region Public Methods and Operators

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.CrescimentoViewListFragment, null);

			return view;
		}

		#endregion
	}

}
