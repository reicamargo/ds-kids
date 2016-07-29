using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

using BRFX.Core;
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
using Cirrious.CrossCore.UI;

using DS.Kids.Apps.Core.Messages;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class LoginViewModel : FormViewModel
	{
		private string _email;

		private string _senha;

		private MvxColor _color;

		private static List<MvxColor> _colors;

		public LoginViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("LoginView");
			InitColors();
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

		private void InitColors()
		{
			_colors = new List<MvxColor>
						{
							new MvxColor(238, 76, 91),
							new MvxColor(255, 159, 28),
							new MvxColor(88, 173, 108),
							new MvxColor(85, 124, 215),
							new MvxColor(91, 64, 133)
						};

			EsqueciSenhaViewModel.currentColor = null;	
			Color = PopRandomColor();
		}

		public static MvxColor PopRandomColor(MvxColor previousColor = null)
		{
			var rnd = new Random();

			if (previousColor != null)
			{
				_colors.Add(previousColor);
			}

			var colorId = rnd.Next(_colors.Count);

			var color = _colors[colorId];
			_colors.RemoveAt(colorId);
			return color;
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

		public ICommand EntrarCommand { get; set; }

		public ICommand EsqueciSenhaCommand { get; set; }

		protected override void CreateCommands()
		{
			base.CreateCommands();

			EntrarCommand = new MvxCommand(ExecuteEntrarCommand);
			EsqueciSenhaCommand = new MvxCommand(ExecuteEsqueciSenhaCommand);
		}

		private async void ExecuteEntrarCommand()
		{
			Dictionary<string, List<ValidationAttribute>> dict = null;
			Validator.Validate(this, Email, "Email", ref dict);
			Validator.Validate(this, Senha, "Senha", ref dict);

			var messageBox = Mvx.Resolve<IMessageBox>();

			var firstError = GetFirstError(dict);
			if(firstError != null)
			{
				messageBox.Show(firstError.ValidationMessage);
				return;
			}

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ILogin>();
				var result = await service.LogarAsync(new Login
														{
															Email = Email,
															Senha = Senha
														});

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Logar: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				LoginHelper.SaveUser(result.Data, this);

				var user = LoginHelper.CurrentUser;

				if(user != null)
				{
					if(user.Criancas.Count > 0)
					{
						if(PlatformInstance.Platform == Platform.iOS)
						{
							NavigateTo<HomeViewModel>();
						}
						else
						{
							Messenger.Publish(new ClearBackStackMessage(this));
							NavigateTo<DiarioViewModel>();
						}
					}
					else
					{
						NavigateTo<AdicionarFilhoViewModel>(false);
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Logar: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private void ExecuteEsqueciSenhaCommand()
		{
			NavigateTo<EsqueciSenhaViewModel>(new MvxBundle(new Dictionary<string, string>
																{
																	{
																		"ShowModal", "true"
																	}
																}));
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

	}

}
