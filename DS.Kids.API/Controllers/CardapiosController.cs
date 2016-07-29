using DS.Kids.API.Filters;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2.TimeAttributes;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    [Parceiro]
    public class CardapiosController : ApiController
    {
        private readonly Services.ICardapio _service;
        public CardapiosController(Repositories.IRefeicoes refeicoes, Repositories.IParceiro parceiro)
        {
            this._service = new Services.Cardapio(refeicoes, parceiro);
        }

        public async Task<Model.Result<Model.Cardapio>> Get(int mesesIdade)
        {
            var result = await this._service.ObterAsync(mesesIdade);
            return result;
        }

        public async Task<Model.Result<Model.Refeicao>> Get(int mesesIdade, Model.TipoRefeicao tipoRefeicao)
        {
            var result = await this._service.SubstituirRefeicaoAsync(mesesIdade, tipoRefeicao);
            return result;
        }
    }
}
