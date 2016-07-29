using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface IAlimento
    {
        Task<Model.Result<IEnumerable<Alimento>>> ObterPorGrupoAlimentar(int mesesDeIdade, int idGrupo);
    }
}
