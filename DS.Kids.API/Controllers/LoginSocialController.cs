using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    public class LoginSocialController : ApiController
    {
        private readonly Services.ILogin _service;
        public LoginSocialController(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens, Repositories.ILoginsSociais loginsSociais)
        {
            this._service = new Services.Login(responsaveis, tokens, loginsSociais);
        }

        public async Task<Model.Result<Model.Responsavel>> Post(Model.LoginSocial loginSocial)
        {
            var result = await this._service.LogarRedeSocialAsync(loginSocial);
            return result;
        }
    }
}
