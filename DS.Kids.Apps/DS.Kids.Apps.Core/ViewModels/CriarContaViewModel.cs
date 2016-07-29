using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

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

	public class CriarContaViewModel : FormViewModel
	{
		#region Fields

		private string _confirmacaoSenha;

		private string _email;

		private string _nome;

		private string _senha;

		#endregion

		#region Constructors and Destructors

		public CriarContaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("CriarContaView");
		}

		#endregion

		#region Public Properties

		[Required("Preencha sua confirmação de Senha.")]
		[MinLength(6, "Tamanho mínimo da senha é de 6 caracteres.")]
		[MaxLength(20, "Tamanho máximo da senha é de 20 caracteres.")]
		public string ConfirmacaoSenha
		{
			get
			{
				return _confirmacaoSenha;
			}
			set
			{
				Set(ref _confirmacaoSenha, value);
			}
		}

		[Required("Preencha seu e-mail.")]
		[MaxLength(80, "Tamanho máximo do e-mail é de 80 caracteres.")]
		[DataType(DataType.EmailAddress, "E-mail inválido.")]
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				Set(ref _email, value);
			}
		}

		public ICommand InserirResponsavelCommand { get; private set; }

		[Required("Preencha seu nome.")]
		[MaxLength(50, "Tamanho máximo do nome é de 50 caracteres.")]
		public string Nome
		{
			get
			{
				return _nome;
			}
			set
			{
				Set(ref _nome, value);
			}
		}

		public ICommand PoliticaPrivacidadeCommand { get; private set; }

		[Required("Preencha sua Senha.")]
		[MinLength(6, "Tamanho mínimo da senha é de 6 caracteres.")]
		[MaxLength(20, "Tamanho máximo da senha é de 20 caracteres.")]
		public string Senha
		{
			get
			{
				return _senha;
			}
			set
			{
				Set(ref _senha, value);
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

			InserirResponsavelCommand = new MvxCommand(ExecuteInserirResponsavelCommand);
			PoliticaPrivacidadeCommand = new MvxCommand(ExecutePoliticaPrivacidadeCommand);
		}

		private async void ExecuteInserirResponsavelCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Nome, "Nome", ref dict);
			Validator.Validate(this, Email, "Email", ref dict);
			Validator.Validate(this, Senha, "Senha", ref dict);
			Validator.Validate(this, ConfirmacaoSenha, "ConfirmacaoSenha", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if(firstError != null)
			{
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			if(Senha != ConfirmacaoSenha)
			{
				messageBox.Show("Sua confirmação de senha não esta correta! Verifique sua senha e sua confirmação de senha!");
				return;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<IResponsavel>();
				var result = await service.InserirAsync(new Responsavel
															{
																Nome = Nome,
																Email = Email,
																Senha = Senha
															});

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Inserir Responsável: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				LoginHelper.SaveUser(result.Data, this);

				NavigateTo<AdicionarFilhoViewModel>(false);
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Inserir Responsável: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private void ExecutePoliticaPrivacidadeCommand()
		{
			NavigateTo<PoliticaPrivacidadeViewModel>(new MvxBundle(new Dictionary<string, string>
															{
																{
																	"ShowModal", "true"
																}
															}));
		}

		#endregion
	}

}
