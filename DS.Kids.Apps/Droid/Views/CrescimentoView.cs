using System.ComponentModel;

using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Droid.Ads;
using MoPub.MobileAds;

namespace DS.Kids.Apps.Droid.Views
{

	internal class CrescimentoView : BaseHomeChildView
	{
		#region Fields

		private readonly MvxSubscriptionToken _crescimentosUpdatedToken;

		private CrescimentoViewHeaderItem _crescimentoViewHeaderItem;

		private FrameLayout _listViewFrame;

        private Ads.Ad _adBanner;

        #endregion

        #region Constructors and Destructors

        public CrescimentoView()
		{
			IsActionBarHomeView = true;
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_crescimentosUpdatedToken = messenger.SubscribeOnMainThread<CrescimentosUpdatedMessage>(ReceiveCrescimentosUpdatedMessage);
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

			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if(navigationDrawerBaseView != null)
			{
				navigationDrawerBaseView.Drawer.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
			}

			var mvxNotifyPropertyChanged = ViewModel as MvxNotifyPropertyChanged;
			if(mvxNotifyPropertyChanged != null)
			{
				mvxNotifyPropertyChanged.PropertyChanged += OnPropertyChanged;
			}

			Title = "Crescimento";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.CrescimentoView, null);

			_crescimentoViewHeaderItem = view.FindViewById<CrescimentoViewHeaderItem>(Resource.Id.crescimentoView_crescimentoViewHeaderItem);

			_listViewFrame = view.FindViewById<FrameLayout>(Resource.Id.crescimentoListViewFrame);

			InitListViewFrame();

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_crescimentosUpdatedToken != null)
				{
					var messenger = Mvx.Resolve<IMvxMessenger>();
					messenger.Unsubscribe<CrescimentosUpdatedMessage>(_crescimentosUpdatedToken);
				}
				_crescimentoViewHeaderItem = null;

				var mvxNotifyPropertyChanged = ViewModel as MvxNotifyPropertyChanged;
				if(mvxNotifyPropertyChanged != null)
				{
					mvxNotifyPropertyChanged.PropertyChanged -= OnPropertyChanged;
				}
			}
			base.Dispose(disposing);
		}

		private void InitListViewFrame()
		{
			if(_listViewFrame == null || DataContext == null)
			{
				return;
			}

			var fragment = new CrescimentoViewListFragment
								{
									DataContext = DataContext
								};

			ChildFragmentManager.BeginTransaction()
				.Replace(Resource.Id.crescimentoListViewFrame, fragment)
				.Commit();
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "Crescimentos")
			{
				InitListViewFrame();
			}
		}

		private void ReceiveCrescimentosUpdatedMessage(CrescimentosUpdatedMessage obj)
		{
			if(_crescimentoViewHeaderItem != null)
			{
				_crescimentoViewHeaderItem.UpdateValues();
			}
		}

		#endregion
	}

}
