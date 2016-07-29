using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IResponsaveis
    {
        Task<Model.Responsavel> ObterPorEmailAsync(string email);
        Task<Model.Responsavel> ObterPorIdAsync(int id);
        Task InserirAsync(Model.Responsavel responsavel);
        Task AtualizarAsync(Model.Responsavel responsavel);
    }
}
