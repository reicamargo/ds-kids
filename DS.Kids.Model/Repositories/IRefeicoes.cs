using System.Collections.Generic;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IRefeicoes
    {
        Task<Model.Refeicao> ListarPorMesesDeIdadeTipoRefeicaoAsync(int mesesDeidade, TipoRefeicao tipoRefeicao, int idParceiro); 
    }
}
