using DS.Kids.Model.Communication.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Brincadeiras
	{
        public async Task<Model.Result<Model.Brincadeira>> ObterPorIdAsync(int id)
        {
            var url = string.Format(Endpoints.BRINCADEIRA, id);
            var result = await Rest.GetAsync<Result<Brincadeira>>(url);
            return result;
        }

        public async Task<Model.Result<IEnumerable<Model.Brincadeira>>> ObterUltimasBrincadeirasAsync(int? pageSize, int? pageNumber)
        {
            var url = string.Format(Endpoints.BRINCADEIRAS, pageSize.GetValueOrDefault(10), pageNumber.GetValueOrDefault(1));
            var result = await Rest.GetAsync<Model.Result<IEnumerable<Model.Brincadeira>>>(url);
            return result;
        }        
    }
}
