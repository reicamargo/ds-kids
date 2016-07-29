using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Paragrafos : IParagrafos
    {
        public async Task<Paragrafo> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                return await context.Paragrafos.FirstOrDefaultAsync(p => p.IdParagrafo == id);
            }
        }

        private IQueryable<Model.Paragrafo> ObterContextComIncludes(Context context)
        {
            return context.Paragrafos.AsNoTracking();
        }
    }
}
