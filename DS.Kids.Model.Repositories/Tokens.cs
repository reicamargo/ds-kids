using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Tokens : ITokens
    {
        public async Task<Token> ObterPorValorAsync(string valor)
        {
            Throw.IfIsNullOrEmpty(valor);

            using (var context = new Context())
            {
                return await context.Tokens.FirstOrDefaultAsync(a => a.Valor == valor);
            }
        }

        public async Task<Model.Token> ObterPorResponsavelIdAsync(int responsavelId)
        {
            Throw.IfLessThanOrEqZero(responsavelId);

            using (var context = new Context())
            {
                return await context.Tokens.FirstOrDefaultAsync(a => a.Responsavel.IdResponsavel == responsavelId);
            }
        }

        public async Task InserirAsync(Model.Token token)
        {
            Throw.IfIsNull(token);

            using (var context = new Context())
            {
                context.Tokens.Add(token);
                await context.SaveChangesAsync();
            }
        }

        public async Task ExcluirPorResponsavelIdAsync(int responsavelId)
        {
            Throw.IfLessThanOrEqZero(responsavelId);

            using (var context = new Context())
            {
                //não curti...
                var tokens = context.Tokens.Where(t => t.ResponsavelId == responsavelId);
                context.Tokens.RemoveRange(tokens);
                await context.SaveChangesAsync();
            }
        }
    }
}
