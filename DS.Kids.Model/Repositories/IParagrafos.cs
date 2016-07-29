using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IParagrafos
    {
        Task<Model.Paragrafo> ObterPorIdAsync(int id);
    }
}
