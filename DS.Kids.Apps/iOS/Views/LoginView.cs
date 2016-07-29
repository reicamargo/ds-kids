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

	partial class LoginView : FormBaseView
	{

		private static List<UIColor> _colors;

		internal UIColor BgColor;

		public LoginView(IntPtr handle)
			: base(handle)
		{
		}

		private void InitColors()
		{
			_colors = new List<UIColor>
						{
							UIColor.FromRGB(238, 76, 91),
							UIColor.FromRGB(255, 159, 28),
							UIColor.FromRGB(88, 173, 108),
							UIColor.FromRGB(85, 124, 215),
							UIColor.FromRGB(91, 64, 133)
						};
			EsqueciSenhaView.currentColor = null;
			BgColor = PopRandomColor();
		}

		public static UIColor PopRandomColor(UIColor previousColor = null)
		{
			var rnd = new Random();

			if(previousColor != null)
			{
				_colors.Add(previousColor);
			}

			var colorId = rnd.Next(_colors.Count);

			var color = _colors[colorId];
			_colors.RemoveAt(colorId);
			return color;
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;
			base.ViewDidLoad();

			InitColors();

			var bindingSet = this.CreateBindingSet<LoginView, LoginViewModel>();

			bindingSet.Bind(txtEmail).To(vm => vm.Email).TwoWay();
			bindingSet.Bind(txtPassword).To(vm => vm.Senha).TwoWay();
			bindingSet.Bind(btnEntrar).To(vm => vm.EntrarCommand);
			bindingSet.Bind(btnEsqueceuSenha).To(vm => vm.EsqueciSenhaCommand);
			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

			View.BackgroundColor = BgColor;
			btnEntrar.SetTitleColor(BgColor, UIControlState.Normal);
			btnEsqueceuSenha.SetTitleColor(BgColor, UIControlState.Normal);

			ExtensionMethods.AddLeftPadding(txtEmail, txtPassword);

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
