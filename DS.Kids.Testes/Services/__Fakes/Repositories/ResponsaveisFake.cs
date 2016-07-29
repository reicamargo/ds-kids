using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class ResponsaveisFake : IResponsaveis
    {
        private readonly Database _database;

        public ResponsaveisFake(Database database)
        {
            _database = database;
        }

        public async Task<Responsavel> ObterPorEmailAsync(string email)
        {
            await Task.Delay(0);
            lock (_database.responsaveis)
            {
                return _database.responsaveis.FirstOrDefault(d => d.Email.Equals(email)); 
            }
        }

        public async Task<Responsavel> ObterPorIdAsync(int id)
        {
            await Task.Delay(0);
            lock (_database.responsaveis)
            {
                return _database.responsaveis.FirstOrDefault(d => d.IdResponsavel == id); 
            }
        }

        public async Task InserirAsync(Responsavel responsavel)
        {
            lock (_database.responsaveis)
            {
                var existente = _database.responsaveis.FirstOrDefault(d => d.Email.Equals(responsavel.Email));
                if (existente != null)
                    throw new DuplicateEntityException();

                responsavel.IdResponsavel = _database.responsaveis.Count + 1;
                _database.responsaveis.Add(responsavel); 
            }
            await Task.Delay(0);
        }

        public async Task AtualizarAsync(Responsavel responsavel)
        {
            lock (_database.responsaveis)
            {
                _database.responsaveis.RemoveAll(d => d.IdResponsavel == responsavel.IdResponsavel);
                _database.responsaveis.Add(responsavel);
            }
            await Task.Delay(0);
        }
    }
}
