using System;

using BRFX.Core.IOS;
using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class SobreView : ProgressView
	{
		#region Constructors and Destructors

		public SobreView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationController.NavigationBarHidden = false;

			NavigationItem.BackBarButtonItem = new UIBarButtonItem
			{
				Title = ""
			};

			ScrollView.FitContent();

			var bindingSet = this.CreateBindingSet<SobreView, SobreViewModel>();

			bindingSet.Bind(btnManifesto).To(vm => vm.ManifestoCommand);

			bindingSet.Bind(btnSair).To(vm => vm.SairCommand);
			bindingSet.Bind(btnAlterarSenha).To(vm => vm.AlterarSenhaCommand);

			bindingSet.Bind(switchOptin).To(vm => vm.Optin);

			bindingSet.Apply();
		}

		#endregion
	}

}
