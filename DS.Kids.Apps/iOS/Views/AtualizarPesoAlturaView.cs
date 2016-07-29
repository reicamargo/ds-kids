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

	partial class AtualizarPesoAlturaView : FormBaseView, IMvxModalTouchView
	{

		public AtualizarPesoAlturaView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;

			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<AtualizarPesoAlturaView, AtualizarPesoAlturaViewModel>();

			var pickerAlturaViewModel = txtAltura.AddAlturaPicker();
			bindingSet.Bind(txtAltura).To("Format('{0:0.00 m}', Altura)");
			bindingSet.Bind(pickerAlturaViewModel).For(p => p.SelectedItem).To(vm => vm.Altura);
			bindingSet.Bind(pickerAlturaViewModel).For(p => p.ItemsSource).To(vm => vm.AlturasPossiveis);

			var pickerPesoViewModel = txtPeso.AddPesoPicker();
			bindingSet.Bind(txtPeso).To("Format('{0:#0.00 kg}', Peso)");
			bindingSet.Bind(pickerPesoViewModel).For(p => p.SelectedItem).To(vm => vm.Peso);
			bindingSet.Bind(pickerPesoViewModel).For(p => p.ItemsSource).To(vm => vm.PesosPossiveis);

			bindingSet.Bind(lblInformativo).To(vm => vm.Informativo);

			bindingSet.Bind(btnAtualizar).To(vm => vm.AtualizarCommand);
			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

			ExtensionMethods.AddLeftPadding(txtPeso, txtAltura);

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
