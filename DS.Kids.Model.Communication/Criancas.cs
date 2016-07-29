using DS.Kids.Model.Communication.Support;
using DS.Kids.Model.Services;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
	public class Criancas : ICrianca
	{
		public async Task<Model.Result<Model.Crianca>> InserirAsync(Model.Crianca crianca)
		{
            var url = string.Format(Endpoints.CRIANCAS, crianca.IdCrianca);
            var result = await Rest.PostAsync<Model.Result<Model.Crianca>, Model.Crianca>(url, crianca);
            return result;
		}

        public async Task<Model.Result<Model.Crianca>> AtualizarAsync(Crianca crianca)
        {
            var url = string.Format(Endpoints.CRIANCAS, crianca.IdCrianca);
            var result = await Rest.PutAsync<Model.Result<Model.Crianca>, Model.Crianca>(url, crianca);
            return result;
        }

        public async Task<Model.Result> ExcluirAsync(int id)
        {
            var url = string.Format(Endpoints.CRIANCAS, id);
            var result = await Rest.DeleteAsync<Model.Result>(url);
            return result;
        }
    }
}
