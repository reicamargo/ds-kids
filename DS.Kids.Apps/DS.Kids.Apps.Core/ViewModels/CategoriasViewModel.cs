using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using BRFX.Core.MessageBox;
using BRFX.Core.Messages;

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

	public class CategoriasViewModel : BaseHomeChildViewModel
	{
		#region Fields

		internal static readonly ObservableCollection<Categoria> StaticCategorias = new ObservableCollection<Categoria>();

		private readonly MvxSubscriptionToken _logoutToken;

		private static int _staticLoadingCount;

		#endregion

		#region Constructors and Destructors

		public CategoriasViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Categorias)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("CategoriasView");

			_logoutToken = Messenger.Subscribe<LogoutMessage>(ReceiveLogoutMessage);

			LoadCategorias();

			_instance = this;
		}

		private static CategoriasViewModel _instance;

		private void ReceiveLogoutMessage(LogoutMessage obj)
		{
			Categorias.Clear();
		}

		public override void OnDispose()
		{
			base.OnDispose();

			if(_logoutToken != null)
			{
				Messenger.Unsubscribe<LogoutMessage>(_logoutToken);
			}

			_instance = null;
		}

		#endregion

		#region Public Properties

		public ObservableCollection<Categoria> Categorias
		{
			get
			{
				return StaticCategorias;
			}
		}

		public MvxCommand<Categoria> CategoriaSelectedCommand { get; private set; }

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			CategoriaSelectedCommand = new MvxCommand<Categoria>(ExecuteCategoriaSelectedCommand);
		}

		private void ExecuteCategoriaSelectedCommand(Categoria categoria)
		{
			NavigateTo<CategoriaViewModel>(new CategoriaViewModelParams(categoria, false));
		}

		public static async void LoadCategorias()
		{
			if(StaticCategorias.Any())
			{
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			StaticStartLoading();
			try
			{
				var service = new Categorias();
				var result = await service.ListarAsync();

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Obter Categorias: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if(result.Data != null)
				{
					StaticCategorias.Clear();
					foreach(var categoria in result.Data.OrderByDescending(c => c.Destaque).ThenBy(c => c.Nome))
					{
						StaticCategorias.Add(categoria);
					}
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - Obter Categorias: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StaticStopLoading();
			}
		}

		private static void StaticStartLoading()
		{
			Interlocked.Increment(ref _staticLoadingCount);
			StaticUpdate();
		}

		private static void StaticStopLoading()
		{
			Interlocked.Decrement(ref _staticLoadingCount);
			StaticUpdate();
		}

		protected override void Update()
		{
			base.Update();
			StaticUpdate();
		}

		private static void StaticUpdate()
		{
			if(_instance != null)
			{
				var progressVisible = _staticLoadingCount > 0;
				var messenger = Mvx.Resolve<IMvxMessenger>();
				messenger.Publish(new LoadingChangedMessage(_instance, progressVisible));
			}
		}

		#endregion
	}

}
