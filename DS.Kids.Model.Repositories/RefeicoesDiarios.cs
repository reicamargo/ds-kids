using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class RefeicoesDiarios : IRefeicoesDiarios
    {
        public async Task<IList<RefeicaoDiario>> ObterPorIdDataAsync(int idCrianca, DateTime dataDiario)
        {
            Throw.IfIsNull(dataDiario);
            Throw.IfLessThanOrEqZero(idCrianca);

            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .Where(d => d.DataCriacao == dataDiario.Date && d.IdCrianca == idCrianca)
                                 .ToListAsync();
            }
        }

        public async Task<RefeicaoDiario> ObterPorIdDataRefeicaoAsync(int idCrianca, DateTime dataDiario, TipoRefeicao tipoRefeicao)
        {       
            Throw.IfIsNull(dataDiario);
            Throw.IfLessThanOrEqZero(idCrianca);

            using (var context = new Context())
            {
                return await this.ObterContextComIncludes(context)
                                 .FirstOrDefaultAsync(d => d.DataCriacao == dataDiario.Date && d.IdCrianca == idCrianca && d.IdTipoRefeicao == (int) tipoRefeicao);
            }
        }

        public async Task InserirAsync(Model.RefeicaoDiario refeicaoDiario)
        {
            Throw.IfIsNull(refeicaoDiario);

            using (var context = new Context())
            {
                try
                {
                    context.RefeicoesDiario.Add(refeicaoDiario);
                    await context.SaveChangesAsync();

                }
                catch (DbUpdateException)
                {
                    throw new DuplicateEntityException();
                }
            }
        }

        public async Task AtualizarAsync(Model.RefeicaoDiario refeicaoDiario)
        {
            Throw.IfIsNull(refeicaoDiario);

            using (var context = new Context())
            {
                context.Entry(refeicaoDiario).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoverAsync(Model.RefeicaoDiario refeicaoDiario)
        {
            Throw.IfIsNull(refeicaoDiario);

            using (var context = new Context())
            {
                context.Entry(refeicaoDiario).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        private IQueryable<Model.RefeicaoDiario> ObterContextComIncludes(Context context)
        {
            return context.RefeicoesDiario.AsNoTracking().Include("RefeicoesGrupos.Alimentos.AlimentosMedidasFaixasEtarias.FaixasEtaria");
        }
    }
}
