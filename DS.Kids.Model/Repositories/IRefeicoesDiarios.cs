using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IRefeicoesDiarios
    {
        Task<IList<RefeicaoDiario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario);
        Task<RefeicaoDiario> ObterPorIdDataRefeicaoAsync(int idCrianca, DateTime dataDiario, TipoRefeicao tipoRefeicao);
        Task InserirAsync(RefeicaoDiario refeicaoGrupo);
        Task AtualizarAsync(RefeicaoDiario refeicaoGrupo);
        Task RemoverAsync(RefeicaoDiario refeicaoGrupo);
    }
}
