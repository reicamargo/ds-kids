using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    public class ResponsaveisController : ApiController
    {
        private readonly Services.IResponsavel _service;
        public ResponsaveisController(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens)
        {
            this._service = new DS.Kids.Model.Services.Responsavel(responsaveis, tokens);
        }

        [HttpPost]
        public async Task<Model.Result<Model.Responsavel>> Post(Model.Responsavel responsavel)
        {
            var result = await _service.InserirAsync(responsavel);
            return result;
        }

        [HttpPut]
        [Filters.Authorization]
        public async Task<Model.Result> Put(Model.Responsavel responsavel)
        {
            var result = await this._service.AtualizarAsync(responsavel);
            return result;
        }
    }
}