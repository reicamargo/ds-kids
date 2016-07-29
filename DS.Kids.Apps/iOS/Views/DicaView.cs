using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using MediaPlayer;

using UIKit;
using ObjCRuntime;

namespace DS.Kids.Apps.iOS.Views
{

	partial class DicaView : ProgressView, IMvxModalTouchView
	{
		
		private NSObject _didEnterFullscreenNotification;
		private NSObject _didExitFullscreenNotification;

		private bool _isFullScreen;

		#region Constructors and Destructors

		public DicaView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		bool IsModal()
		{
			var isModal = ((ParentViewController != null && Equals(ParentViewController.ModalViewController, this)) ||
							//or if I have a navigation controller, check if its parent modal view controller is self navigation controller
							(NavigationController != null && NavigationController.ParentViewController != null && Equals(NavigationController.ParentViewController.ModalViewController, NavigationController)) ||
							//or if the parent of my UITabBarController is also a UITabBarController class, then there is no way to do that, except by using a modal presentation
							(TabBarController != null && TabBarController.ParentViewController is UITabBarController));

			//iOS 5+
			if(!isModal && RespondsToSelector(new Selector("presentingViewController")))
			{
				isModal = ((PresentingViewController != null && Equals(PresentingViewController.ModalViewController, this)) ||
							//or if I have a navigation controller, check if its parent modal view controller is self navigation controller
							(NavigationController != null && NavigationController.PresentingViewController != null && Equals(NavigationController.PresentingViewController.ModalViewController, NavigationController)) ||
							//or if the parent of my UITabBarController is also a UITabBarController class, then there is no way to do that, except by using a modal presentation
							(TabBarController != null && TabBarController.PresentingViewController is UITabBarController));
			}

			return isModal;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// listening
			_didEnterFullscreenNotification = MPMoviePlayerController.Notifications.ObserveDidEnterFullscreen(DidEnterFullscreenCallBack);
			_didExitFullscreenNotification = MPMoviePlayerController.Notifications.ObserveDidExitFullscreen(DidExitFullscreenCallBack);

			var bindingSet = this.CreateBindingSet<DicaView, DicaViewModel>();

			if(IsModal())
			{
				var okBarButtonItem = new UIBarButtonItem("OK", UIBarButtonItemStyle.Plain, null);

				NavigationItem.SetRightBarButtonItem(okBarButtonItem, true);

				bindingSet.Bind(okBarButtonItem).To(vm => vm.GoBackCommand);
			}

			var source = new DicaTableViewSource(tableView);
			tableView.Source = source;

			bindingSet.Bind(source).For(v => v.Dica).To(vm => vm.Dica);

			bindingSet.Apply();
		}

		private void DidEnterFullscreenCallBack(object sender, NSNotificationEventArgs e)
		{
			_isFullScreen = true;
		}

		private void DidExitFullscreenCallBack(object sender, NSNotificationEventArgs e)
		{
			_isFullScreen = false;
		}

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(_didEnterFullscreenNotification != null)
				{
					_didEnterFullscreenNotification.Dispose();
					_didEnterFullscreenNotification = null;
				}
				if(_didExitFullscreenNotification != null)
				{
					_didExitFullscreenNotification.Dispose();
					_didExitFullscreenNotification = null;
				}
			}

			base.Dispose(disposing);
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return _isFullScreen ? UIInterfaceOrientationMask.All : UIInterfaceOrientationMask.Portrait;
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		#endregion
	}

}
