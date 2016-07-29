using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class DicasController : ApiController
    {
        private readonly Repositories.IDicas _dicas;

        public DicasController(Repositories.IDicas dicas)
        {
            _dicas = dicas;
        }

        [CacheOutputUntilToday]
        public async Task<Model.Result<Model.Dica>> Get(int id)
        {
            var dica = await this._dicas.ObterPorIdAsync(id);
            var result = new Model.Result<Model.Dica>(dica);
            return result;
        }
    }
}