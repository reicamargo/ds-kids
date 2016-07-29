using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class RefeicoesGrupos : IRefeicoesGrupos, IDisposable
    {

        private readonly Context _context = new Context();

        public async Task<RefeicaoGrupo> ObterPorIdAsync(int idRefeicaoGrupo)
        {
            Throw.IfLessThanOrEqZero(idRefeicaoGrupo);

            return await ObterContextComIncludes(_context)
                                .FirstOrDefaultAsync(r => r.IdRefeicaoGrupo == idRefeicaoGrupo);
        }

        public async Task InserirAsync(RefeicaoGrupo refeicaoGrupo)
        {
            Throw.IfIsNull(refeicaoGrupo);

            _context.RefeicoesGrupo.Add(refeicaoGrupo);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(RefeicaoGrupo refeicaoGrupo)
        {
            Throw.IfIsNull(refeicaoGrupo);

            _context.Entry(refeicaoGrupo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(RefeicaoGrupo refeicaoGrupo)
        {
            Throw.IfIsNull(refeicaoGrupo);

            _context.Entry(refeicaoGrupo).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo)
        {
            Throw.IfIsNull(refeicaoGrupo);

            if (refeicaoGrupo.Alimentos.Any(a => a.IdAlimento == idAlimento))
            {
                throw new DuplicateEntityException();
            }

            var alimento = new Alimento { IdAlimento = idAlimento };
                
            _context.Alimentos.Attach(alimento);
            
            _context.RefeicoesGrupo.Attach(refeicaoGrupo);

            refeicaoGrupo.Alimentos.Add(alimento);

            await _context.SaveChangesAsync();
        }

        public async Task RemoverAlimento(int idAlimento, RefeicaoGrupo refeicaoGrupo)
        {
            Throw.IfIsNull(refeicaoGrupo);

            var alimento = refeicaoGrupo.Alimentos.FirstOrDefault(a => a.IdAlimento == idAlimento);

            refeicaoGrupo.Alimentos.Remove(alimento);

            await _context.SaveChangesAsync();
        }

        private static IQueryable<RefeicaoGrupo> ObterContextComIncludes(Context context)
        {
            return context.RefeicoesGrupo
                .Include(rg => rg.Alimentos)
                .Include(rg => rg.RefeicaoDiario.RefeicoesGrupos)
                .Include(rg => rg.Alimentos.Select(a => a.AlimentosMedidasFaixasEtarias));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
