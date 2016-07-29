using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class AlimentosFake : IAlimentos
    {
        private readonly Database _database;

        public AlimentosFake(Database database)
        {
            _database = database;
        }

        public async Task<Alimento> ObterPorId(int idAlimento)
        {
            await Task.Delay(0);

            return _database.alimentos.FirstOrDefault(a => a.IdAlimento == idAlimento);
        }

        public async Task<IEnumerable<Alimento>> ObterPorGrupoAlimentar(int idGrupo)
        {
            await Task.Delay(0);

            return _database.alimentos.Where(a => a.IdGrupo == idGrupo).ToList();
        }
    }
}
