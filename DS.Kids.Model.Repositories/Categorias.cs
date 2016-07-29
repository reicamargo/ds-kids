using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Categorias : ICategorias
    {
        public async Task<Model.Categoria> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);
            using (var context = new Context())
            {
                var lista = await this.ObterContextComIncludes(context)
                                 .FirstOrDefaultAsync(c => c.IdCategoria == id);

                lista.Dicas.RemoveAll(Inativo);
                lista.Dicas.Sort(delegate (Dica x, Dica y)
               {
                   if (x.Destaque.HasValue && x.Destaque.Value) return -1;

                   return x.Titulo.CompareTo(y.Titulo);
               });

                return lista;
            }
        }

        private bool Inativo(Dica dica)
        {
            return !dica.Ativo;
        }

        public async Task<IEnumerable<Model.Categoria>> ListarAsync()
        {
            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .Where(c => c.Ativo == true).ToListAsync();
            }
        }

        private IQueryable<Model.Categoria> ObterContextComIncludes(Context context)
        {
            return context.Categorias.AsNoTracking()
                                     .Include(d => d.Dicas)
                                     .Include(d => d.Dicas.Select(p => p.Paragrafos))
                                     .Include(d => d.Dicas.Select(pa => pa.Parceiro));
        }
    }
}
