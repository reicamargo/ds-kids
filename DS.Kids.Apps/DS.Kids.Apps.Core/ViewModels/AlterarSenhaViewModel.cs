using System;
using System.Collections.Generic;
using System.Diagnostics;

using BRFX.Core.MessageBox;
using BRFX.Core.Validation;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class AlterarSenhaViewModel : FormViewModel
	{
		#region Fields

		private string _novaSenha;

		private string _novaSenhaConfirmacao;

		private string _senhaAtual;

		private string _titulo;

		#endregion

		#region Constructors and Destructors

		public AlterarSenhaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("AlterarSenhaView");

			Titulo = string.Format("Perfil de {0}", LoginHelper.CurrentUser.Nome);
		}

		#endregion

		#region Public Properties

		[Required("Preencha sua nova Senha.")]
		[MinLength(6, "Tamanho mínimo da senha é de 6 caracteres.")]
		[MaxLength(20, "Tamanho máximo da senha é de 20 caracteres.")]
		public string NovaSenha
		{
			get
			{
				return _novaSenha;
			}
			set
			{
				Set(ref _novaSenha, value);
			}
		}

		[Required("Preencha sua nova Senha novamente.")]
		[MinLength(6, "Tamanho mínimo da senha é de 6 caracteres.")]
		[MaxLength(20, "Tamanho máximo da senha é de 20 caracteres.")]
		public string NovaSenhaConfirmacao
		{
			get
			{
				return _novaSenhaConfirmacao;
			}
			set
			{
				Set(ref _novaSenhaConfirmacao, value);
			}
		}

		public MvxCommand SalvarCommand { get; set; }

		[Required("Preencha sua Senha atual.")]
		[MinLength(6, "Tamanho mínimo da senha é de 6 caracteres.")]
		[MaxLength(20, "Tamanho máximo da senha é de 20 caracteres.")]
		public string SenhaAtual
		{
			get
			{
				return _senhaAtual;
			}
			set
			{
				Set(ref _senhaAtual, value);
			}
		}

		public string Titulo
		{
			get
			{
				return _titulo;
			}
			private set
			{
				Set(ref _titulo, value);
			}
		}

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			SalvarCommand = new MvxCommand(ExecuteSalvarCommand);
		}

		private async void ExecuteSalvarCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, SenhaAtual, "SenhaAtual", ref dict);
			Validator.Validate(this, NovaSenha, "NovaSenha", ref dict);
			Validator.Validate(this, NovaSenhaConfirmacao, "NovaSenhaConfirmacao", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if (firstError != null)
			{
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			if (NovaSenha != NovaSenhaConfirmacao)
			{
				messageBox.Show("Sua confirmação de senha não esta correta! Verifique sua senha e sua confirmação de senha!");
				return;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ISenha>();
				var result = await service.TrocaAsync(new TrocaDeSenha
				{
					IdResponsavel = LoginHelper.CurrentUser.IdResponsavel,
					SenhaAtual = SenhaAtual,
					NovaSenha = NovaSenha
				});

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Trocar Senha: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				messageBox.Show("Senha alterada com sucesso!", ok =>
					{
						Close(this);
					});
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Trocar Senha: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		#endregion
	}

}
