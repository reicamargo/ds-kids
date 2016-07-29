using DS.Kids.Model.Communication.Support;
using DS.Kids.Model.Services;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Optin : IOptin
	{
		public async Task<Model.Result> SetAsync(Model.Optin optin)
		{
			var result = await Rest.PutAsync<Model.Result, Model.Optin>(Endpoints.OPTIN, optin);
			return result;
		}
	}
}
