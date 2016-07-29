using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.IOS.Views;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;

using Foundation;

using UIKit;
using CoreGraphics;

namespace DS.Kids.Apps.iOS.Views
{

	partial class AdicionarFilhoView : FormBaseView, IUIPickerViewDelegate, IMvxModalTouchView
	{

		private MvxSubscriptionToken _sexChangedToken;

		private IMvxTouchModalHost _oldModalHost;

		public AdicionarFilhoView(IntPtr handle)
			: base(handle)
		{
		}

		public override void OnLoad()
		{
			base.OnLoad();

			var viewModel = ViewModel as BaseViewModel;
			if(viewModel != null)
			{
				_sexChangedToken = viewModel.Messenger.SubscribeOnMainThread<SexChangedMessage>(ReceiveSexChangedMessage);
			}

			_oldModalHost = Mvx.Resolve<IMvxTouchModalHost>();
			Mvx.RegisterSingleton<IMvxTouchModalHost>(new TemporaryPresenter(NavigationController));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			Mvx.RegisterSingleton(_oldModalHost);
		}

		private void ReceiveSexChangedMessage(SexChangedMessage obj)
		{
			var viewModel = ViewModel as AdicionarFilhoViewModel;
			if(viewModel != null)
			{
				if(viewModel.Sexo == "F")
				{
					btnSexoFem.Alpha = 1.0f;
					btnSexoMasc.Alpha = 0.5f;
				}
				else if(viewModel.Sexo == "M")
				{
					btnSexoFem.Alpha = 0.5f;
					btnSexoMasc.Alpha = 1.0f;
				}
			}
		}

		public override void OnUnload()
		{
			var viewModel = ViewModel as BaseViewModel;
			if(viewModel != null)
			{
				viewModel.Messenger.Unsubscribe<SexChangedMessage>(_sexChangedToken);
			}

			base.OnUnload();
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;

			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<AdicionarFilhoView, AdicionarFilhoViewModel>();

			bindingSet.Bind(txtNome).To(vm => vm.Nome).TwoWay();

			var pickerAlturaViewModel = txtAltura.AddAlturaPicker();
			bindingSet.Bind(txtAltura).To("Format('{0:0.00 m}', Altura)");
			bindingSet.Bind(pickerAlturaViewModel).For(p => p.SelectedItem).To(vm => vm.Altura);
			bindingSet.Bind(pickerAlturaViewModel).For(p => p.ItemsSource).To(vm => vm.AlturasPossiveis);

			var pickerPesoViewModel = txtPeso.AddPesoPicker();
			bindingSet.Bind(txtPeso).To("Format('{0:#0.00 kg}', Peso)");
			bindingSet.Bind(pickerPesoViewModel).For(p => p.SelectedItem).To(vm => vm.Peso);
			bindingSet.Bind(pickerPesoViewModel).For(p => p.ItemsSource).To(vm => vm.PesosPossiveis);

			var datePicker = txtDataNascimento.AddDateTimePicker();
			bindingSet.Bind(datePicker).To(vm => vm.DataNascimento);
			bindingSet.Bind(txtDataNascimento).To("Format('{0:dd MMM yyyy}', DataNascimento)");

			bindingSet.Bind(btnCamera).To(vm => vm.SelectPhotoCommand);
			bindingSet.Bind(btnSexoFem).To(vm => vm.SelectSexoCommand).CommandParameter("F");
			bindingSet.Bind(btnSexoMasc).To(vm => vm.SelectSexoCommand).CommandParameter("M");
			bindingSet.Bind(btnConcluir).To(vm => vm.ConcluirCommand);

			var esconderBotaoFechar = LoginHelper.CurrentUser.Criancas.Count == 0;

			btnClose.Hidden = esconderBotaoFechar;

			bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

			bindingSet.Bind(imgFilho).To(vm => vm.PictureBytes).WithConversion("InMemoryImage");
			bindingSet.Bind(imgFilho).For("Visibility").To(vm => vm.PictureBytes).WithConversion("Visibility");

			imgFilho.Layer.CornerRadius = 54;
			imgFilho.Layer.BorderColor = UIColor.White.CGColor;
			imgFilho.Layer.BorderWidth = 2;
			imgFilho.Layer.MasksToBounds = true;
			imgFilho.ClipsToBounds = true;

			// Se for iPhone 4S ou inferior
			CGRect frame;
			if(UIScreen.MainScreen.Bounds.Height == 480)
			{
				frame = SexoBox.Frame;
				frame.Y = 375;
				SexoBox.Frame = frame;
				frame = btnConcluir.Frame;
				frame.Y = 420;
				btnConcluir.Frame = frame;
			}
			else
			{
				frame = SexoBox.Frame;
				frame.Y = 385;
				SexoBox.Frame = frame;
				frame = btnConcluir.Frame;
				frame.Y = 460;
				btnConcluir.Frame = frame;
			}

			ExtensionMethods.AddLeftPadding(txtNome, txtAltura, txtPeso, txtDataNascimento);

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

		public class TemporaryPresenter : IMvxTouchModalHost
		{

			private readonly UINavigationController _navigationController;

			public TemporaryPresenter(UINavigationController navigationController)
			{
				_navigationController = navigationController;
			}

			public bool PresentModalViewController(UIViewController controller, bool animated)
			{
				_navigationController.PresentModalViewController(controller, animated);
				return true;
			}

			public void NativeModalViewControllerDisappearedOnItsOwn()
			{
				Debug.WriteLine("Dismissed");
			}

		}

	}

}
