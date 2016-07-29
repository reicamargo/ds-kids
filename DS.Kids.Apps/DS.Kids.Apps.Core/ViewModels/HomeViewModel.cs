using System.Collections.Generic;
using System.Threading.Tasks;

using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Resources;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class HomeViewModel : BaseViewModel
	{

		private BrincadeirasViewModel _brincadeirasViewModel;

		private CardapiosViewModel _cardapiosViewModel;

		private CategoriasViewModel _categoriasViewModel;

		private DiarioViewModel _diarioViewModel;

		private CrescimentoViewModel _crescimentoViewModel;

		public HomeViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("HomeView");

			DiarioViewModel = new DiarioViewModel();
			CardapiosViewModel = new CardapiosViewModel();
			CrescimentoViewModel = new CrescimentoViewModel();
			BrincadeirasViewModel = new BrincadeirasViewModel();
			CategoriasViewModel = new CategoriasViewModel();
		}

		public override void OnDispose()
		{
			if (_diarioViewModel != null)
			{
				_diarioViewModel.Dispose();
				_diarioViewModel = null;
			}

			if (_cardapiosViewModel != null)
			{
				_cardapiosViewModel.Dispose();
				_cardapiosViewModel = null;
			}

			if (_crescimentoViewModel != null)
			{
				_crescimentoViewModel.Dispose();
				_crescimentoViewModel = null;
			}

			if (_brincadeirasViewModel != null)
			{
				_brincadeirasViewModel.Dispose();
				_brincadeirasViewModel = null;
			}

			if (_categoriasViewModel != null)
			{
				_categoriasViewModel.Dispose();
				_categoriasViewModel = null;
			}

			base.OnDispose();
		}

		public async void Init()
		{
			await Task.Delay(100);

			var user = LoginHelper.CurrentUser;

			if (user != null)
			{
				if (user.Criancas.Count == 0)
				{
					NavigateTo<AdicionarFilhoViewModel>(true, new MvxBundle(new Dictionary<string, string>
																{
																	{
																		"ShowModal", "true"
																	}
																}));
				}
			}
		}

		public CardapiosViewModel CardapiosViewModel
		{
			get
			{
				return _cardapiosViewModel;
			}
			set
			{
				SetProperty(ref _cardapiosViewModel, value);
			}
		}

		public DiarioViewModel DiarioViewModel
		{
			get
			{
				return _diarioViewModel;
			}
			set
			{
				SetProperty(ref _diarioViewModel, value);
			}
		}

		public BrincadeirasViewModel BrincadeirasViewModel
		{
			get
			{
				return _brincadeirasViewModel;
			}
			set
			{
				SetProperty(ref _brincadeirasViewModel, value);
			}
		}

		public CategoriasViewModel CategoriasViewModel
		{
			get
			{
				return _categoriasViewModel;
			}
			set
			{
				SetProperty(ref _categoriasViewModel, value);
			}
		}

		public CrescimentoViewModel CrescimentoViewModel
		{
			get
			{
				return _crescimentoViewModel;
			}
			set
			{
				SetProperty(ref _crescimentoViewModel, value);
			}
		}

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

	}

}
