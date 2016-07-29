using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.IOS.Views;
using BRFX.Core.Validation;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class AlterarFilhoView : FormBaseView, IUIPickerViewDelegate
	{
		#region Constructors and Destructors

		private IMvxTouchModalHost _oldModalHost;

		public AlterarFilhoView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void OnLoad()
		{
			base.OnLoad();

			_oldModalHost = Mvx.Resolve<IMvxTouchModalHost>();
			Mvx.RegisterSingleton<IMvxTouchModalHost>(new AdicionarFilhoView.TemporaryPresenter(NavigationController));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			Mvx.RegisterSingleton(_oldModalHost);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<AlterarFilhoView, AlterarFilhoViewModel>();

			var selecionarFilhoBarButtonItem = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);

			NavigationItem.SetRightBarButtonItem(selecionarFilhoBarButtonItem, true);
			bindingSet.Bind(selecionarFilhoBarButtonItem).For(b => b.Title).To(vm => vm.SelecionarButtonText);
			bindingSet.Bind(selecionarFilhoBarButtonItem).To(vm => vm.SelecionarOuSalvarCommand);
			bindingSet.Bind(this).For(v => v.Title).To("Format('Perfil de {0}', Crianca.Nome)");

			bindingSet.Bind(txtNome).To(vm => vm.Nome).TwoWay();

			var datePicker = txtDataNascimento.AddDateTimePicker();
			bindingSet.Bind(datePicker).To(vm => vm.DataNascimento);
			bindingSet.Bind(txtDataNascimento).To("Format('{0:dd MMM yyyy}', DataNascimento)");

			bindingSet.Bind(btnCamera).To(vm => vm.SelectPhotoCommand);
			bindingSet.Bind(imgFotoFilho).For(v => v.ImageUrl).To(vm => vm.Crianca.UrlImagem).WithConversion("RelativeToAbsoluteUrl");
			bindingSet.Bind(imgFotoFilho).For("Visibility").To(vm => vm.PictureBytes).WithConversion("InvertedVisibility");
			bindingSet.Bind(imgFilho).To(vm => vm.PictureBytes).WithConversion("InMemoryImage");
			bindingSet.Bind(imgFilho).For("Visibility").To(vm => vm.PictureBytes).WithConversion("Visibility");
			bindingSet.Bind(btnExcluir).For("Title").To("Format('Excluir perfil de {0}', Crianca.Nome)");
			bindingSet.Bind(btnExcluir).To(vm => vm.ExcluirCommand);

			MakeRound(imgFotoFilho);
			MakeRound(imgFilho);

			ExtensionMethods.AddLeftPadding(175, 5, txtNome, txtDataNascimento);

			bindingSet.Apply();
		}

		private static void MakeRound(UIImageView imageView)
		{
			imageView.Layer.CornerRadius = 45;
			imageView.Layer.BorderColor = UIColor.White.CGColor;
			imageView.Layer.BorderWidth = 2;
			imageView.Layer.MasksToBounds = true;
			imageView.ClipsToBounds = true;
		}

		public override void ViewWillAppear(bool animated)
		{
			NavigationController.NavigationBarHidden = false;
			base.ViewWillAppear(animated);
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
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
