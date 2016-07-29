using System.Collections.Generic;

using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Controls;
using BRFX.Core.Droid.Validation;
using BRFX.Core.Droid.Views;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{

	internal class AdicionarFilhoView : FormBaseView
	{
		#region Fields

		private BRFXSVGButton _btnSexoFem;

		private BRFXSVGButton _btnSexoMasc;

		private MvxSubscriptionToken _sexChangedToken;

		#endregion

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			HideActionBar();

			base.ConfigureActionBarView(view);

			Title = "Adicionar filho";
		}

		public override bool OnBackPressed()
		{
			var adicionarFilhoViewModel = ViewModel as AdicionarFilhoViewModel;
			if(adicionarFilhoViewModel != null)
			{
				return adicionarFilhoViewModel.GoBackCommand == null
						|| adicionarFilhoViewModel.GoBackCommand.CanExecute();
			}

			return true;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var viewModel = ViewModel as BaseViewModel;
			if(viewModel != null)
			{
				_sexChangedToken = viewModel.Messenger.SubscribeOnMainThread<SexChangedMessage>(ReceiveSexChangedMessage);
			}

			var view = this.BindingInflate(Resource.Layout.AdicionarFilhoView, null);

			var dataNascEditText = view.FindViewById<BRFXEditText>(Resource.Id.adicionarFilho_DataNascimentoEditText);

			dataNascEditText.AddDatePopup<AdicionarFilhoView, AdicionarFilhoViewModel>(this, vm => vm.DataNascimento, "DateFormat");

			_btnSexoFem = view.FindViewById<BRFXSVGButton>(Resource.Id.adicionarFilhoView_icoFem);

			_btnSexoMasc = view.FindViewById<BRFXSVGButton>(Resource.Id.adicionarFilhoView_icoMasc);

			return view;
		}

		#endregion

		#region Methods

		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				var viewModel = ViewModel as BaseViewModel;
				if(viewModel != null)
				{
					viewModel.Messenger.Unsubscribe<SexChangedMessage>(_sexChangedToken);
				}
			}
			base.Dispose(disposing);
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

		private void ReceiveSexChangedMessage(SexChangedMessage obj)
		{
			var viewModel = ViewModel as AdicionarFilhoViewModel;
			if(viewModel != null)
			{
				if(viewModel.Sexo == "F")
				{
					_btnSexoFem.Alpha = 1.0f;
					_btnSexoMasc.Alpha = 0.5f;
				}
				else if(viewModel.Sexo == "M")
				{
					_btnSexoFem.Alpha = 0.5f;
					_btnSexoMasc.Alpha = 1.0f;
				}
			}
		}

		#endregion
	}

}
