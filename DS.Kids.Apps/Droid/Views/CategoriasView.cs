using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.RecyclerView;

using MoPub.MobileAds;
using DS.Kids.Apps.Droid.Ads;

namespace DS.Kids.Apps.Droid.Views
{

	internal class CategoriasView : BaseHomeChildView
	{
        private Ad _adBanner;

        #region Constructors and Destructors

        public CategoriasView()
		{
			IsActionBarHomeView = true;
		}

        public override void OnDestroy()
        {
            if (_adBanner != null) _adBanner.Destroy();

            base.OnDestroy();
        }

        #endregion

        #region Public Methods and Operators

        public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Dicas";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.CategoriasView, null);

			var categoriasRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.categorias_gridView);

			categoriasRecyclerView.SetLayoutManager(new StaggeredGridLayoutManager(3, StaggeredGridLayoutManager.Vertical));

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
		}

		#endregion
	}

}
