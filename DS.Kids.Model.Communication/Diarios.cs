using System;
using System.Threading.Tasks;

using DS.Kids.Model.Communication.Support;
using DS.Kids.Model.Services;

namespace DS.Kids.Model.Communication
{
	public class Diarios : IDiario
	{
		public async Task<Result<Diario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario)
		{
			var url = string.Format(Endpoints.DIARIO, idCrianca, dataDiario.ToString("MM/dd/yyyy"));
			var result = await Rest.GetAsync<Result<Diario>>(url);
			return result;
		}

		public async Task<Result<DiarioDTO>> AtualizarAsync(DiarioDTO diarioDTO)
		{
			var result = await Rest.PostAsync<Result<DiarioDTO>, DiarioDTO>(Endpoints.UPDATE_DIARIO, diarioDTO);
			return result;
		}

		public async Task<Result<DiarioDTO>> RemoverAlimentoAsync(int idRefeicaoGrupo, int idAlimento)
		{
			var url = string.Format(Endpoints.DELETE_REFEICAO_DIARIO, idRefeicaoGrupo, idAlimento);
			var result = await Rest.DeleteAsync<Result<DiarioDTO>>(url);
			return result;
		}

	}
}
