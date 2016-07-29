using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BRFX.Core.ViewModels;

using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class LeftMenuViewModel : BaseViewModel
	{
		#region Fields

		private readonly MvxSubscriptionToken _currentCriancaChangedToken;

		private readonly MvxSubscriptionToken _currentUserChangedToken;

		private readonly MvxSubscriptionToken _leftMenuIndexChangedToken;

		private readonly ObservableCollection<Crianca> _listaCriancas = new ObservableCollection<Crianca>();

		private Crianca _crianca;

		private int _selectedIndex;

		private int _selectedMenu;

		private Responsavel _user;

		#endregion

		#region Constructors and Destructors

		public LeftMenuViewModel()
		{
			SelectedIndex = (int)LeftMenuIndex.Cardapios;
			SelectedMenu = (int)VisibleMenuIndex.Menu;

			_currentCriancaChangedToken = Messenger.Subscribe<CurrentCriancaChangedMessage>(ReceiveCurrentCriancaChangedMessage);
			_currentUserChangedToken = Messenger.Subscribe<CurrentUserChangedMessage>(ReceiveCurrentUserChangedMessage);
			_leftMenuIndexChangedToken = Messenger.Subscribe<LeftMenuIndexChangedMessage>(ReceiveLeftMenuIndexChangedMessage);

			User = LoginHelper.CurrentUser;
			Crianca = LoginHelper.CurrentCrianca;

			UpdateCriancaList();
		}

		#endregion

		#region Enums

		public enum LeftMenuIndex
		{
			Diario,

			Cardapios,

			Crescimento,

			Brincadeiras,

			Categorias,

			Sobre,

			None

		}

		public enum VisibleMenuIndex
		{

			Menu,

			Criancas

		}

		#endregion

		#region Public Properties

		public ICommand AlternarMenuVisivelCommand { get; private set; }

		public ICommand BrincadeirasCommand { get; private set; }

		public ICommand CardapiosCommand { get; private set; }

		public ICommand CategoriasCommand { get; private set; }

		public ICommand CrescimentoCommand { get; private set; }

		public ICommand DiarioCommand { get; private set; }

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

		public ICommand EditarFilhoCommand { get; private set; }

		public ObservableCollection<Crianca> ListaCriancas
		{
			get
			{
				return _listaCriancas;
			}
		}

		public ICommand SobreCommand { get; private set; }

		public ICommand AdicionarFilhoCommand { get; private set; }

		public ICommand SelecionarCurrentFilhoCommand { get; private set; }

		public ICommand SelecionarFilhoCommand { get; private set; }

		public int SelectedIndex
		{
			get
			{
				return _selectedIndex;
			}
			private set
			{
				Set(ref _selectedIndex, value);
				SendCloseLeftMenuMessage();
			}
		}

		public int SelectedMenu
		{
			get
			{
				return _selectedMenu;
			}
			private set
			{
				Set(ref _selectedMenu, value);
			}
		}

		public Responsavel User
		{
			get
			{
				return _user;
			}
			private set
			{
				SetProperty(ref _user, value);
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

			if (_currentCriancaChangedToken != null)
			{
				Messenger.Unsubscribe<CurrentCriancaChangedMessage>(_currentCriancaChangedToken);
			}
			if (_currentUserChangedToken != null)
			{
				Messenger.Unsubscribe<CurrentCriancaChangedMessage>(_currentCriancaChangedToken);
				Messenger.Unsubscribe<CurrentUserChangedMessage>(_currentUserChangedToken);
			}
			if (_leftMenuIndexChangedToken != null)
			{
				Messenger.Unsubscribe<LeftMenuIndexChangedMessage>(_leftMenuIndexChangedToken);
			}
		}

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			SobreCommand = new MvxCommand(ExecuteSobreCommand);
			DiarioCommand = new MvxCommand(ExecuteDiarioCommand);
			CardapiosCommand = new MvxCommand(ExecuteCardapiosCommand);
			CategoriasCommand = new MvxCommand(ExecuteCategoriasCommand);
			CrescimentoCommand = new MvxCommand(ExecuteCrescimentoCommand);
			BrincadeirasCommand = new MvxCommand(ExecuteBrincadeirasCommand);
			AlternarMenuVisivelCommand = new MvxCommand(ExecutarAlternarMenuVisivelCommand);
			EditarFilhoCommand = new MvxCommand<Crianca>(ExecutarEditarFilhoCommand);
			AdicionarFilhoCommand = new MvxCommand(ExecuteAdicionarFilhoCommand);
			SelecionarFilhoCommand = new MvxCommand<Crianca>(ExecuteSelecionarFilhoCommand);
			SelecionarCurrentFilhoCommand = new MvxCommand(ExecuteSelecionarCurrentFilhoCommand);
		}

		private void ExecutarAlternarMenuVisivelCommand()
		{
			SelectedMenu = (int)(SelectedMenu == (int)VisibleMenuIndex.Criancas ? VisibleMenuIndex.Menu : VisibleMenuIndex.Criancas);
		}

		private void ExecutarEditarFilhoCommand(Crianca crianca)
		{
			SelectedIndex = (int)LeftMenuIndex.None;

			if (crianca != null)
			{
				SelectedMenu = (int)VisibleMenuIndex.Menu;
				NavigateTo<AlterarFilhoViewModel>(crianca);
			}
			else
			{
				NavigateTo<AdicionarFilhoViewModel>(true, new MvxBundle(new Dictionary<string, string>
																			{
																				{
																					"ShowModal", "true"
																				}
																			}));
			}
		}

		private void ExecuteDiarioCommand()
		{
			NavigateTo<DiarioViewModel>();
		}

		private void ExecuteAdicionarFilhoCommand()
		{
			NavigateTo<AdicionarFilhoViewModel>(true, new MvxBundle(new Dictionary<string, string> { {
						"ShowModal", "true"
					}
				}));
		}

		private void ExecuteBrincadeirasCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.Brincadeiras;
			NavigateTo<BrincadeirasViewModel>();
		}

		private void ExecuteCardapiosCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.Cardapios;
			NavigateTo<CardapiosViewModel>();
		}

		private void ExecuteCategoriasCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.Categorias;
			NavigateTo<CategoriasViewModel>();
		}

		private void ExecuteCrescimentoCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.Crescimento;
			NavigateTo<CrescimentoViewModel>();
		}

		private void ExecuteSobreCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.Sobre;
			NavigateTo<SobreViewModel>();
		}

		private void ExecuteSelecionarCurrentFilhoCommand()
		{
			SelectedIndex = (int)LeftMenuIndex.None;
			NavigateTo<AlterarFilhoViewModel>(Crianca);
		}

		private void ExecuteSelecionarFilhoCommand(Crianca crianca)
		{
			LoginHelper.UpdateCurrentCrianca(crianca.IdCrianca, this);
		}

		private void ReceiveCurrentCriancaChangedMessage(CurrentCriancaChangedMessage obj)
		{
			Crianca = LoginHelper.CurrentCrianca;
			UpdateCriancaList();
		}

		private void ReceiveCurrentUserChangedMessage(CurrentUserChangedMessage obj)
		{
			User = LoginHelper.CurrentUser;
		}

		private void ReceiveLeftMenuIndexChangedMessage(LeftMenuIndexChangedMessage obj)
		{
			SelectedIndex = (int)obj.LeftMenuIndex;
		}

		private void SendCloseLeftMenuMessage()
		{
			Messenger.Publish(new CloseLeftMenuMessage(this));
		}

		private void UpdateCriancaList()
		{
			if (LoginHelper.IsLoggedin() && LoginHelper.CurrentUser.Criancas != null)
			{
				ListaCriancas.Clear();

				var criancas = (from crianca in LoginHelper.CurrentUser.Criancas
								where (LoginHelper.CurrentCrianca == null ||
										crianca.IdCrianca != LoginHelper.CurrentCrianca.IdCrianca)
								orderby crianca.DataCriacao descending
								select crianca).Take(8);

				foreach (var crianca in criancas)
				{
					ListaCriancas.Add(crianca);
				}
			}
		}

		#endregion
	}

}
