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
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;
using Cirrious.CrossCore.UI;
using DS.Kids.Apps.Core.Helpers;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class EsqueciSenhaViewModel : FormViewModel
	{

		public EsqueciSenhaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("EsqueciSenhaView");
			Color = currentColor = LoginViewModel.PopRandomColor(currentColor);
		}

		internal static MvxColor currentColor;
		
		private string _email;

		private MvxColor _color;

		protected override void CreateCommands()
		{
			base.CreateCommands();

			EnviarCommand = new MvxCommand(ExecuteEnviarCommand);
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

		public MvxColor Color
		{
			get
			{
				return _color;
			}
			set
			{
				SetProperty(ref _color, value);
			}
		}

		public ICommand EnviarCommand { get; set; }

		public async void ExecuteEnviarCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Email, "Email", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if (firstError != null)
			{
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ISenha>();
				var result = await service.EsqueciAsync(Email);

				if (result.ResultCode != ResultCodes.Success)
				{
                    if (result.ResultCode == ResultCodes.ResponsavelNaoEncontrado)
                    {
                        messageBox.Show("E-mail não encontrado!");
                    }
                    else
                    {
                        Debug.WriteLine("Error - Esqueci senha: " + result.ResultMessage);
                        messageBox.Log(result.ResultMessage);
                    }
                    
					return;
				}

				messageBox.Show("Um email foi enviado com mais informações.", ok =>
					{
						ExecuteGoBack();
					});
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Esqueci senha: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

	}

}
