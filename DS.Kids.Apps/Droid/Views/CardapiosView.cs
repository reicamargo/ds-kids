using Android.OS;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace DS.Kids.Apps.Droid.Views
{

	internal class CardapiosView : BaseHomeChildView
	{
		#region Constructors and Destructors

		public CardapiosView()
		{
			IsActionBarHomeView = true;
		}

		#endregion

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			ShowActionBar();

			base.ConfigureActionBarView(view);

			Title = "Cardápios";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.CardapiosView, null);

			return view;
		}

		#endregion
	}

}
