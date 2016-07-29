using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BRFX.Core;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class CrescimentoViewModel : BaseHomeChildViewModel
	{
		#region Fields

		private static ObservableCollection<Crescimento> _staticCrescimentos;

		private readonly ObservableCollection<Crescimento> _crescimentos = new ObservableCollection<Crescimento>();

		private readonly MvxSubscriptionToken _newCrescimentoToken;

		private readonly Timer _updateNotificationTimer;

		private bool _oldPrecisaAtualizar = CrescimentoHelpers.PrecisaAtualizar();

		#endregion

		#region Constructors and Destructors

		public CrescimentoViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Crescimento)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("CrescimentoView");

			_newCrescimentoToken = Messenger.Subscribe<NewCrescimentoMessage>(ReceiveNewCrescimentoMessage);

			_updateNotificationTimer = new Timer(UpdateNotificationTimerHandler, null, 0, 5000);

			_staticCrescimentos = _crescimentos;

			UpdateCrescimentos();
		}

		#endregion

		#region Public Properties

		public MvxCommand<Crescimento> AtualizarPesoAlturaCommand { get; private set; }

		public ObservableCollection<Crescimento> Crescimentos
		{
			get
			{
				return _crescimentos;
			}
		}

		public static ObservableCollection<Crescimento> StaticCrescimentos
		{
			get 
			{
				return _staticCrescimentos;
			}
		}

		#endregion

		#region Public Methods and Operators

		public override void OnDispose()
		{
			base.OnDispose();

			if(_newCrescimentoToken != null)
			{
				Messenger.Unsubscribe<NewCrescimentoMessage>(_newCrescimentoToken);
			}

			if(_updateNotificationTimer != null)
			{
				_updateNotificationTimer.Dispose();
			}
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			AtualizarPesoAlturaCommand = new MvxCommand<Crescimento>(ExecuteAtualizarPesoAlturaCommand, CanExecuteAtualizarPesoAlturaCommand);
		}

		private static bool CanExecuteAtualizarPesoAlturaCommand(Crescimento crescimento)
		{
			return LoginHelper.CurrentCrianca != null;
		}

		private void UpdateNotificationTimerHandler(object state)
		{
			var precisaAtualizar = CrescimentoHelpers.PrecisaAtualizar();
			if (precisaAtualizar != _oldPrecisaAtualizar)
			{
				RaisePropertyChanged(() => Crescimentos);
			}
			_oldPrecisaAtualizar = precisaAtualizar;
		}

		private void ExecuteAtualizarPesoAlturaCommand(Crescimento crescimento)
		{
			NavigateTo<AtualizarPesoAlturaViewModel>(crescimento, new MvxBundle(new Dictionary<string, string>
																					{
																						{
																							"ShowModal", "true"
																						}
																					}));
		}

		protected override void OnCurrentCriancaChanged()
		{
			base.OnCurrentCriancaChanged();

			if(AtualizarPesoAlturaCommand != null)
			{
				AtualizarPesoAlturaCommand.RaiseCanExecuteChanged();

				UpdateCrescimentos();
			}
		}

		private void ReceiveNewCrescimentoMessage(NewCrescimentoMessage obj)
		{
			UpdateCrescimentos();
		}

		private void UpdateCrescimentos()
		{
			Crescimentos.Clear();
			if(LoginHelper.CurrentCrianca != null)
			{
				foreach(var crescimento in LoginHelper.CurrentCrianca.Crescimentos.OrderByDescending(c => c.DataCriacao))
				{
					Crescimentos.Add(crescimento);
				}
			}

			Messenger.Publish(new CrescimentosUpdatedMessage(this));
		}

		#endregion
	}

}
