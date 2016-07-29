using System.ComponentModel;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Views
{

	public class LeftMenuView : BaseView
	{
		#region Fields

		private readonly MvxSubscriptionToken _clearBackStackToken;

		private readonly MvxSubscriptionToken _currentCriancaChangedToken;

		private readonly MvxSubscriptionToken _logoutToken;

		private readonly MvxSubscriptionToken _closeLeftMenuToken;

		#endregion

		#region Constructors and Destructors

		public LeftMenuView()
		{
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_currentCriancaChangedToken = messenger.Subscribe<CurrentCriancaChangedMessage>(ReceiveCurrentCriancaChangedMessage);
			_clearBackStackToken = messenger.SubscribeOnMainThread<ClearBackStackMessage>(ReceiveClearBackStackMessage);
			_logoutToken = messenger.SubscribeOnMainThread<LogoutMessage>(ReceiveLogoutMessage);
			_closeLeftMenuToken = messenger.SubscribeOnMainThread<CloseLeftMenuMessage>(ReceiveCloseLeftMenuMessage);
		}

		private void ReceiveCloseLeftMenuMessage(CloseLeftMenuMessage obj)
		{
			CloseDrawer();
		}

		#endregion

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.LeftMenuView, null);

			var viewModel = ViewModel as LeftMenuViewModel;
			if(viewModel != null)
			{
				viewModel.PropertyChanged += ViewModel_PropertyChanged;
			}

			return view;
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				var viewModel = ViewModel as LeftMenuViewModel;
				if(viewModel != null)
				{
					viewModel.PropertyChanged -= ViewModel_PropertyChanged;
				}

				var messenger = Mvx.Resolve<IMvxMessenger>();

				if(_currentCriancaChangedToken != null)
				{
					messenger.Unsubscribe<CurrentCriancaChangedMessage>(_currentCriancaChangedToken);
				}

				if(_clearBackStackToken != null)
				{
					messenger.Unsubscribe<ClearBackStackMessage>(_clearBackStackToken);
				}

				if(_logoutToken != null)
				{
					messenger.Unsubscribe<LogoutMessage>(_logoutToken);
				}

				if(_closeLeftMenuToken != null)
				{
					messenger.Unsubscribe<CloseLeftMenuMessage>(_closeLeftMenuToken);
				}
			}
			base.Dispose(disposing);
		}

		private void ReceiveClearBackStackMessage(ClearBackStackMessage obj)
		{
			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if(navigationDrawerBaseView != null)
			{
				navigationDrawerBaseView.AllowEmptyNavigationStack = false;
			}
			ClearBackStack();
		}

		private void ReceiveCurrentCriancaChangedMessage(CurrentCriancaChangedMessage obj)
		{
			CloseDrawer();
		}

		private void CloseDrawer()
		{
			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if(navigationDrawerBaseView != null)
			{
				if(navigationDrawerBaseView.Drawer.IsDrawerOpen(navigationDrawerBaseView.DrawerLeftHolder))
				{
					navigationDrawerBaseView.Drawer.CloseDrawer(navigationDrawerBaseView.DrawerLeftHolder);
				}
			}
		}

		private void ReceiveLogoutMessage(LogoutMessage obj)
		{
			var navigationDrawerBaseView = NavigationDrawerBaseView;
			if(navigationDrawerBaseView != null)
			{
				navigationDrawerBaseView.AllowEmptyNavigationStack = true;
			}
			ClearBackStack();
		}

		private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var bkp = ViewModel;
			ViewModel = null;
			ViewModel = bkp;
		}

		#endregion
	}

}
