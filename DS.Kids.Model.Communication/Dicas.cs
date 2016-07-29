using DS.Kids.Model.Communication.Support;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Dicas
	{
		public async Task<Model.Result<Model.Dica>> ObterPorIdAsync(int id)
		{
			var url = string.Format(Endpoints.DICA, id);
			var result = await Rest.GetAsync<Model.Result<Model.Dica>>(url);
			return result;
		}
	}
}
