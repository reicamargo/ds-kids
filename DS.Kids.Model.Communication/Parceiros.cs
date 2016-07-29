using DS.Kids.Model.Communication.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Parceiros
	{
        public async Task<Model.Result<IEnumerable<Model.Parceiro>>> ListarAsync()
        {
            var url = string.Format("{0}", Endpoints.PARCEIROS);
            var result = await Rest.GetAsync<Model.Result<IEnumerable<Model.Parceiro>>>(url);
            return result;
        }

        public async Task<Model.Result<IEnumerable<Model.Parceiro>>> ListarPorTipoAsync(TipoParceiro tipo)
        {
            var url = string.Format(Endpoints.PARCEIROS_POR_TIPO, tipo);
            var result = await Rest.GetAsync<Model.Result<IEnumerable<Model.Parceiro>>>(url);
            return result;
        }
    }
}