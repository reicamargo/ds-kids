using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IParceiros
    {
        Task<Model.Parceiro> ObterPorIdAsync(int id);
        Task<IEnumerable<Model.Parceiro>> ListarAsync();
        Task<IEnumerable<Model.Parceiro>> ListarPorTipoAsync(TipoParceiro tipoParceiro);
    }
}
