using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class TokensFake : ITokens
    {
        private readonly Database _database;

        public TokensFake(Database database)
        {
            _database = database;
        }
        
        public async Task<Token> ObterPorResponsavelIdAsync(int responsavelId)
        {
            await Task.Delay(0);
            lock (_database.tokens)
            {
                return _database.tokens.FirstOrDefault(d => d.Responsavel.IdResponsavel == responsavelId); 
            }
        }

        public async Task InserirAsync(Token token)
        {
            lock (_database.tokens)
            {
                _database.tokens.Add(token); 
            }
            await Task.Delay(0);
        }

        public async Task ExcluirPorResponsavelIdAsync(int responsavelId)
        {
            var existente = await _database.tokens.FirstOrDefaultAsync(d => d.Responsavel.IdResponsavel == responsavelId);
            lock (_database.tokens)
            {
                _database.tokens.Remove(existente); 
            }
        }

        public async Task<Token> ObterPorValorAsync(string valor)
        {
            return await _database.tokens.FirstOrDefaultAsync(d => d.Valor == valor);
        }
    }
}