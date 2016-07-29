using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class TrocaDeSenhaController : ApiController
    {
        private Services.Senha _service;
        public TrocaDeSenhaController(Services.Events.ISenha events, Repositories.IResponsaveis responsaveis)
        {
            this._service = new Services.Senha(events, responsaveis);
        }

        public async Task<Model.Result> Post(Model.TrocaDeSenha trocaSenha)
        {
            var result = await this._service.TrocaAsync(trocaSenha);
            return result;            
        }
    }
}
