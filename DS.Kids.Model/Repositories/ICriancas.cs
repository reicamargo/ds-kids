using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface ICriancas
    {
        Task<Model.Crianca> ObterPorIdAsync(int id);
        Task<IEnumerable<Model.Crianca>> ListarPorResponsavelIdAsync(int responsavelId);
        Task InserirAsync(Crianca crianca);
        Task AtualizarAsync(Crianca crianca);
        Task InativarAsync(int id);
    }
}
