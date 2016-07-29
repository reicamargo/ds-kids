using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class BrincadeirasController : ApiController
    {
        private readonly Repositories.IBrincadeiras _brincadeira;
        public BrincadeirasController(Repositories.IBrincadeiras brincadeiras)
        {
            _brincadeira = brincadeiras;
        }

        [ActionName("ObterPorId")]
        [CacheOutputUntilToday]
        public async Task<Model.Result<Model.Brincadeira>> Get(int id)
        {
            var brincadeira = await this._brincadeira.ObterPorIdAsync(id);
            var result = new Model.Result<Model.Brincadeira>(brincadeira);
            return result;
        }

        [CacheOutputUntilToday]
        public async Task<Model.Result<IEnumerable<Model.Brincadeira>>> Get(int? pageSize, int? pageNumber)
        {
            var brincadeiras = await this._brincadeira.ObterUltimasBrincadeirasAsync(pageSize, pageNumber);
            var result = new Model.Result<IEnumerable<Model.Brincadeira>>(brincadeiras);
            return result;
        }
    }
}