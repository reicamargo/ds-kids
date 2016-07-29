using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.iOS.Views
{

	partial class PoliticaPrivacidadeView : BaseView, IMvxModalTouchView
	{
		#region Constructors and Destructors

		public PoliticaPrivacidadeView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<PoliticaPrivacidadeView, PoliticaPrivacidadeViewModel>();

			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);
			bindingSet.Bind(webView).For(v => v.Url).To(vm => vm.Url);

			bindingSet.Apply();
		}

		#endregion
	}

}
