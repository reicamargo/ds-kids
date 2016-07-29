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

	partial class AlterarSenhaView : FormBaseView
	{
		#region Constructors and Destructors

		public AlterarSenhaView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<AlterarSenhaView, AlterarSenhaViewModel>();

			var selecionarFilhoBarButtonItem = new UIBarButtonItem("Salvar", UIBarButtonItemStyle.Plain, null);

			NavigationItem.SetRightBarButtonItem(selecionarFilhoBarButtonItem, true);
			bindingSet.Bind(selecionarFilhoBarButtonItem).To(vm => vm.SalvarCommand);

			bindingSet.Bind(this).For(b => b.Title).To(vm => vm.Titulo);

			bindingSet.Bind(txtCurrentPassword).To(vm => vm.SenhaAtual).TwoWay();
			bindingSet.Bind(txtNewPassword).To(vm => vm.NovaSenha).TwoWay();
			bindingSet.Bind(txtNewPasswordConfirmation).To(vm => vm.NovaSenhaConfirmacao).TwoWay();

			ExtensionMethods.AddLeftPadding(175, 5, txtCurrentPassword, txtNewPassword, txtNewPasswordConfirmation);

			bindingSet.Apply();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void ViewWillAppear(bool animated)
		{
			NavigationController.NavigationBarHidden = false;
			base.ViewWillAppear(animated);
		}

		#endregion

		#region Methods

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

		#endregion
	}

}
