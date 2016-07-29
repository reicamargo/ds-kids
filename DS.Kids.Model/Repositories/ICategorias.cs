using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface ICategorias
    {
        Task<Model.Categoria> ObterPorIdAsync(int id);
        Task<IEnumerable<Model.Categoria>> ListarAsync();
    }
}
