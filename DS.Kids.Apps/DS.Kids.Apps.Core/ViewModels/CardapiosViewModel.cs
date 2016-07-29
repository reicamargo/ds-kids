using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BRFX.Core.MessageBox;

using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Model;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class CardapiosViewModel : BaseHomeChildViewModel
	{
		#region Fields

		private static readonly ObservableCollection<CardapioRefeicao> _cardapio = new ObservableCollection<CardapioRefeicao>();

		#endregion

		#region Constructors and Destructors

		public CardapiosViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Cardapios)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("CardapiosView");

			CategoriasViewModel.LoadCategorias();
			LoadCardapios();
		}

		protected override void OnCurrentCriancaChanged()
		{
		    base.OnCurrentCriancaChanged();
			Cardapio.Clear();
			LoadCardapios();
		}

		#endregion

		#region Public Properties

		public ObservableCollection<CardapioRefeicao> Cardapio
		{
			get
			{
				return _cardapio;
			}
		}

		public MvxCommand<TipoRefeicao> OutraSugestaoCommand { get; private set; }

		public MvxCommand<RefeicaoItem> DicaCommand { get; private set; }

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			OutraSugestaoCommand = new MvxCommand<TipoRefeicao>(ExecuteOutraSugestaoCommand);
			DicaCommand = new MvxCommand<RefeicaoItem>(ExecuteDicaCommand);
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

		private void ExecuteDicaCommand(RefeicaoItem refeicaoItem)
		{
			if (refeicaoItem != null && refeicaoItem.Alimento != null && refeicaoItem.Alimento.Dica != null)
			{
				NavigateTo<DicaViewModel>(refeicaoItem.Alimento.Dica, new MvxBundle(new Dictionary<string, string>
																{
																	{
																		"ShowModal", "true"
																	}
																}));
			}
		}

		private async void ExecuteOutraSugestaoCommand(TipoRefeicao tipoRefeicao)
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ICardapio>();
				var result = await service.SubstituirRefeicaoAsync(LoginHelper.CurrentCrianca.MesesDeIdade, tipoRefeicao);

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Substituir Refeição: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if (result.Data != null)
				{
					var index = (int)tipoRefeicao - 1;
					var cardapio = Cardapio[index];
					cardapio.Clear();
					cardapio.Parceiro = result.Data.Parceiro;
					foreach (var refeicaoItem in result.Data.RefeicoesItens)
					{
						cardapio.Add(refeicaoItem);
					}
					Cardapio.Remove(cardapio);
					Cardapio.Insert(index, cardapio);
					Messenger.Publish(new CardapioChangedMessage(this));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Substituir Refeição: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private async void LoadCardapios()
		{
			if (LoginHelper.CurrentCrianca == null)
			{
				return;
			}

			if (Cardapio.Any())
			{
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();
			try
			{
				var service = Mvx.Resolve<ICardapio>();
				var result = await service.ObterAsync(LoginHelper.CurrentCrianca.MesesDeIdade);

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Obter cardápio: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if (result.Data != null)
				{
					Cardapio.Clear();
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.CafeDaManha, result.Data.CafeDaManha, OutraSugestaoCommand));
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.LancheDaManha, result.Data.LancheDaManha, OutraSugestaoCommand));
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.Almoco, result.Data.Almoco, OutraSugestaoCommand));
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.LancheDaTarde, result.Data.LancheDaTarde, OutraSugestaoCommand));
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.Jantar, result.Data.Jantar, OutraSugestaoCommand));
					Cardapio.Add(new CardapioRefeicao(TipoRefeicao.LancheDaNoite, result.Data.LancheNoite, OutraSugestaoCommand));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Obter cardápio: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		#endregion

		public class CardapioRefeicao : ObservableCollection<RefeicaoItem>
		{
			#region Fields

			private TipoRefeicao _tipoRefeicao;

			private Parceiro _parceiro;

			private readonly MvxCommand<TipoRefeicao> _outraSugestaoCommand;

			public Parceiro Parceiro
			{
				get
				{
					return _parceiro;
				}
				set
				{
					_parceiro = value;
					OnPropertyChanged(new PropertyChangedEventArgs("Parceiro"));
				}
			}

			#endregion

			#region Constructors and Destructors

			public CardapioRefeicao(TipoRefeicao tipoRefeicao, Refeicao refeicoes, MvxCommand<TipoRefeicao> outraSugestaoCommand)
				: base(refeicoes.RefeicoesItens)
			{
				_tipoRefeicao = tipoRefeicao;
				_outraSugestaoCommand = outraSugestaoCommand;
				OutraSugestaoCommand = new MvxCommand(ExecuteOutraSugestaoCommand);
				_parceiro = refeicoes.Parceiro;
			}

			private void ExecuteOutraSugestaoCommand()
			{
				_outraSugestaoCommand.Execute(TipoRefeicao);
			}

			#endregion

			#region Public Properties

			public MvxCommand OutraSugestaoCommand { get; private set; }

			public TipoRefeicao TipoRefeicao
			{
				get
				{
					return _tipoRefeicao;
				}
				set
				{
					_tipoRefeicao = value;
					OnPropertyChanged(new PropertyChangedEventArgs("TipoRefeicao"));
				}
			}

			public MvxColor Color
			{
				get
				{
					switch (TipoRefeicao)
					{
						case TipoRefeicao.CafeDaManha:
							return new MvxColor(255, 159, 27);
						case TipoRefeicao.LancheDaManha:
						case TipoRefeicao.LancheDaTarde:
							return new MvxColor(237, 76, 90);
						case TipoRefeicao.Almoco:
							return new MvxColor(88, 173, 109);
						case TipoRefeicao.Jantar:
							return new MvxColor(4, 63, 132);
						case TipoRefeicao.LancheDaNoite:
							return new MvxColor(91, 64, 132);
					}
					return null;
				}
			}

			#endregion
		}

	}

}
