using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using BRFX.Core.MessageBox;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class BrincadeirasViewModel : BaseHomeChildViewModel
	{
		#region Fields

		private static readonly ObservableCollection<Brincadeira> _brincadeiras = new ObservableCollection<Brincadeira>();

		private readonly MvxSubscriptionToken _logoutToken;

		#endregion
		
		#region Constructors and Destructors

		public BrincadeirasViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Brincadeiras)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("BrincadeirasView");

			_logoutToken = Messenger.Subscribe<LogoutMessage>(ReceiveLogoutMessage);

			LoadBrincadeiras();
		}

		private void ReceiveLogoutMessage(LogoutMessage obj)
		{
			Brincadeiras.Clear();
		}

		public override void OnDispose()
		{
			base.OnDispose();

			if(_logoutToken != null)
			{
				Messenger.Unsubscribe<LogoutMessage>(_logoutToken);
			}
		}

		#endregion

		#region Public Properties

		public ObservableCollection<Brincadeira> Brincadeiras
		{
			get
			{
				return _brincadeiras;
			}
		}

		public MvxCommand<Brincadeira> BrincadeiraSelectedCommand { get; private set; }

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			BrincadeiraSelectedCommand = new MvxCommand<Brincadeira>(ExecuteBrincadeiraSelectedCommand);
		}

		private void ExecuteBrincadeiraSelectedCommand(Brincadeira brincadeira)
		{
			NavigateTo<BrincadeiraViewModel>(brincadeira);
		}

		private async void LoadBrincadeiras()
		{
			if(Brincadeiras.Any())
			{
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();
			try
			{
				var service = new Brincadeiras();
				var result = await service.ObterUltimasBrincadeirasAsync(40, 1);

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Obter últimas brincadeiras: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if(result.Data != null)
				{
					Brincadeiras.Clear();
					foreach(var brincadeira in result.Data)
					{
						Brincadeiras.Add(brincadeira);
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Obter últimas brincadeiras: " + ex);
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
