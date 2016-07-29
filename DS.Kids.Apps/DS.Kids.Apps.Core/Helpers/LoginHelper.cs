using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BRFX.Core;
using BRFX.Core.Plugins;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;
using DS.Kids.Model.Communication;

namespace DS.Kids.Apps.Core.Helpers
{

	public static class LoginHelper
	{
		#region Constants

		public const string SimpleDataFile = "simpleData.dat";

		private const string CriancaFile = "crianca.dat";

		private const string LoginFile = "login.dat";

		#endregion

		#region Constructors and Destructors

		static LoginHelper()
		{
			var fileManager = Mvx.Resolve<IMvxFileStore>();

			if(fileManager.Exists(LoginFile))
			{
				using(var file = fileManager.OpenRead(LoginFile))
				{
					if(file != null)
					{
						var data = new byte[file.Length];
						file.Read(data, 0, data.Length);
						var fileString = Encoding.UTF8.GetString(data, 0, data.Length);
						try
						{
							CurrentUser = fileString.FromJson<Responsavel>();
						}
						catch (Exception)
						{
							// ignored
						}

						if(CurrentUser != null)
						{
							Authorization.Singleton.SetToken(CurrentUser.Token.Valor);
						}
					}
				}
			}

			if(fileManager.Exists(CriancaFile))
			{
				using(var file = fileManager.OpenRead(CriancaFile))
				{
					if(file != null)
					{
						var data = new byte[file.Length];
						file.Read(data, 0, data.Length);
						var fileString = Encoding.UTF8.GetString(data, 0, data.Length);
						CurrentCriancaId = fileString.FromJson<int>();
					}
				}
			}

			UpdateCurrentCrianca(CurrentCriancaId, null);
		}

		#endregion

		#region Public Properties

		public static Crianca CurrentCrianca { get; set; }

		public static Responsavel CurrentUser { get; set; }

		#endregion

		#region Properties

		private static int CurrentCriancaId { get; set; }

		#endregion

		#region Public Methods and Operators

		public static void AddCrianca(Crianca crianca, object sender)
		{
			if(crianca == null)
			{
				return;
			}

			if(CurrentUser.Criancas == null)
			{
				CurrentUser.Criancas = new List<Crianca>();
			}

			CurrentUser.Criancas.Add(crianca);
			UpdateCurrentCrianca(crianca.IdCrianca, sender);

			SaveCurrentUser();
		}

		public static bool IsLoggedin()
		{
			return CurrentUser != null;
		}

		public static void Logout(BaseViewModel sender)
		{
			var fileManager = Mvx.Resolve<IMvxFileStore>();
			if(fileManager.Exists(LoginFile))
			{
				fileManager.DeleteFile(LoginFile);
			}
			if(fileManager.Exists(CriancaFile))
			{
				fileManager.DeleteFile(CriancaFile);
			}

			var localNotifications = Mvx.Resolve<ILocalNotifications>();
			localNotifications.CancelAllNotifications();

			CurrentUser = null;
			CurrentCrianca = null;

			Authorization.Singleton.KillToken();

			//SettingsHelper.Delete();

			var facebookPlugin = Mvx.Resolve<IFacebook>();
			facebookPlugin.Logout();

			var messenger = Mvx.Resolve<IMvxMessenger>();
			messenger.Publish(new LogoutMessage(sender));
		}

		public static void RemoveCrianca(Crianca crianca, object sender)
		{
			if(crianca == null)
			{
				return;
			}

			if(CurrentUser.Criancas == null)
			{
				CurrentUser.Criancas = new List<Crianca>();
			}

			CurrentUser.Criancas.Remove(crianca);

			UpdateCurrentCrianca(-1, sender);

			SaveCurrentUser();
		}

		public static void SaveCurrentUser()
		{
			var fileManager = Mvx.Resolve<IMvxFileStore>();
			fileManager.WriteFile(LoginFile, CurrentUser.ToJson());

			SaveCurrentCrianca();
		}

		public static void SaveUser(Responsavel user, object sender)
		{
			// User is Logged in
			CurrentUser = user;
			UpdateCurrentCrianca(CurrentCriancaId, sender);

			SaveCurrentUser();

			//SettingsHelper.Update();

			var messenger = Mvx.Resolve<IMvxMessenger>();
			messenger.Publish(new CurrentUserChangedMessage(sender));

			//var notificationService = Mvx.Resolve<INotificationService>();
			//notificationService.SendNotificationId();
		}

		public static void UpdateCrianca(Crianca newCrianca, object sender)
		{
			var oldCrianca = CurrentUser.Criancas.FirstOrDefault(c => c.IdCrianca == newCrianca.IdCrianca);
			if(oldCrianca != null)
			{
				CurrentUser.Criancas.Remove(oldCrianca);
				CurrentUser.Criancas.Add(newCrianca);

				UpdateCurrentCrianca(CurrentCriancaId, sender);

				SaveCurrentUser();
			}
		}

		public static void UpdateCurrentCrianca(int id, object sender)
		{
			CurrentCrianca = null;
			if(CurrentUser != null && CurrentUser.Criancas != null)
			{
				CurrentCrianca = CurrentUser.Criancas.LastOrDefault(c => c.IdCrianca == id);
				if(CurrentCrianca != null)
				{
					CurrentCriancaId = CurrentCrianca.IdCrianca;
				}
				else
				{
					CurrentCrianca = CurrentUser.Criancas.LastOrDefault();
				}
			}

			if(sender is AlterarFilhoViewModel || sender is LeftMenuViewModel)
			{
				SaveCurrentCrianca();
			}

			if(sender != null)
			{
				var messenger = Mvx.Resolve<IMvxMessenger>();
				messenger.Publish(new CurrentCriancaChangedMessage(sender));
			}
		}

		#endregion

		#region Methods

		private static void SaveCurrentCrianca()
		{
			var fileManager = Mvx.Resolve<IMvxFileStore>();
			fileManager.WriteFile(CriancaFile, CurrentCriancaId.ToJson());

			WriteSimpleDataFile();
		}

		private static void WriteSimpleDataFile()
		{
			// Somente salva estes dados para o LocalNotification do Android
			// poder reagendar a mensagem sem inicializar o MVVMCross
			if(PlatformInstance.Platform != Platform.Android || CurrentCrianca == null)
			{
				return;
			}

			var fileManager = Mvx.Resolve<IMvxFileStore>();
			fileManager.WriteFile(SimpleDataFile, new SimpleDataFile
													{
														CurrentCriancaId = CurrentCrianca.IdCrianca,
														Body = CrescimentoHelpers.Body,
														StartTime = CrescimentoHelpers.AlertStartTime
													}.ToJson());
		}

		#endregion
	}

	public class SimpleDataFile
	{
		#region Public Properties

		public string Body { get; set; }

		public int CurrentCriancaId { get; set; }

		public DateTime StartTime { get; set; }

		#endregion
	}

}
