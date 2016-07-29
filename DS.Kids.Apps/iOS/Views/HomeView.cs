using System;

using BRFX.Core.IOS.Views;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.ViewModels;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class HomeView : BaseTabView
	{

		public HomeView(IntPtr handle)
			: base(handle)
		{
			// need this additional call to ViewDidLoad because UIKit creates the view before the C# hierarchy has been constructed
			ViewDidLoad();
		}

		public override void ViewWillAppear(bool animated)
		{
			if (NavigationController != null)
			{
				NavigationController.NavigationBarHidden = true;
			}
			base.ViewWillAppear(animated);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			ClearBackStack();
		}

		public override sealed void ViewDidLoad()
		{
			base.ViewDidLoad();

			var homeViewModel = ViewModel as HomeViewModel;

			if(homeViewModel != null)
			{
				var viewControllers = new[]
										{
											CreateTabFor("Diário", homeViewModel.DiarioViewModel, "bt0", "bt0on", true),
											CreateTabFor("Cardápios", homeViewModel.CardapiosViewModel, "bt1", "bt1on", true),
											CreateTabFor("Crescimento", homeViewModel.CrescimentoViewModel, "bt2", "bt2on", true),
											CreateTabFor("Brincadeiras", homeViewModel.BrincadeirasViewModel, "bt3", "bt3on", true),
											CreateTabFor("Dicas", homeViewModel.CategoriasViewModel, "bt4", "bt4on", true)
										};
				ViewControllers = viewControllers;
				CustomizableViewControllers = new UIViewController[]
												{
												};
				var showCrescimentoTab = AppDelegate.startedFromNotification || CrescimentoHelpers.PrecisaAtualizar();
				SelectedViewController = ViewControllers[showCrescimentoTab ? 2 : 0];
				AppDelegate.startedFromNotification = false;
			}
		}

	}

}
