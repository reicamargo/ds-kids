using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Criancas : ICriancas
    {
        public async Task<Model.Crianca> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .FirstOrDefaultAsync(c => c.IdCrianca == id && c.Ativo);
            }
        }

        public async Task InserirAsync(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            using (var context = new Context())
            {
                context.Criancas.Add(crianca);
                await context.SaveChangesAsync();
            }
        }

        public async Task AtualizarAsync(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            using (var context = new Context())
            {
                context.Entry(crianca).State = EntityState.Modified;
                
                /*
                 * TODO: A imagem, por conter muita informação só é trafegada ao backend quando for inserida ou alterada.
                 * Nesse caso, a princípio, caso seja nula eu não altero no banco de dados
                 * Até então, não sei se essa é a melhor maneira de se fazer isso, mas foi a única. 
                 */
                if (crianca.Imagem == null || (crianca.Imagem != null && crianca.Imagem.Length == 0))
                    context.Entry(crianca).Property(c => c.Imagem).IsModified = false;
                
                await context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Model.Crianca>> ListarPorResponsavelIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);

            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .Where(r => r.IdResponsavel == id && r.Ativo)
                                 .ToListAsync();
            }
        }

        public async Task InativarAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                var crianca = await context.Criancas.FirstOrDefaultAsync(c => c.IdCrianca == id);
                if (crianca != null)
                    crianca.Ativo = false;

                await context.SaveChangesAsync();
            }
        }

        private IQueryable<Model.Crianca> ObterContextComIncludes(Context context)
        {
            return context.Criancas.AsNoTracking()
                                   .Include("Crescimentos");
        }
    }
}
