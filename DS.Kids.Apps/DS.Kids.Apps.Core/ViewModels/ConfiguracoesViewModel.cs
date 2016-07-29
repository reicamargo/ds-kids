using System.Collections.ObjectModel;
using System.Linq;

using BRFX.Core;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class ConfiguracoesViewModel : BaseViewModel
	{
		#region Fields

		private readonly ObservableCollection<Crianca> _criancas = new ObservableCollection<Crianca>();

		private readonly MvxSubscriptionToken _currentCriancaChangedToken;

		private Crianca _selectedCrianca;

		internal static ConfiguracoesViewModel Instance;

		#endregion

		#region Constructors and Destructors

		public ConfiguracoesViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("ConfiguracoesView");

			if(PlatformInstance.Platform == Platform.iOS)
			{
				Instance = this;
			}

			_currentCriancaChangedToken = Messenger.Subscribe<CurrentCriancaChangedMessage>(ReceiveCurrentCriancaChangedMessage);

			UpdateCriancas();
		}

		#endregion

		#region Public Properties

		public MvxCommand AdicionarFilhoCommand { get; private set; }

		public MvxCommand SobreCommand { get; private set; }

		public ObservableCollection<Crianca> Criancas
		{
			get
			{
				return _criancas;
			}
		}

		public MvxCommand<Crianca> SelecionarFilhoCommand { get; private set; }

		public Crianca SelectedCrianca
		{
			get
			{
				return _selectedCrianca;
			}
			private set
			{
				SetProperty(ref _selectedCrianca, value);
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

			Instance = null;
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			AdicionarFilhoCommand = new MvxCommand(ExecuteAdicionarFilhoCommand);
			SelecionarFilhoCommand = new MvxCommand<Crianca>(ExecuteSelecionarFilhoCommand);
			SobreCommand = new MvxCommand(ExecuteSobreCommand);
		}

		private void ExecuteSobreCommand()
		{
			NavigateTo<SobreViewModel>();
		}

		private void ExecuteAdicionarFilhoCommand()
		{
			// único caso onde não é modal e deve chamar o Close(this)
			NavigateTo<AdicionarFilhoViewModel>(true);
		}

		private void ExecuteSelecionarFilhoCommand(Crianca crianca)
		{
			NavigateTo<AlterarFilhoViewModel>(crianca);
		}

		private void ReceiveCurrentCriancaChangedMessage(CurrentCriancaChangedMessage obj)
		{
			UpdateCriancas();
		}

		private void UpdateCriancas()
		{
			Criancas.Clear();
			foreach(var crianca in LoginHelper.CurrentUser.Criancas.OrderBy(c => c.Nome))
			{
				Criancas.Add(crianca);
			}

			SelectedCrianca = LoginHelper.CurrentCrianca;
		}

		#endregion
	}

}
