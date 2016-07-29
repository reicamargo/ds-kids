using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.IOS.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class EsqueciSenhaView : FormBaseView, IMvxModalTouchView
	{

		internal static UIColor currentColor;

		public EsqueciSenhaView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;
			base.ViewDidLoad();

			currentColor = LoginView.PopRandomColor(currentColor);

			View.BackgroundColor = currentColor;
			btnEnviar.SetTitleColor(currentColor, UIControlState.Normal);

			var bindingSet = this.CreateBindingSet<EsqueciSenhaView, EsqueciSenhaViewModel>();

			bindingSet.Bind(txtEmail).To(vm => vm.Email).TwoWay();
			bindingSet.Bind(btnEnviar).To(vm => vm.EnviarCommand);
			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

			txtEmail.AddLeftPadding();

			bindingSet.Apply();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		protected override void HandleValidation(object sender, Dictionary<string, List<ValidationAttribute>> validationErrors)
		{
			foreach(var validationError in validationErrors)
			{
				if(validationError.Value != null)
				{
					foreach(var validationAttribute in validationError.Value)
					{
						Debug.WriteLine(validationAttribute.ValidationMessage);
					}
				}
			}
		}

	}

}
