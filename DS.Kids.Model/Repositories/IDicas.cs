using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IDicas
    {
        Task<Model.Dica> ObterPorIdAsync(int id);
    }
}
