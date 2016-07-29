using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface ICrescimentos
    {
        Task<IEnumerable<Crescimento>> ListarPorCriancaIdAsync(int criancaId);
        Task<Crescimento> ObterUltimoRegistroDeCrescimentoPorCriancaIdAsync(int criancaId);
        Task InserirAsync(Crescimento crescimento);
        Task AtualizarAsync(Crescimento crescimento);
    }
}
