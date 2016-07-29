using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface IDiario
    {
        Task<Result<Diario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario);
        Task<Result<DiarioDTO>> AtualizarAsync(DiarioDTO diarioDTO);
        Task<Result<DiarioDTO>> RemoverAlimentoAsync(int idRefeicaoGrupo, int idAlimento);
    }
}
