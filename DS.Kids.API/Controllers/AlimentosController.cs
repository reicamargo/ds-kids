using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;
using DS.Kids.API.Filters;
using WebApi.OutputCache.V2.TimeAttributes;

namespace DS.Kids.API.Controllers
{
    [Authorization]
    [Parceiro]
    public class AlimentosController : ApiController
    {
        private readonly Services.IAlimento _alimento;

        public AlimentosController(Services.IAlimento alimento)
        {
            _alimento = alimento;
        }

        //[CacheOutputUntilToday]
        public async Task<Model.Result<IEnumerable<Model.Alimento>>> Get(int mesesDeIdade, int idGrupo)
        {
            var result = await this._alimento.ObterPorGrupoAlimentar(mesesDeIdade, idGrupo);
            return result;
        }
    }
}
