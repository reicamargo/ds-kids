using DS.Kids.Model.Communication.Support;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
    public class Cardapios : Services.ICardapio
    {
        public async Task<Model.Result<Model.Cardapio>> ObterAsync(int mesesIdade)
        {
            var url = string.Format(Endpoints.CARDAPIOS, mesesIdade);
            var result = await Rest.GetAsync<Model.Result<Model.Cardapio>>(url);
            return result;
        }

        public async Task<Model.Result<Refeicao>> SubstituirRefeicaoAsync(int mesesIdade, Model.TipoRefeicao tipoRefeicao)
        {
            var url = string.Format(Endpoints.CARDAPIOS_POR_TIPO_REFEICAO, mesesIdade, (int)tipoRefeicao);
            var result = await Rest.GetAsync<Model.Result<Refeicao>>(url);
            return result;
        }
    }
}
