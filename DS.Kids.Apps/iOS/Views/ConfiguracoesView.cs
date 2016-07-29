using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using CoreGraphics;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class ConfiguracoesView : BaseView, IMvxModalTouchView
	{
		#region Constructors and Destructors

		public ConfiguracoesView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			NavigationController.NavigationBarHidden = false;
			base.ViewWillAppear(animated);
		}

		public override sealed void ViewDidLoad()
		{
			base.ViewDidLoad();

			var backButton = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(0, 0, 34, 34),
				ShowsTouchWhenHighlighted = true,
				ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 0)
			};
			backButton.SetImage(UIImage.FromBundle("fecharazul"), UIControlState.Normal);

			var backBarButtonItem = new UIBarButtonItem(backButton);

			NavigationItem.SetLeftBarButtonItem(backBarButtonItem, true);

			var sobreBarButtonItem = new UIBarButtonItem("Sobre", UIBarButtonItemStyle.Plain, null);

			NavigationItem.SetRightBarButtonItem(sobreBarButtonItem, true);

			NavigationItem.BackBarButtonItem = new UIBarButtonItem
			{
				Title = ""
			};

			var source = new ConfiguracoesCollectionViewSource(collectionView);
			collectionView.Source = source;

			var bindingSet = this.CreateBindingSet<ConfiguracoesView, ConfiguracoesViewModel>();

			bindingSet.Bind(source).To(vm => vm.Criancas);
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.SelecionarFilhoCommand);
			bindingSet.Bind(source).For(s => s.AdicionarFilhoCommand).To(vm => vm.AdicionarFilhoCommand);
			bindingSet.Bind(source).For(s => s.SelectedItem).To(vm => vm.SelectedCrianca);
			bindingSet.Bind(backButton).To(vm => vm.GoBackCommand);

			bindingSet.Bind(sobreBarButtonItem).To(vm => vm.SobreCommand);

			bindingSet.Apply();

			collectionView.ReloadData();
		}

		#endregion
	}

}
