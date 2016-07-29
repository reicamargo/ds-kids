using System;
using System.Linq;

using BRFX.Core.ViewModels;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

using BRFX.Core;
using BRFX.Core.MessageBox;

using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Model.Services;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class DiarioRefeicaoViewModel : ProgressViewModel<RefeicaoDiario>
	{

		private TipoRefeicao _tipoRefeicao;

		public static DiarioRefeicaoViewModel Instance;

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		public DiarioRefeicaoViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("DiarioRefeicaoView");

			if (PlatformInstance.Platform == Platform.Android)
			{
				Instance = this;
			}
		}

		protected override void CreateCommands()
		{
			base.CreateCommands();

			RefeicaoGrupoSelectedCommand = new MvxCommand<RefeicaoGrupo>(ExecuteRefeicaoGrupoSelectedCommand);
			DeleteAlimentoCommand = new MvxCommand<Alimento>(ExecuteDeleteAlimentoCommand);
			CheckBoxCommand = new MvxCommand<RefeicaoGrupo>(ExecuteCheckBoxCommand);
		}

		public async void ExecuteCheckBoxCommand(RefeicaoGrupo refeicaoGrupo)
		{
			if(refeicaoGrupo.Alimentos.Any())
			{
				return;
			}

			var messageBox = Mvx.Resolve<IMessageBox>();

			StartLoading();

			try
			{
				var service = Mvx.Resolve<IDiario>();

				var dto = new DiarioDTO
				{
					IdCrianca = LoginHelper.CurrentCrianca.IdCrianca,
					Data = DiarioViewModel.data,
					IdGrupo = refeicaoGrupo.TipoGrupoRefeicao,
					IdTipoRefeicao = refeicaoGrupo.RefeicaoDiario.TipoRefeicao,
					Checked = refeicaoGrupo.IdRefeicaoGrupo == 0
				};

				var result = await service.AtualizarAsync(dto);

				if (result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Atualizar Checkbox Alimento: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if (result.Data != null)
				{
					refeicaoGrupo.IdRefeicao = result.Data.IdRefeicao ?? 0;
					refeicaoGrupo.IdRefeicaoGrupo = result.Data.IdRefeicaoGrupo ?? 0;

					var messenger = Mvx.Resolve<IMvxMessenger>();
					messenger.Publish(new RefreshDiarioMessage(refeicaoGrupo, refeicaoGrupo.TipoGrupoRefeicao));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Atualizar Checkbox Alimento: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private async void ExecuteDeleteAlimentoCommand(Alimento alimento)
		{
			var messageBox = Mvx.Resolve<IMessageBox>();

			RefeicaoGrupo refeicaoGrupo = null;
			foreach (var grupo in RefeicoesGrupo)
			{
				// Remove a primeira ocorrência da refeição
				if (grupo.Alimentos.Any(a => a.IdAlimento == alimento.IdAlimento))
				{
					refeicaoGrupo = grupo;
					break;
				}
			}

			if (refeicaoGrupo == null)
			{
				return;
			}

			if (PlatformInstance.Platform == Platform.iOS)
			{
				await RemoveAlimento(refeicaoGrupo, alimento);
			}
			else if(PlatformInstance.Platform == Platform.Android)
			{
				messageBox.Show("Deseja realmente remover esse alimento?", async ok =>
				{
					if (ok)
					{
						await RemoveAlimento(refeicaoGrupo, alimento);
					}

				}, alimento.Nome, MessageBoxButtons.OkCancel); 
			}
		}

		private async System.Threading.Tasks.Task RemoveAlimento(RefeicaoGrupo refeicaoGrupo, Alimento alimento)
		{
			StartLoading();

			var messageBox = Mvx.Resolve<IMessageBox>();

			try
			{
				var service = Mvx.Resolve<IDiario>();

				var result = await service.RemoverAlimentoAsync(refeicaoGrupo.IdRefeicaoGrupo, alimento.IdAlimento);

				if(result.ResultCode != ResultCodes.Success)
				{
					Debug.WriteLine("Error - Remover Alimento: " + result.ResultMessage);
					messageBox.Log(result.ResultMessage);
					return;
				}

				if(result.Data != null)
				{
					refeicaoGrupo.IdRefeicao = result.Data.IdRefeicao ?? 0;
					refeicaoGrupo.IdRefeicaoGrupo = result.Data.IdRefeicaoGrupo ?? 0;
				}

				if(refeicaoGrupo.Alimentos.Remove(alimento))
				{
					Messenger.Publish(new RefreshDiarioMessage(this, refeicaoGrupo.TipoGrupoRefeicao));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error - Remover Alimento: " + ex);
				messageBox.Log(ex);
			}
			finally
			{
				StopLoading();
			}
		}

		private void ExecuteRefeicaoGrupoSelectedCommand(RefeicaoGrupo refeicaoGrupo)
		{
			NavigateTo<DiarioAlimentosViewModel>(new DiarioAlimentosViewModel.DiarioAlimentosViewModelParams(refeicaoGrupo));
		}

		protected override void GetParams(RefeicaoDiario refeicaoDiario)
		{
			TipoRefeicao = refeicaoDiario.TipoRefeicao;
			StaticRefeicoesGrupo.Clear();
			foreach(var grupoAlimentarRefeicao in refeicaoDiario.RefeicoesGrupos.OrderByDescending(r => r.Sugerido))
			{
				StaticRefeicoesGrupo.Add(grupoAlimentarRefeicao);
			}

            //Coloca Bebidas em primeiro na categoria "Outros"
            var oldIndex = StaticRefeicoesGrupo.IndexOf(StaticRefeicoesGrupo.Where(rg => rg.TipoGrupoRefeicao == TipoGrupoRefeicao.Bebidas).FirstOrDefault());
            var newIndex = StaticRefeicoesGrupo.IndexOf(StaticRefeicoesGrupo.Last(rg => rg.Sugerido == true));
            StaticRefeicoesGrupo.Move(oldIndex, newIndex + 1);
        }

		public TipoRefeicao TipoRefeicao
		{
			get
			{
				return _tipoRefeicao;
			}
			set
			{
				Set(ref _tipoRefeicao, value);
			}
		}

		public ObservableCollection<RefeicaoGrupo> RefeicoesGrupo
		{
			get
			{
				return StaticRefeicoesGrupo;
			}
		}

		public static readonly ObservableCollection<RefeicaoGrupo> StaticRefeicoesGrupo = new ObservableCollection<RefeicaoGrupo>();

		public MvxCommand<RefeicaoGrupo> RefeicaoGrupoSelectedCommand { get; private set; }

		public MvxCommand<Alimento> DeleteAlimentoCommand { get; private set; }

		public MvxCommand<RefeicaoGrupo> CheckBoxCommand { get; private set; }
	}

}