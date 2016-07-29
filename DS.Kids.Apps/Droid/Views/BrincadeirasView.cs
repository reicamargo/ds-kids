using Android.OS;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.RecyclerView;
using Android.Support.V7.Widget;
using DS.Kids.Apps.Droid.Ads;
using MoPub.MobileAds;

namespace DS.Kids.Apps.Droid.Views
{

	internal class BrincadeirasView : BaseHomeChildView
	{
        MoPubInterstitial _interstitial;
        private Ad _adBanner;

        #region Constructors and Destructors

        public BrincadeirasView()
		{
			IsActionBarHomeView = true;
		}

        public override void OnDestroy()
        {
            if (_interstitial != null) _interstitial.Destroy();

            if (_adBanner != null) _adBanner.Destroy();

            base.OnDestroy();
        }
        #endregion

        #region Public Methods and Operators

        public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Brincadeiras";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.BrincadeirasView, null);

			var recyclerView = view.FindViewById<MvxRecyclerView> (Resource.Id.brincadeiras_recyclerView);
			recyclerView.SetLayoutManager (new GridLayoutManager(Activity, 2));

            _interstitial = new MoPubInterstitial(Activity, MoPubconfig.AD_UNIT_ID_FULLSCREEN);
            _interstitial.InterstitialLoaded += _interstitial_InterstitialLoaded;
            _interstitial.Load();

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
		}

        private void _interstitial_InterstitialLoaded(object sender, MoPubInterstitial.InterstitialLoadedEventArgs e)
        {
            MoPubInterstitial interSafadao = (MoPubInterstitial)sender;
            if (interSafadao.IsReady)
                interSafadao.Show();
        }

        #endregion
    }

}
