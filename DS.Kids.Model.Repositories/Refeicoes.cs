using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class Refeicoes : IRefeicoes
    {
        public async Task<Refeicao> ListarPorMesesDeIdadeTipoRefeicaoAsync(int mesesDeIdade, TipoRefeicao tipoRefeicao, int idParceiro)
        {
            Throw.IfLessThanOrEqZero(mesesDeIdade);
            using (var context = new Context())
            {
                var refeicao = await ObterContextComIncludes(context)
                                        .Where(r => r.FaixaEtaria.MesesDeIdadeInicial <= mesesDeIdade &&
                                                    r.FaixaEtaria.MesesDeIdadeFinal >= mesesDeIdade &&
                                                    r.TiposRefeicao == tipoRefeicao &&
                                                    r.Ativo == true )
                                                    //&& r.IdParceiro.Value == idParceiro)
                                        .OrderBy(r => Guid.NewGuid())
                                        .Take(1)
                                        .FirstOrDefaultAsync();

                if (refeicao == null)
                {
                    var idParceiroDefault = 7;
                    refeicao = await ListarPorMesesDeIdadeTipoRefeicaoAsync(mesesDeIdade, tipoRefeicao, idParceiroDefault);
                }
                else
                {
                    PreencheNomeMedidaPorQuantidade(refeicao.RefeicoesItens);
                }

                return refeicao;
            }
        }

        private static void PreencheNomeMedidaPorQuantidade(List<RefeicaoItem> refeicoesItens)
        {
            foreach (var item in refeicoesItens)
            {
                item.RefreshMedida();
            }
        }

        private static IQueryable<Refeicao> ObterContextComIncludes(Context context)
        {
            return context.Refeicoes.AsNoTracking()
                                     .Include(r => r.RefeicoesItens)
                                     .Include(r => r.Parceiro)
                                     .Include(r => r.RefeicoesItens.Select(ri => ri.Alimento))
                                     .Include(r => r.RefeicoesItens.Select(ri => ri.Medida))
                                     .Include(r => r.RefeicoesItens.Select(ri => ri.Alimento.Dica))
                                     .Include(r => r.RefeicoesItens.Select(ri => ri.Alimento.Dica.Categoria));
        }
    }
}