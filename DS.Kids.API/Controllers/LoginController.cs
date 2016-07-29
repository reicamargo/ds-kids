using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    public class LoginController : ApiController
    {
        private readonly Services.ILogin _service;
        public LoginController(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens, Repositories.ILoginsSociais loginsSociais)
        {
            this._service = new DS.Kids.Model.Services.Login(responsaveis, tokens, loginsSociais);
        }

        public async Task<Model.Result<Model.Responsavel>> Post(Model.Login login)
        {
            var result = await this._service.LogarAsync(login);
            return result;
        }
    }
}
