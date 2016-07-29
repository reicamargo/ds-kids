using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    public class EsqueciMinhaSenhaController : ApiController
    {
        private readonly Services.ISenha _service;
        public EsqueciMinhaSenhaController(Services.Events.ISenha events, Repositories.IResponsaveis responsaveis)
        {
            this._service = new DS.Kids.Model.Services.Senha(events, responsaveis);
        }

        public async Task<Model.Result> Post(Model.Login.EsqueciMinhaSenha esqueciMinhaSenha)
        {
            var result = await this._service.EsqueciAsync(esqueciMinhaSenha.Email);
            return result;
        }
    }
}
