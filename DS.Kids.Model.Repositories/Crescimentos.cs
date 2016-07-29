using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Crescimentos : ICrescimentos
    {
        public async Task<IEnumerable<Model.Crescimento>> ListarPorCriancaIdAsync(int criancaId)
        {
            Throw.IfLessThanOrEqZero(criancaId);

            using (var context = new Context())
            {
                return await context.Crescimentos.Where(c => c.IdCrianca == criancaId).ToListAsync();
            }
        }

        public async Task<Model.Crescimento> ObterUltimoRegistroDeCrescimentoPorCriancaIdAsync(int criancaId)
        {
            Throw.IfLessThanOrEqZero(criancaId);

            using (var context = new Context())
            {
                //isso tem cara que não é muito certo de se fazer....
                var items = context.Crescimentos.Where(c => c.IdCrianca == criancaId).OrderByDescending(c => c.IdCrescimento).Take(1);
                return await items.FirstOrDefaultAsync();
            }
        }

        public async Task InserirAsync(Model.Crescimento crescimento)
        {
            Throw.IfIsNull(crescimento);

            using (var context = new Context())
            {
                context.Crescimentos.Add(crescimento);
                await context.SaveChangesAsync();
            }
        }

        public async Task AtualizarAsync(Model.Crescimento crescimento)
        {
            Throw.IfIsNull(crescimento);

            using (var context = new Context())
            {
                context.Entry(crescimento).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
