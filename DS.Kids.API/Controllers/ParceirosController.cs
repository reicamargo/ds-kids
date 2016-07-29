using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class ParceirosController : ApiController
    {
        private readonly Repositories.IParceiros _parceiros;

        public ParceirosController(Repositories.IParceiros parceiros)
        {
            _parceiros = parceiros;
        }

        [CacheOutputUntilToday]
        public async Task<Model.Result<IEnumerable<Model.Parceiro>>> Get()
        {
            var parceiros = await this._parceiros.ListarAsync();
            var result = new Model.Result<IEnumerable<Model.Parceiro>>(parceiros);
            return result;
        }

        [ActionName("ObterPorId")]
        [CacheOutputUntilToday]
        public async Task<Model.Result<IEnumerable<Model.Parceiro>>> Get(Model.TipoParceiro id)
        {
            var parceiros = await this._parceiros.ListarPorTipoAsync(id);
            var result = new Model.Result<IEnumerable<Model.Parceiro>>(parceiros);
            return result;
        }
    }
}