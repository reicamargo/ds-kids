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
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class DiarioAlimentosViewModel : ProgressViewModel<DiarioAlimentosViewModel.DiarioAlimentosViewModelParams>
	{

		private RefeicaoGrupo _refeicaoGrupo;

		private readonly MvxSubscriptionToken _logoutToken;

		private Alimento _alimento;

		private readonly ObservableCollection<Alimento> _alimentos = new ObservableCollection<Alimento>();

		private string _searchQuery = "";

		private bool _descricaoVisible = true;

		public MvxCommand SearchCommand { get; private set; }

		private static readonly Dictionary<TipoGrupoRefeicao, List<Alimento>> _alimentosCache = new Dictionary<TipoGrupoRefeicao, List<Alimento>>();

		public MvxCommand<Alimento> AlimentoSelectedCommand { get; private set; }

		public DiarioAlimentosViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("DiarioAlimentosView");

			_logoutToken = Messenger.Subscribe<LogoutMessage>(ReceiveLogoutMessage);
		}

		protected override void CreateCommands()
		{
			base.CreateCommands();

			SearchCommand = new MvxCommand(ExecuteSearchCommand);
			AlimentoSelectedCommand = new MvxCommand<Alimento>(ExecuteAlimentoSelectedCommand);
		}

		public override void OnDispose()
		{
			base.OnDispose();

			if (_logoutToken != null)
			{
				Messenger.Unsubscribe<LogoutMessage>(_logoutToken);
			}
		}

		private static void ReceiveLogoutMessage(LogoutMessage obj)
		{
			_alimentosCache.Clear();
		}

		private async void ExecuteAlimentoSelectedCommand(Alimento alimento)
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();

			try
			{
				var service = Mvx.Resolve<IDiario>();

				var dto = new DiarioDTO
				{
					IdCrianca = LoginHelper.CurrentCrianca.IdCrianca,
					Data = DiarioViewModel.data,
					IdGrupo = RefeicaoGrupo.TipoGrupoRefeicao,
					IdTipoRefeicao = RefeicaoGrupo.RefeicaoDiario.TipoRefeicao,
					IdAlimento = alimento.IdAlimento
				};

				var result = await service.AtualizarAsync(dto);

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Atualizar Alimento: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage, "Oppsssss");
					return;
				}

				if (result.Data != null)
				{
					RefeicaoGrupo.IdRefeicao = result.Data.IdRefeicao ?? 0;
					RefeicaoGrupo.IdRefeicaoGrupo = result.Data.IdRefeicaoGrupo ?? 0;

					if (Alimento != null)
					{
						var index = RefeicaoGrupo.Alimentos.IndexOf(Alimento);

						RefeicaoGrupo.Alimentos.RemoveAt(index);
						RefeicaoGrupo.Alimentos.Insert(index, alimento);
					}
					else
					{
						RefeicaoGrupo.Alimentos.Add(alimento);
					}
					Messenger.Publish(new RefreshDiarioMessage(this, RefeicaoGrupo.TipoGrupoRefeicao));
					Close(this);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Atualizar Alimento: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private void ExecuteSearchCommand()
		{
			SearchRefeicoes();
		}

		public ObservableCollection<Alimento> Alimentos
		{
			get
			{
				return _alimentos;
			}
		}

		public string SearchQuery
		{
			get
			{
				return _searchQuery;
			}
			set
			{
				Set(ref _searchQuery, value);
				SearchRefeicoes();
			}
		}

		public bool DescricaoVisible
		{
			get
			{
				return _descricaoVisible;
			}
			private set
			{
				Set(ref _descricaoVisible, value);
			}
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		protected override async void GetParams(DiarioAlimentosViewModelParams diarioAlimentosViewModelParams)
		{
			RefeicaoGrupo = diarioAlimentosViewModelParams.RefeicaoGrupo;
			Alimento = diarioAlimentosViewModelParams.Alimento;

			await Task.Delay(100);

			if (_alimentosCache.ContainsKey(RefeicaoGrupo.TipoGrupoRefeicao) == false)
			{
				var messageBox = Mvx.Resolve<IMessageBox>();

				StartLoading();

				try
				{
					var service = Mvx.Resolve<IAlimento>();
					var result = await service.ObterPorGrupoAlimentar(LoginHelper.CurrentCrianca.MesesDeIdade, (int)RefeicaoGrupo.TipoGrupoRefeicao);
					if(result.ResultCode != ResultCodes.Success)
					{
						Debug.WriteLine("Error - Obter Alimentos: " + result.ResultMessage);
						messageBox.Log(result.ResultMessage);
						return;
					}

					if(result.Data != null)
					{
						_alimentosCache.Add(RefeicaoGrupo.TipoGrupoRefeicao, result.Data.ToList());

						SearchRefeicoes();
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Error - Obter Alimentos: " + ex);
					messageBox.Log(ex);
					Close(this);
				}
				finally
				{
					StopLoading();
				}
			}
			else
			{
				SearchRefeicoes();
			}
		}

		public Alimento Alimento
		{
			get
			{
				return _alimento;
			}
			private set
			{
				SetProperty(ref _alimento, value);
			}
		}

		public RefeicaoGrupo RefeicaoGrupo
		{
			get
			{
				return _refeicaoGrupo;
			}
			private set
			{
				SetProperty(ref _refeicaoGrupo, value);
			}
		}

		private void SearchRefeicoes()
		{
			if(_alimentosCache.ContainsKey(RefeicaoGrupo.TipoGrupoRefeicao) == false)
			{
				return;
			}

			if (_searchQuery == null)
			{
				_searchQuery = "";
			}

			Alimentos.Clear();

			DescricaoVisible = false;

			var alimentos = _alimentosCache[RefeicaoGrupo.TipoGrupoRefeicao];

			var searchQuery = SearchQuery.Trim().ToLower();

            if (String.IsNullOrEmpty(searchQuery))
            {
                foreach (var alimento in alimentos)
                {
                    Alimentos.Add(alimento);
                }
            }
            else
            {
                foreach (var alimento in alimentos.Where(a => Matches(a, searchQuery)).OrderBy(a => a.Nome))
                {
                    Alimentos.Add(alimento);
                }
            }
		}

		private bool Matches(Alimento alimento, string searchQuery)
		{
			if (alimento == null || string.IsNullOrEmpty(alimento.Nome) || RefeicaoGrupo.Alimentos.Any(a => a.IdAlimento == alimento.IdAlimento))
			{
				return false;
			}

			var nome = alimento.Nome.Trim().ToLower();

			return nome.Contains(searchQuery);
		}

		public class DiarioAlimentosViewModelParams
		{

			public RefeicaoGrupo RefeicaoGrupo { get; private set; }

			public Alimento Alimento { get; private set; }

			public DiarioAlimentosViewModelParams(RefeicaoGrupo refeicaoGrupo, Alimento alimento = null)
			{
				RefeicaoGrupo = refeicaoGrupo;
				Alimento = alimento;
			}

		}

	}

}