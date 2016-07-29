using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class CrescimentosFake : ICrescimentos
    {
        private readonly Database _database;

        public CrescimentosFake(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Crescimento>> ListarPorCriancaIdAsync(int criancaId)
        {
            await Task.Delay(0);
            lock (_database.crescimentos)
            {
                return _database.crescimentos.Where(d => d.IdCrianca == criancaId);
            }
        }

        public async Task<Crescimento> ObterUltimoRegistroDeCrescimentoPorCriancaIdAsync(int criancaId)
        {
            await Task.Delay(0);
            lock (_database.crescimentos)
            {
                return _database.crescimentos.Where(d => d.IdCrianca == criancaId).OrderByDescending(c => c.IdCrescimento).FirstOrDefault();
            }
        }

        public async Task InserirAsync(Crescimento crescimento)
        {
            lock (_database.crescimentos)
            {
                crescimento.IdCrescimento = _database.crescimentos.Count + 1;
                _database.crescimentos.Add(crescimento);
                //var crianca = DATABASE.CRIANCAS.FirstOrDefault(c => c.Id == crescimento.CriancaId);
                //DATABASE.CRIANCAS.Remove(crianca);
                //crianca.Crescimentos.Add(crescimento);
                //DATABASE.CRIANCAS.Add(crianca);
            }
            await Task.Delay(0);
        }

        public async Task AtualizarAsync(Crescimento crescimento)
        {
            lock (_database.crescimentos)
            {
                var existente = _database.crescimentos.FirstOrDefault(c => c.IdCrescimento == crescimento.IdCrescimento);
                _database.crescimentos.Remove(existente);
                _database.crescimentos.Add(crescimento);
            }
            await Task.Delay(0);
        }
    }
}
