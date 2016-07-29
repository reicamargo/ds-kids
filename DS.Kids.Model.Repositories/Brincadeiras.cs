using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Brincadeiras : IBrincadeiras
    {
        public async Task<Model.Brincadeira> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);

            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .FirstOrDefaultAsync(b => b.Id == id);
            }
        }

        public async Task<IEnumerable<Brincadeira>> ObterUltimasBrincadeirasAsync(int? pageSize, int? pageNumber)
        {
            pageSize = (pageSize.HasValue ? pageSize.Value : 10);
            pageNumber = (pageNumber.HasValue ? pageNumber.Value : 1);

            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .Where(b => b.Ativo == true)
                                 .OrderByDescending(b => b.Id)
                                 .Skip((pageNumber.Value - 1) * pageSize.Value)
                                 .Take(pageSize.Value)
                                 .ToListAsync();
            }
        }

        private IQueryable<Model.Brincadeira> ObterContextComIncludes(Context context)
        {
            return context.Brincadeiras.AsNoTracking()
                                       .Include(o => o.Objetivos)
                                       .Include(m => m.Materiais);
        }
    }
}
