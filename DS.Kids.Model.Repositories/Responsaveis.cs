using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Responsaveis : IResponsaveis
    {
        public async Task<Model.Responsavel> ObterPorEmailAsync(string email)
        {
            Throw.IfIsNullOrEmpty(email);

            using (var context = new Context())
            {
                var responsavel = await this.ObterContextComIncludes(context)
                                 .FirstOrDefaultAsync(r => r.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));


                return this.RemoveCriancasInativas(responsavel);
            }
        }

        public async Task<Model.Responsavel> ObterPorIdAsync(int id)
        {
            Throw.IfLessThanOrEqZero(id);

            using (var context = new Context())
            {
                var responsavel = await this.ObterContextComIncludes(context)
                                            .FirstOrDefaultAsync(r => r.IdResponsavel == id);

                return this.RemoveCriancasInativas(responsavel);
            }
        }

        private IQueryable<Model.Responsavel> ObterContextComIncludes(Context context)
        {
            return context.Responsaveis.AsNoTracking()
                                .Include(c => c.Criancas)
                                .Include(c => c.Criancas.Select(x => x.Crescimentos))
                                .Include(c => c.Token)
                                .Include(c => c.LoginSocial);
        }

        /*
         TODO: É preciso ver como fazer esse filtro no EF
         */
        private Model.Responsavel RemoveCriancasInativas(Model.Responsavel responsavel)
        {
            if (responsavel == null) return null;

            var criancasInativas = responsavel.Criancas.Where(c => !c.Ativo).ToList();
            foreach (var crianca in criancasInativas)
                responsavel.Criancas.Remove(crianca);

            return responsavel;
        }

        public async Task InserirAsync(Model.Responsavel responsavel)
        {
            Throw.IfIsNull(responsavel);

            try
            {
                using (var context = new Context())
                {
                    context.Responsaveis.Add(responsavel);
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException)
            {
                throw new Repositories.DuplicateEntityException();
            }
        }

        public async Task AtualizarAsync(Model.Responsavel responsavel)
        {
            Throw.IfIsNull(responsavel);

            using (var context = new Context())
            {
                context.Entry(responsavel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
