using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace DS.Kids.Apps.Droid.Views
{

	internal class BrincadeiraView : BaseView
	{
		private bool _showingCustomToolbar;

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			if (_showingCustomToolbar)
			{
				return;
			}

			HideActionBar();

			base.ConfigureActionBarView(view);

			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if (navigationDrawerBaseView != null)
			{
				navigationDrawerBaseView.DrawerToggle.DrawerIndicatorEnabled = false;
			}

			Title = "";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.BrincadeiraView, null);

			var brincadeiraViewModel = ViewModel as BrincadeiraViewModel;

			if (brincadeiraViewModel != null)
			{
				var collapsingToolbar = view.FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsing_toolbar);
				collapsingToolbar.Title = brincadeiraViewModel.Brincadeira.Titulo;
			}

			var toolbar = view.FindViewById<Toolbar>(Resource.Id.internal_toolbar);
			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if (navigationDrawerBaseView != null)
			{
				_showingCustomToolbar = true;
				navigationDrawerBaseView.SetSupportActionBar(toolbar);
				ShowActionBar();

				navigationDrawerBaseView.DrawerToggle.DrawerIndicatorEnabled = false;
				navigationDrawerBaseView.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			}

			return view;
		}

		public override bool OnBackPressed()
		{
			var back = base.OnBackPressed();
			if (back)
			{
				var oldActionBar = NavigationDrawerBaseView.FindViewById<Toolbar>(Resource.Id.toolbar);
				NavigationDrawerBaseView.SetSupportActionBar(oldActionBar);
				_showingCustomToolbar = false;
			}

			return back;
		}

		#endregion
	}

}
