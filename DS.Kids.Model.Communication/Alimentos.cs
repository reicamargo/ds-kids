using System.Collections.Generic;
using System.Threading.Tasks;

using DS.Kids.Model.Communication.Support;
using DS.Kids.Model.Services;

namespace DS.Kids.Model.Communication
{
    public class Alimentos : IAlimento
    {
        public async Task<Result<IEnumerable<Alimento>>> ObterPorGrupoAlimentar(int mesesDeIdade, int idGrupo)
        {
            var url = string.Format(Endpoints.ALIMENTOS, mesesDeIdade, idGrupo);
            var result = await Rest.GetAsync<Result<IEnumerable<Alimento>>>(url);
            return result;
        }

    }
}
