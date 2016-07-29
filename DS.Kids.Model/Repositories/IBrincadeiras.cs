using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IBrincadeiras
    {
        Task<Brincadeira> ObterPorIdAsync(int id);
        Task<IEnumerable<Brincadeira>> ObterUltimasBrincadeirasAsync(int? pageSize, int? pageNumber);
    }
}
