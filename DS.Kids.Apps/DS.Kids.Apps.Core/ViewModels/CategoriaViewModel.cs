using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BRFX.Core.MessageBox;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class CategoriaViewModel : ProgressViewModel<CategoriaViewModelParams>
	{
		#region Static Fields

		private static readonly Dictionary<int, DateTime> _dicasDateCache = new Dictionary<int, DateTime>();

		#endregion

		#region Fields

		private readonly ObservableCollection<Dica> _dicas = new ObservableCollection<Dica>();

		private readonly MvxSubscriptionToken _logoutToken;

		private Categoria _categoria;

		#endregion

		#region Constructors and Destructors

		public CategoriaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("CategoriaView");

			_logoutToken = Messenger.Subscribe<LogoutMessage>(ReceiveLogoutMessage);
		}

		#endregion

		#region Public Properties

		public Categoria Categoria
		{
			get
			{
				return _categoria;
			}
			private set
			{
				SetProperty(ref _categoria, value);
			}
		}

        public bool ShowHamburgerMenu { get; private set; }

		public ObservableCollection<Dica> Dicas
		{
			get
			{
				return _dicas;
			}
		}

		public MvxCommand<Dica> DicaSelectedCommand { get; private set; }

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
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

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			DicaSelectedCommand = new MvxCommand<Dica>(ExecuteDicaSelectedCommand);
		}

		/// <summary>
		///     Obtém os parâmetros enviado para este view model.
		/// </summary>
        protected override async void GetParams(CategoriaViewModelParams categoriaViewModelParams)
		{
            Categoria = categoriaViewModelParams.Categoria;
            ShowHamburgerMenu = categoriaViewModelParams.ShowHamburgerMenu;

            //Delay para que o thread de UI consiga desenhar a view, fazendo com que o loadspin e o overlay apareçam
			await Task.Delay(50);

			await LoadCategoria();
		}

		private static void ReceiveLogoutMessage(LogoutMessage obj)
		{
			_dicasDateCache.Clear();
		}

		private void ExecuteDicaSelectedCommand(Dica dica)
		{
			NavigateTo<DicaViewModel>(dica);
		}

		private void FillDicas()
		{
			Dicas.Clear();
			if(Categoria.Dicas != null)
			{
				foreach(var dica in Categoria.Dicas)
				{
					dica.Categoria = Categoria;
					Dicas.Add(dica);
				}
			}
		}

		private async Task LoadCategoria()
		{
			// Cache
			if(Categoria.Dicas != null && Categoria.Dicas.Any() &&
				_dicasDateCache.ContainsKey(Categoria.IdCategoria) &&
				_dicasDateCache[Categoria.IdCategoria] < DateTime.UtcNow + TimeSpan.FromDays(1))
			{
				FillDicas();
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();
			try
			{
				var service = new Categorias();
				var result = await service.ObterPorIdAsync(Categoria.IdCategoria);

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Obter Categoria: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				_dicasDateCache[Categoria.IdCategoria] = DateTime.UtcNow;

				if(result.Data != null)
				{
					if(Categoria.Dicas == null)
					{
						Categoria.Dicas = new List<Dica>();
					}
					else
					{
						Categoria.Dicas.Clear();
					}
					if(result.Data.Dicas != null)
					{
						foreach(var dica in result.Data.Dicas)
						{
							Categoria.Dicas.Add(dica);
						}
					}
				}

				FillDicas();
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Obter Categoria: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		#endregion
    }

    public class CategoriaViewModelParams
    {
        public CategoriaViewModelParams(Categoria categoria, bool showHamburgerMenu)
		{
            Categoria = categoria;
            ShowHamburgerMenu = showHamburgerMenu;
		}

        public Categoria Categoria { get; private set; }

        public bool ShowHamburgerMenu { get; private set; }
        
    }
}
