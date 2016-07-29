using System;
using System.Threading.Tasks;
using System.Web.Http;
using DS.Kids.API.Filters;
using DS.Kids.Model;
using DS.Kids.Model.Repositories;
using DS.Kids.Model.Services;
using Diario = DS.Kids.Model.Diario;

namespace DS.Kids.API.Controllers
{
    [Authorization]
    public class DiarioController : ApiController
    {
        private readonly IDiario _service;
        public DiarioController(IRefeicoesDiarios refeicoesDiarios, IRefeicoesGrupos refeicoesGrupos, ICriancas criancas)
        {
            _service = new Model.Services.Diario(refeicoesDiarios, refeicoesGrupos, criancas);
        }
        
        public async Task<Result<Diario>> Get(int criancaId, DateTime data)
        {
            var result = await _service.ObterPorIdDataAsync(criancaId, data);
            return result;
        }

        public async Task<Result<DiarioDTO>> Post(DiarioDTO diarioDTO)
        {
            var result = await _service.AtualizarAsync(diarioDTO);
            return result;
        }

        public async Task<Result<DiarioDTO>> Delete(int idRefeicaoGrupo, int idAlimento)
        {
            var result = await _service.RemoverAlimentoAsync(idRefeicaoGrupo, idAlimento);
            return result;
        }
    }
}
