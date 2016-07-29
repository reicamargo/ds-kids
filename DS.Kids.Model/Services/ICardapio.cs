using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface ICardapio
    {
        Task<Model.Result<Model.Cardapio>> ObterAsync(int mesesIdade);
        Task<Model.Result<Refeicao>> SubstituirRefeicaoAsync(int mesesIdade, TipoRefeicao tipoRefeicao);
    }
}
