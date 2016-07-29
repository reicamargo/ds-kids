using System.Threading.Tasks;
using System.Web.Http;
using DS.Kids.Model;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class OptinController : ApiController
    {
        private readonly Model.Services.IOptin _service;
        public OptinController(Model.Repositories.IResponsaveis responsaveis)
        {
            this._service = new DS.Kids.Model.Services.Optin(responsaveis);
        }

        public async Task<Model.Result> Put(Optin optin)
        {
            var result = await this._service.SetAsync(optin);
            return result;
        }
    }
}
