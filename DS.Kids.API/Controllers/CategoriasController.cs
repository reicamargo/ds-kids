using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class CategoriasController : ApiController
    {
        private readonly Repositories.ICategorias _categoria;

        public CategoriasController(Repositories.ICategorias categorias)
        {
            _categoria = categorias;
        }

        [CacheOutputUntilToday]
        public async Task<Model.Result<Model.Categoria>> Get(int id)
        {
            var categoria = await this._categoria.ObterPorIdAsync(id);
            var result = new Model.Result<Model.Categoria>(categoria);
            return result;
        }

        [ActionName("ObterPorId")]
        [CacheOutputUntilToday]
        public async Task<Model.Result<IEnumerable<Model.Categoria>>> Get()
        {
            var categorias = await this._categoria.ListarAsync();
            var result = new Model.Result<IEnumerable<Model.Categoria>>(categorias);
            return result;
        }
    }
}