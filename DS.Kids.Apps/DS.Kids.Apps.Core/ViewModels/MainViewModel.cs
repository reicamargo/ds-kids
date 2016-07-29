using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using BRFX.Core;
using BRFX.Core.MessageBox;
using BRFX.Core.Messages;
using BRFX.Core.Plugins;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class MainViewModel : BaseMainViewModel
	{
		#region Fields

		private readonly MvxSubscriptionToken _facebookLoginCompletedToken;

		#endregion

		#region Constructors and Destructors

		public MainViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("MainView");

			_facebookLoginCompletedToken = Messenger.Subscribe<FacebookLoginCompletedMessage>(ReceiveFacebookLoginCompletedMessage);
		}

		#endregion

		#region Public Properties

		public MvxCommand CadastroCommand { get; private set; }

		public MvxCommand EmailLoginCommand { get; set; }

		public MvxCommand FbLoginCommand { get; set; }

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		public override async void Init()
		{
			base.Init();

			if(PlatformInstance.Platform == Platform.Android)
			{
				await Task.Delay(100);

				if(LoginHelper.IsLoggedin())
				{
					Messenger.Publish(new ClearBackStackMessage(this));
					NavigateTo<DiarioViewModel>();
				}
			}
		}

		/// <summary>
		///     Realize tarefas relacionadas com liberação de recursos não gerenciados.
		/// </summary>
		public override void OnDispose()
		{
			base.OnDispose();

			Messenger.Unsubscribe<FacebookLoginCompletedMessage>(_facebookLoginCompletedToken);
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			FbLoginCommand = new MvxCommand(ExecuteFbLoginCommand);
			EmailLoginCommand = new MvxCommand(ExecuteEmailLoginCommand);
			CadastroCommand = new MvxCommand(ExecuteCadastroCommand);

			base.CreateCommands();
		}

		private static void ExecuteFbLoginCommand()
		{
			var facebookPlugin = Mvx.Resolve<IFacebook>();

			if(facebookPlugin.LoginInProgress)
			{
				return;
			}

			facebookPlugin.Authorize(SettingsHelper.FacebookPermissions);
		}

		private void ExecuteCadastroCommand()
		{
			NavigateTo<CriarContaViewModel>();
		}

		private void ExecuteEmailLoginCommand()
		{
			NavigateTo<LoginViewModel>();
		}

		private async void InternalLogarRedeSocial(LoginSocial basicUserInfo, IMessageBox messageBox)
		{
			try
			{
				var service = Mvx.Resolve<ILogin>();
				var result = await service.LogarRedeSocialAsync(basicUserInfo);

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Logar Rede Social: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				LoginHelper.SaveUser(result.Data, this);

				if(PlatformInstance.Platform == Platform.iOS)
				{
					NavigateTo<HomeViewModel>();
				}
				else
				{
					var user = LoginHelper.CurrentUser;

					if(user != null)
					{
						Messenger.Publish(new ClearBackStackMessage(this));
						if(user.Criancas.Count > 0)
						{
							NavigateTo<DiarioViewModel>();
						}
						else
						{
							NavigateTo<AdicionarFilhoViewModel>(false);
						}
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Logar Rede Social: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				Messenger.Publish(new LoadingChangedMessage(this, false));
			}
		}

		private async void ReceiveFacebookLoginCompletedMessage(FacebookLoginCompletedMessage obj)
		{
			var facebookPlugin = Mvx.Resolve<IFacebook>();
			var fbDataPlugin = Mvx.Resolve<IFbData>();
			var messageBox = Mvx.Resolve<IMessageBox>();
			var inputBox = Mvx.Resolve<IInputBox>();

			if(!string.IsNullOrEmpty(facebookPlugin.AccessToken) && obj.Success)
			{
				Messenger.Publish(new LoadingChangedMessage(this, true));

				try
				{
					var basicUserInfo = await fbDataPlugin.GetUserInfoAsync();

					if(basicUserInfo != null && string.IsNullOrEmpty(basicUserInfo.Email))
					{
						Messenger.Publish(new LoadingChangedMessage(this, false));

						const string message = "Parece que seu e-mail não está cadastrado no Facebook. Preencha com seu e-mail atual:";

						Action<bool, string> response = null;
						response = (ok, email) =>
							{
								if(ok)
								{
									if (ValidateEmail(email) == false)
									{
										messageBox.Show("E-mail inválido.", b =>
											{
												inputBox.Show(message, response, buttons: MessageBoxButtons.OkCancel);
											}, "Erro");
										return;
									}

									basicUserInfo.Email = email;

									Messenger.Publish(new LoadingChangedMessage(this, true));
									InternalLogarRedeSocial(basicUserInfo, messageBox);
								}
								else
								{
									messageBox.Show("Login cancelado pelo usuário.");
								}
							};
						inputBox.Show(message, response, buttons: MessageBoxButtons.OkCancel);
					}
					else
					{
						InternalLogarRedeSocial(basicUserInfo, messageBox);
					}
				}
				catch(Exception ex)
				{
					Debug.WriteLine("Error - Logar Rede Social: " + ex);
					messageBox.Log(ex);
				}
				finally
				{
					Messenger.Publish(new LoadingChangedMessage(this, false));
				}
			}
			else
			{
				Debug.WriteLine(obj.Error);
				if(obj.Cancelled)
				{
					messageBox.Show("Login cancelado pelo usuário.");
				}
				else
				{
					messageBox.Show("Erro no login com o facebook.");
				}
			}
		}

		private static bool ValidateEmail(string email)
		{
			if(string.IsNullOrEmpty(email))
			{
				return false;
			}

			if(email.Length > 80)
			{
				return false;
			}

			return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		}

		#endregion
	}

}
