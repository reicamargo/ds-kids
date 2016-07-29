using System;

using Android.Animation;
using Android.OS;
using Android.Views;
using Android.Widget;

using BRFX.Core.Droid.Controls;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.Droid.Controls;

using MoPub.MobileAds;
using DS.Kids.Apps.Droid.Ads;

namespace DS.Kids.Apps.Droid.Views
{

	internal class DiarioView : BaseHomeChildView, View.IOnTouchListener, ValueAnimator.IAnimatorUpdateListener
	{
		private PagedHorizontalScrollView _scrollView;
		private View _firstPageBullet;
		private View _secondPageBullet;

		private readonly MvxSubscriptionToken _showDiarioCalendarToken;

        private Ad _adBanner;

        #region Constructors and Destructors

        public DiarioView()
		{
			IsActionBarHomeView = true;
			_showDiarioCalendarToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<ShowDiarioCalendarMessage>(ReceiveShowDiarioCalendarMessage);
		}

		void ReceiveShowDiarioCalendarMessage (ShowDiarioCalendarMessage obj)
		{
			if (obj.Sender == ViewModel) 
			{
				var vm = ViewModel as DiarioViewModel;
				if(vm != null)
				{
					var dialog = new BRFXDatePickerFragment(Activity, vm.Data,
						(s, e) =>
							{
								var newDate = new DateTime(e.Year, e.MonthOfYear + 1, e.DayOfMonth);
								if (newDate.Date <= DateTime.Now.Date)
								{
									vm.Data = newDate;
								}
							}, DateTime.Now);
					dialog.Show(FragmentManager, "date");
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) 
			{	
				if (_showDiarioCalendarToken != null) 
				{
					Mvx.Resolve<IMvxMessenger>().Unsubscribe<ShowDiarioCalendarMessage> (_showDiarioCalendarToken);
				}
			}
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

			SetActionBarElevation(0);

			Title = "Diário";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.DiarioView, null);

			var firstPage = view.FindViewById<LinearLayout>(Resource.Id.diario_firstPage);
			var secondPage = view.FindViewById<LinearLayout>(Resource.Id.diario_secondPage);

			firstPage.LayoutParameters.Width = Resources.DisplayMetrics.WidthPixels;
			secondPage.LayoutParameters.Width = Resources.DisplayMetrics.WidthPixels;

			_firstPageBullet = view.FindViewById(Resource.Id.diario_firstPageBullet);
			_secondPageBullet = view.FindViewById(Resource.Id.diario_secondPageBullet);

			_scrollView = view.FindViewById<PagedHorizontalScrollView>(Resource.Id.diario_viewSwitcher);
			_scrollView.SetOnTouchListener(this);

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if(item.ItemId == Resource.Id.diarioMenu_showDiario)
			{
				var diarioViewModel = ViewModel as DiarioViewModel;
				if(diarioViewModel != null)
				{
					if (diarioViewModel.CalendarioCommand.CanExecute()) {
						diarioViewModel.CalendarioCommand.Execute();
					}
				}
			}

			return base.OnOptionsItemSelected(item);
		}

		public override void OnPrepareOptionsMenu(IMenu menu)
		{
			for(var i = 0; i < menu.Size(); i++)
			{
				var menuItem = menu.GetItem(i);
				menuItem.SetVisible(menuItem.ItemId == Resource.Id.diarioMenu_showDiario);
			}

			base.OnPrepareOptionsMenu(menu);
		}

		public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
		{
			inflater.Inflate(Resource.Menu.diarioMenu, menu);

			base.OnCreateOptionsMenu(menu, inflater);
		}

		public bool OnTouch(View v, MotionEvent e) 
		{
			float currentPosition = _scrollView.ScrollX;
			float pageLengthInPx = Resources.DisplayMetrics.WidthPixels;
			float currentPage = currentPosition/pageLengthInPx;

			bool isBehindHalfScreen = currentPage-(int)currentPage > 0.5;

			float edgePosition;
			if(isBehindHalfScreen)
			{
				edgePosition = (int)(currentPage+1) * pageLengthInPx;

			}
			else
			{
				edgePosition = (int)currentPage * pageLengthInPx;
			}

			if (edgePosition == 0) {
				_firstPageBullet.SetBackgroundResource (Resource.Drawable.carinha_bullet_on);
				_secondPageBullet.SetBackgroundResource (Resource.Drawable.carinha_bullet_off);
			} else {
				_firstPageBullet.SetBackgroundResource (Resource.Drawable.carinha_bullet_off);
				_secondPageBullet.SetBackgroundResource (Resource.Drawable.carinha_bullet_on);
			}

			if(e.Action == MotionEventActions.Up)
			{
				ValueAnimator realSmoothScrollAnimation =  ValueAnimator.OfFloat(currentPosition, edgePosition);
				realSmoothScrollAnimation.SetDuration(500);
				realSmoothScrollAnimation.AddUpdateListener(this);
				realSmoothScrollAnimation.Start();
			}

			return false;
		}


		public void OnAnimationUpdate (ValueAnimator animation)
		{	
			_scrollView.ScrollTo((int)animation.AnimatedValue, 0);
		}
		#endregion
	}
}