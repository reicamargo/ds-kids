using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class SemaforoViewModel : ProgressViewModel
	{
		#region Fields

		private readonly ObservableCollection<Alimento> _alimentos = new ObservableCollection<Alimento>();

		private string _searchQuery = "";

		private CancellationTokenSource _searchCancellationTokenSource;

		private bool _descricaoVisible = true;

		#endregion

		#region Constructors and Destructors

		public MvxCommand SearchCommand { get; private set; }

		public SemaforoViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("SemaforoView");
		}

		#endregion

		#region Public Properties

		protected override void CreateCommands()
		{
			base.CreateCommands();

			SearchCommand = new MvxCommand(ExecuteSearchCommand);
		}

		private void ExecuteSearchCommand()
		{
			SearchAlimentos(true);
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
				SearchAlimentos(false);
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

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		#endregion

		#region Methods

		private async void SearchAlimentos(bool force)
		{
			if(force == false && (string.IsNullOrEmpty(SearchQuery) || SearchQuery.Length < 3))
			{
				return;
			}

			Alimentos.Clear();

			DescricaoVisible = false;

			var tempAlimentos = new[]
									{
										new Alimento
											{
												Nome = "Iogurte desnatado"
											},
										new Alimento
											{
												Nome = "Danoninho"
											},
										new Alimento
											{
												Nome = "Danete Chocolate Branco"
											},
										new Alimento
											{
												Nome = "Iogurte cenoura e mel light Taeq"
											},
										new Alimento
											{
												Nome = "Iogurte grego tradicional"
											},
										new Alimento
											{
												Nome = "Activia zero morango"
											},
										new Alimento
											{
												Nome = "Iogurte cenoura e mel light Taeq"
											},
										new Alimento
											{
												Nome = "Iogurte grego tradicional"
											}
									};

			if(_searchCancellationTokenSource != null)
			{
				_searchCancellationTokenSource.Cancel();
				_searchCancellationTokenSource.Dispose();
				_searchCancellationTokenSource = null;
			}

			_searchCancellationTokenSource = new CancellationTokenSource();

			StartLoading();

			// Simulação do serviço
			try
			{
				await Task.Delay(1000, _searchCancellationTokenSource.Token);

				StopLoading();

				Alimentos.Clear();

				var searchQuery = SearchQuery.Trim().ToLower();

				foreach(var alimento in tempAlimentos.Where(a => Matches(a, searchQuery)))
				{
					Alimentos.Add(alimento);
				}
			}
			catch(TaskCanceledException)
			{
				StopLoading();
			}
		}

		private static bool Matches(Alimento alimento, string searchQuery)
		{
			if(alimento == null || string.IsNullOrEmpty(alimento.Nome))
			{
				return false;
			}

			var nome = alimento.Nome.Trim().ToLower();

			return nome.Contains(searchQuery);
		}

		#endregion
	}

}
