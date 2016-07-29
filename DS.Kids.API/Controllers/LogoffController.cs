using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class LogoffController : ApiController
    {
        private readonly Services.ILogin _service;
        public LogoffController(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens, Repositories.ILoginsSociais loginsSociais)
        {
            this._service = new Services.Login(responsaveis, tokens, loginsSociais);
        }

        public async Task<Model.Result> Post(int id)
        {
            var result = await this._service.LogoffAsync(id);
            return result;
        }
    }
}
