using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface IRefeicoesGrupos
    {
        Task<RefeicaoGrupo> ObterPorIdAsync(int idRefeicaoGrupo);
        Task InserirAsync(RefeicaoGrupo refeicaoGrupo);
        Task AtualizarAsync(RefeicaoGrupo refeicaoGrupo);
        Task RemoverAsync(RefeicaoGrupo refeicaoGrupo);
        Task AdicionarAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo);
        Task RemoverAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo);
    }
}
