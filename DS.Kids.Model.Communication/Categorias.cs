using DS.Kids.Model.Communication.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
    public class Categorias
    {
        public async Task<Model.Result<Model.Categoria>> ObterPorIdAsync(int id)
        {
            var url = string.Format(Endpoints.CATEGORIA, id);
            var result = await Rest.GetAsync<Model.Result<Model.Categoria>>(url);
            return result;
        }

        public async Task<Model.Result<IEnumerable<Model.Categoria>>> ListarAsync()
        {
            var result = await Rest.GetAsync<Model.Result<IEnumerable<Model.Categoria>>>(Endpoints.CATEGORIAS);
            return result;
        }
    }
}
