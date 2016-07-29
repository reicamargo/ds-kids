using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Dicas : IDicas
    {
        public async Task<Model.Dica> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context).FirstOrDefaultAsync(d => d.IdDica == id);
            }
        }

        private IQueryable<Model.Dica> ObterContextComIncludes(Context context)
        {
            return context.Dicas.AsNoTracking()
                                     .Include(d => d.Parceiro)
                                     .Include(d => d.Paragrafos);
        }
    }
}
