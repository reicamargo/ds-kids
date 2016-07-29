using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

namespace DS.Kids.Apps.Droid.Views
{

	internal class PoliticaPrivacidadeView : BaseView
	{
		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			HideActionBar();

			base.ConfigureActionBarView(view);

			Title = "Política de Privacidade";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.PoliticaPrivacidadeView, null);

			return view;
		}

		#endregion
	}

}
