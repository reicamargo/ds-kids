using DS.Kids.Model.Communication.Support;
using DS.Kids.Model.Services;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Responsaveis : IResponsavel
	{
        public async Task<Model.Result<Model.Responsavel>> InserirAsync(Model.Responsavel responsavel)
		{
            var result = await Rest.PostAsync<Model.Result<Model.Responsavel>, Model.Responsavel>(Endpoints.RESPONSAVEIS, responsavel);
            Authorization.Singleton.SetToken(result);
            return result;
		}

        public async Task<Model.Result> AtualizarAsync(Model.Responsavel responsavel)
		{
            var result = await Rest.PutAsync<Model.Result, Model.Responsavel>(Endpoints.RESPONSAVEIS, responsavel);
            return result;
		}
	}
}
