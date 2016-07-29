using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.IOS.Views;
using BRFX.Core.Validation;

using Cirrious.MvvmCross.Binding.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	public partial class CriarContaView : FormBaseView
	{

		public CriarContaView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<CriarContaView, CriarContaViewModel>();

			bindingSet.Bind(txtNome).To(vm => vm.Nome).TwoWay();
			bindingSet.Bind(txtEmail).To(vm => vm.Email).TwoWay();
			bindingSet.Bind(txtPassword).To(vm => vm.Senha).TwoWay();
			bindingSet.Bind(txtPasswordConfirmation).To(vm => vm.ConfirmacaoSenha).TwoWay();
			bindingSet.Bind(btnContinuar).To(vm => vm.InserirResponsavelCommand);
			bindingSet.Bind(btnPoliticaPrivacidade).To(vm => vm.PoliticaPrivacidadeCommand);
			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

			btnPoliticaPrivacidade.SetAttributedTitle(new NSAttributedString(btnPoliticaPrivacidade.CurrentTitle,
				underlineStyle: NSUnderlineStyle.Single, foregroundColor: UIColor.White), UIControlState.Normal);

			ExtensionMethods.AddLeftPadding(txtNome, txtEmail, txtPassword, txtPasswordConfirmation);

			bindingSet.Apply();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		protected override void HandleValidation(object sender, Dictionary<string, List<ValidationAttribute>> validationErrors)
		{
			foreach (var validationError in validationErrors)
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
