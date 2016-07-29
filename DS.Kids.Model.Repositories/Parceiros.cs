using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Parceiros : IParceiros
    {
        public async Task<IEnumerable<Parceiro>> ListarAsync()
        {
            using (var context = new Context())
            {
                return await context.Parceiros
                    .Where(p => p.Ativo)
                    .OrderByDescending(p => p.IdParceiro)                    
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Parceiro>> ListarPorTipoAsync(TipoParceiro tipo)
        {
            using (var context = new Context())
            {
                return await context.Parceiros
                    .Where(p => p.Ativo && p.Tipo == tipo)
                    .ToListAsync();
            }
        }

        public async Task<Model.Parceiro> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                return await context.Parceiros.FirstOrDefaultAsync(p => p.Ativo && p.IdParceiro == id);
            }
        }
    }
}
