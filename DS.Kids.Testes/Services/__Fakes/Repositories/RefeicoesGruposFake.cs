using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class RefeicoesGruposFake : IRefeicoesGrupos
    {
        private readonly Database _database;

        public RefeicoesGruposFake(Database database)
        {
            _database = database;
        }

        public async Task InserirAsync(RefeicaoGrupo refeicaoGrupo)
        {
            await Task.Delay(0);

            _database.refeicoes_Grupo.Add(refeicaoGrupo);

            var refeicaoDiario = _database.refeicoes_Diario.FirstOrDefault(x => x.IdRefeicao == refeicaoGrupo.IdRefeicao);
            if (refeicaoDiario != null)
            {
                refeicaoDiario.RefeicoesGrupos.Add(refeicaoGrupo);
                refeicaoGrupo.IdRefeicaoGrupo = _database.refeicoes_Grupo.Count;
                refeicaoGrupo.RefeicaoDiario = refeicaoDiario;
            }
        }

        public async Task AtualizarAsync(RefeicaoGrupo refeicaoGrupo)
        {
            await Task.Delay(0);

            var oldItem = _database.refeicoes_Grupo.FirstOrDefault(x => x.IdRefeicaoGrupo == refeicaoGrupo.IdRefeicaoGrupo);
            _database.refeicoes_Grupo[_database.refeicoes_Grupo.IndexOf(oldItem)] = refeicaoGrupo;
        }

        public async Task RemoverAsync(RefeicaoGrupo refeicaoGrupo)
        {
            await Task.Delay(0);
            _database.refeicoes_Grupo.Remove(refeicaoGrupo);

            foreach (var refGrupo in _database.refeicoes_Grupo)
            {
                refGrupo.RefeicaoDiario.RefeicoesGrupos.Remove(refeicaoGrupo);
            }
        }

        public async Task<RefeicaoGrupo> ObterPorIdAsync(int idRefeicaoGrupo)
        {
            await Task.Delay(0);
            return _database.refeicoes_Grupo.FirstOrDefault(x => x.IdRefeicaoGrupo == idRefeicaoGrupo);
        }

        public async Task AdicionarAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo)
        {
            await Task.Delay(0);

            var alimento = _database.alimentos.FirstOrDefault(x => x.IdAlimento == idAlimento);
            if(refeicaoGrupo.Alimentos.Contains(alimento))
            {
                throw new DuplicateEntityException();
            }

            refeicaoGrupo.Alimentos.Add(alimento);
        }

        public async Task RemoverAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo)
        {
            await Task.Delay(0);

            var alimento = _database.alimentos.FirstOrDefault(x => x.IdAlimento == idAlimento);
            refeicaoGrupo.Alimentos.Remove(alimento);
        }

    }
}
