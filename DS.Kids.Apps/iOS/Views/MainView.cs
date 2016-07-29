using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.iOS.Views
{

	public partial class MainView : ProgressView
	{

		public MainView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;

			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<MainView, MainViewModel>();

			bindingSet.Bind(btnFacebook).To(vm => vm.FbLoginCommand);
			bindingSet.Bind(btnEmail).To(vm => vm.EmailLoginCommand);
			bindingSet.Bind(btnCadastrar).To(vm => vm.CadastroCommand);

			bindingSet.Apply();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			ClearBackStack();
		}

	}

}
