using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

using BRFX.Core.MessageBox;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Model;
using DS.Kids.Model.Validations;
using DS.Kids.Model.Services;

namespace DS.Kids.Apps.Core.ViewModels
{
	public class DiarioViewModel : BaseHomeChildViewModel
	{
        //AdeS => 8
        private readonly int _idParceiro = 8;

        private readonly ObservableCollection<RefeicaoDiario> _refeicoesDiario = new ObservableCollection<RefeicaoDiario>();

		private readonly ObservableCollection<StatusCarinha> _carinhas = new ObservableCollection<StatusCarinha>();

		private readonly MvxSubscriptionToken _refreshDiarioToken;

		internal static DateTime data;

		private static Diario _diario;

		#region Constructors and Destructors

		public ObservableCollection<RefeicaoDiario> RefeicoesDiario
		{
			get
			{
				return _refeicoesDiario;
			}
		}

		public ObservableCollection<StatusCarinha> Carinhas
		{
			get
			{
				return _carinhas;
			}
		}

		public DateTime Data
		{
			get
			{
				return data;
			}
			set
			{
				if(Set(ref data, value))
				{
					UpdateCalendario();
				}
			}
		}

		private async void UpdateCalendario()
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();

			try
			{
				var service = Mvx.Resolve<IDiario>();
				var result = await service.ObterPorIdDataAsync(LoginHelper.CurrentCrianca.IdCrianca, Data);
				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - GetDiario: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if(result.Data != null)
				{
					_diario = result.Data;

					UpdateRefeicoesDiario();
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error - GetDiario: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private void UpdateRefeicoesDiario()
		{
			RefeicoesDiario.Clear();
			RefeicoesDiario.Add(_diario.CafeDaManha);
			RefeicoesDiario.Add(_diario.LancheDaManha);
			RefeicoesDiario.Add(_diario.Almoco);
			RefeicoesDiario.Add(_diario.LancheDaTarde);
			RefeicoesDiario.Add(_diario.Jantar);
			RefeicoesDiario.Add(_diario.LancheDaNoite);

			foreach (var refeicaoDiario in RefeicoesDiario)
			{
				foreach (var refeicaoGrupo in refeicaoDiario.RefeicoesGrupos)
				{
					refeicaoGrupo.RefeicaoDiario = refeicaoDiario;
				}
			}

			var dataNascimento = LoginHelper.CurrentCrianca.DataNascimento;
            
            foreach (var carinha in Carinhas)
			{
				carinha.Count = StatusCarinha.GetCount(dataNascimento, RefeicoesDiario, carinha.TipoGrupoRefeicao);
				carinha.Max = StatusCarinha.GetMax(dataNascimento, carinha.TipoGrupoRefeicao);
				carinha.Carinha = StatusCarinha.GetCarinha(dataNascimento, RefeicoesDiario, carinha.TipoGrupoRefeicao, _idParceiro);
			}
		}

		public DiarioViewModel()
			: base(LeftMenuViewModel.LeftMenuIndex.Diario)
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("DiarioView");

			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.CereaisTuberculosERaizes
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.VerdurasELegumes
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.Frutas
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.LeitesIogurtesEQueijos
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.CarnesEOvos
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.FeijoesESimilares
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.OleosEGorduras
							 });
			Carinhas.Add(new StatusCarinha
							 {
								 TipoGrupoRefeicao = TipoGrupoRefeicao.AcucaresEDoces
							 });

			UpdateCarinhas();

			CategoriasViewModel.LoadCategorias();

			_refreshDiarioToken = Messenger.SubscribeOnMainThread<RefreshDiarioMessage>(ReceiveRefreshDiarioMessage);
		}

		protected override void OnCurrentCriancaChanged()
		{
			base.OnCurrentCriancaChanged();

			_diario = null;

			UpdateCarinhas();
		}

		private void UpdateCarinhas()
		{
		    if (LoginHelper.CurrentCrianca == null)
		    {
		        return;
		    }

			var dataNascimento = LoginHelper.CurrentCrianca.DataNascimento;
			foreach(var carinha in Carinhas)
			{
				carinha.Max = StatusCarinha.GetMax(dataNascimento, carinha.TipoGrupoRefeicao);
			}

			if(_diario == null)
			{
				Data = DateTime.Now;
			}
			else
			{
				UpdateRefeicoesDiario();
			}
		}

		private void ReceiveRefreshDiarioMessage(RefreshDiarioMessage obj)
		{
			var carinha = Carinhas.FirstOrDefault(c => c.TipoGrupoRefeicao == obj.TipoGrupoRefeicao);
			if(carinha != null)
			{
				var dataNascimento = LoginHelper.CurrentCrianca.DataNascimento;

				carinha.Count = StatusCarinha.GetCount(dataNascimento, RefeicoesDiario, carinha.TipoGrupoRefeicao);
				carinha.Max = StatusCarinha.GetMax(dataNascimento, carinha.TipoGrupoRefeicao);
				carinha.Carinha = StatusCarinha.GetCarinha(dataNascimento, RefeicoesDiario, carinha.TipoGrupoRefeicao, _idParceiro);
			}
		}

		public override void OnDispose()
		{
			base.OnDispose();

			if(_refreshDiarioToken != null)
			{
				Messenger.Unsubscribe<RefreshDiarioMessage>(_refreshDiarioToken);
			}
		}

		#endregion

		#region Public Properties

		public MvxCommand SemaforoCommand { get; private set; }

		public MvxCommand<RefeicaoDiario> RefeicaoDiarioSelectedCommand { get; private set; }

		public MvxCommand HelpCommand { get; private set; }

		public MvxCommand CalendarioCommand { get; private set; }

		#endregion

		#region Methods

		protected override void CreateCommands()
		{
			base.CreateCommands();

			SemaforoCommand = new MvxCommand(ExecuteSemaforoCommand);
			RefeicaoDiarioSelectedCommand = new MvxCommand<RefeicaoDiario>(ExecuteRefeicaoDiarioSelectedCommand);
			HelpCommand = new MvxCommand(ExecuteHelpCommand);
			CalendarioCommand = new MvxCommand(ExecuteCalendarioCommand);
		}

		private void ExecuteCalendarioCommand()
		{
			Messenger.Publish(new ShowDiarioCalendarMessage(this));
		}

		private void ExecuteHelpCommand()
		{
			NavigateTo<DiarioHelpViewModel>(new MvxBundle(new Dictionary<string, string>
																{
																	{
																		"ShowModal", "true"
																	}
																}));
		}

		private void ExecuteRefeicaoDiarioSelectedCommand(RefeicaoDiario refeicaoDiario)
		{
			NavigateTo<DiarioRefeicaoViewModel>(refeicaoDiario);
		}

		private void ExecuteSemaforoCommand()
		{
			NavigateTo<SemaforoViewModel>();
		}

		#endregion
	}

}