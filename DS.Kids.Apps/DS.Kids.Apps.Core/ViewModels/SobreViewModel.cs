using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.MessageBox;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class SobreViewModel : BaseHomeChildViewModel
	{

		private bool _optin = LoginHelper.CurrentUser.Optin;

		#region Constructors and Destructors

		public SobreViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Sobre)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("SobreView");
		}

		#endregion

		#region Public Properties

		public MvxCommand AlterarSenhaCommand { get; private set; }

		public MvxCommand EquipeCommand { get; private set; }

		public MvxCommand ManifestoCommand { get; private set; }

		public MvxCommand PatrocinioCommand { get; private set; }

		public MvxCommand SairCommand { get; private set; }

		public bool Optin
		{
			get
			{
				return _optin;
			}
			set
			{
				SetOptinChange(value);
			}
		}

        private Parceiro _parceiro;

        public Parceiro Parceiro
        {
            get
            {
                return _parceiro;
            }
            set
            {
                _parceiro = value;
                RaisePropertyChanged(() => Parceiro);
            }
        }

        private async void SetOptinChange(bool newOptin)
		{
			if(newOptin == Optin)
			{
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			Result result = null;
			StartLoading();
			try
			{
				var service = Mvx.Resolve<IOptin>();

				result = await service.SetAsync(new Optin
												  {
													  IdResponsavel = LoginHelper.CurrentUser.IdResponsavel, 
													  OptinPrincipal = newOptin
												  });
				if(result.ResultCode != ResultCodes.Success)
				{
					RaisePropertyChanged(() => Optin);

					Debug.WriteLine("Error - Set Optin: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);

					return;
				}

				Set(ref _optin, newOptin);

				LoginHelper.CurrentUser.Optin = Optin;

				LoginHelper.SaveCurrentUser();
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Set Optin: " + ex);
				messageBox.Log(ex);

				if(result == null || result.ResultCode != ResultCodes.Success)
				{
					RaisePropertyChanged(() => Optin);
				}
			}
			finally
			{
				StopLoading();
			}
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			ManifestoCommand = new MvxCommand(ExecuteManifestoCommand);
			PatrocinioCommand = new MvxCommand(ExecutePatrocinioCommand);
			EquipeCommand = new MvxCommand(ExecuteEquipeCommand);
			SairCommand = new MvxCommand(ExecuteSairCommand);
			AlterarSenhaCommand = new MvxCommand(ExecuteAlterarSenhaCommand);
		}

		private void ExecuteAlterarSenhaCommand()
		{
			NavigateTo<AlterarSenhaViewModel>();
		}

		private void ExecuteEquipeCommand()
		{
			NavigateTo<SobreWebViewModel>(new SobreWebViewModelParams("Equipe", "http://www.dskids.com.br/equipe"));
		}

		private void ExecuteManifestoCommand()
		{
			NavigateTo<SobreWebViewModel>(new SobreWebViewModelParams("Manifesto", "http://www.dskids.com.br/manifesto"));
		}

		private void ExecutePatrocinioCommand()
		{
			NavigateTo<SobreWebViewModel>(new SobreWebViewModelParams("Patrocínio", "http://www.dskids.com.br/patrocinio"));
		}

		private void ExecuteSairCommand()
		{
			var messageBox = Mvx.Resolve<IMessageBox>();
			messageBox.Show("Tem certeza que deseja sair de sua conta?", ok =>
				{
					if(ok)
					{
						LoginHelper.Logout(this);
						Close(this);
						if(ConfiguracoesViewModel.Instance != null)
						{
							Close(ConfiguracoesViewModel.Instance);
						    ConfiguracoesViewModel.Instance = null;
						}
						NavigateTo<MainViewModel>(new MvxBundle(new Dictionary<string, string>
																	{
																		{
																			"ShowInParent", "true"
																		}
																	}));
					}
				}, "Tem certeza?", MessageBoxButtons.OkCancel);
		}

		#endregion
	}

}
