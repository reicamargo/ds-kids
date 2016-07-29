using DS.Kids.Model.Communication.Support;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
    public class Crescimentos : Services.ICrescimento
    {
        public async Task<Model.Result<Model.Crescimento>> InserirAsync(Model.PesoAltura pesoAltura)
        {
            var result = await Rest.PostAsync<Model.Result<Model.Crescimento>, Model.PesoAltura>(Endpoints.CRESCIMENTOS, pesoAltura);
            return result;
        }

        public async Task<Result<Model.Crescimento>> AtualizarAsync(Model.PesoAltura pesoAltura)
        {
            var result = await Rest.PutAsync<Model.Result<Model.Crescimento>, Model.PesoAltura>(Endpoints.CRESCIMENTOS, pesoAltura);
            return result;
        }
    }
}
