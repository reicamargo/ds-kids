using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class CriancasFake : ICriancas
    {
        private readonly Database _database;

        public CriancasFake(Database database)
        {
            _database = database;
        }

        public async Task<Crianca> ObterPorIdAsync(int id)
        {
            await Task.Delay(0);
            lock (_database.criancas)
            {
                return _database.criancas.FirstOrDefault(d => d.IdCrianca == id); 
            }
        }

        public async Task<IEnumerable<Crianca>> ListarPorResponsavelIdAsync(int id)
        {
            await Task.Delay(0);
            lock (_database.criancas)
            {
                return _database.criancas.Where(r => r.Responsavel.IdResponsavel == id); 
            }
        }

        public async Task InserirAsync(Crianca crianca)
        {
            lock (_database.criancas)
            {
                crianca.IdCrianca = _database.criancas.Count + 1;
                _database.criancas.Add(crianca); 
            }
            await Task.Delay(0);
        }

        public async Task AtualizarAsync(Crianca crianca)
        {
            await Task.Delay(0);
            lock (_database.criancas)
            {
                var criancaExistente = _database.criancas.FirstOrDefault(d => d.IdCrianca == crianca.IdCrianca);
                _database.criancas.Remove(criancaExistente);
                _database.criancas.Add(crianca); 
            }
        }


        public async Task InativarAsync(int id)
        {
            await Task.Delay(0);
            lock (_database.criancas)
            {
                var criancaExistente = _database.criancas.FirstOrDefault(d => d.IdCrianca == id);
                _database.criancas.Remove(criancaExistente);
            }
        }
    }
}