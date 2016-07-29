using System.Collections.Generic;

using BRFX.Core.ViewModels;

using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class BaseHomeChildViewModel : ProgressViewModel
	{

		private readonly LeftMenuViewModel.LeftMenuIndex _leftMenuIndex;

		#region Fields

		private readonly MvxSubscriptionToken _currentCriancaChangedToken;

		private Crianca _crianca;

		#endregion

		#region Constructors and Destructors

		protected BaseHomeChildViewModel(LeftMenuViewModel.LeftMenuIndex leftMenuIndex)
		{
			_leftMenuIndex = leftMenuIndex;
			_currentCriancaChangedToken = Messenger.Subscribe<CurrentCriancaChangedMessage>(ReceiveCurrentCriancaChangedMessage);

			Crianca = LoginHelper.CurrentCrianca;
		}

		public void Displayed()
		{
			Messenger.Publish(new LeftMenuIndexChangedMessage(this, _leftMenuIndex));
		}

		#endregion

		#region Public Properties

		public MvxCommand ConfiguracoesCommand { get; private set; }

		public Crianca Crianca
		{
			get
			{
				return _crianca;
			}
			private set
			{
				SetProperty(ref _crianca, value);
			}
		}

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		public override void OnDispose()
		{
			base.OnDispose();

			if(_currentCriancaChangedToken != null)
			{
				Messenger.Unsubscribe<CurrentCriancaChangedMessage>(_currentCriancaChangedToken);
			}
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			ConfiguracoesCommand = new MvxCommand(ExecuteConfiguracoesCommand);
		}

		private void ExecuteConfiguracoesCommand()
		{
			NavigateTo<ConfiguracoesViewModel>(new MvxBundle(new Dictionary<string, string>
																{
																	{
																		"ShowModal", "true"
																	}
																}));
		}

		private void ReceiveCurrentCriancaChangedMessage(CurrentCriancaChangedMessage obj)
		{
			Crianca = LoginHelper.CurrentCrianca;
			OnCurrentCriancaChanged();
		}

		protected virtual void OnCurrentCriancaChanged()
		{
		}

		#endregion
	}

}
