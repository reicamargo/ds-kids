using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class CrescimentosController : ApiController
    {
        private readonly Services.ICrescimento _service;
        public CrescimentosController(Repositories.ICriancas criancas, Repositories.ICrescimentos crescimentos)
        {
            this._service = new Services.Crescimento(criancas, crescimentos);
        }

        public async Task<Model.Result<Model.Crescimento>> Post(Model.PesoAltura pesoAltura)
        {
            var result = await this._service.InserirAsync(pesoAltura);
            return result;
        }

        public async Task<Model.Result<Model.Crescimento>> Put(Model.PesoAltura pesoAltura)
        {
            var result = await this._service.AtualizarAsync(pesoAltura);
            return result;
        }
    }
}
