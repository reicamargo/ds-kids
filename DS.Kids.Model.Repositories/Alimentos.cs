using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DS.Kids.Model.Repositories
{
    public class Alimentos : IAlimentos
    {
        private readonly IParceiro _parceiro;

        public Alimentos(IParceiro parceiro)
        {
            this._parceiro = parceiro;
        }

        public async Task<Alimento> ObterPorId(int idAlimento)
        {
            using (var context = new Context())
            {
                return await context.Alimentos.AsNoTracking().FirstOrDefaultAsync(a => a.IdAlimento == idAlimento);
            }
        }

        public async Task<IEnumerable<Alimento>> ObterPorGrupoAlimentar(int idGrupo)
        {
            var idParceiro = this._parceiro.Obter();
            using (var context = new Context())
            {
                var retornoAlimentos = await (from a in context.Alimentos
                                              join amfe in context.AlimentosMedidasFaixasEtarias on a.IdAlimento equals amfe.IdAlimento
                                              join fe in context.FaixasEtarias on amfe.IdFaixaEtaria equals fe.IdFaixaEtaria
                                              join m in context.Medidas on amfe.IdMedida equals m.IdMedida
                                              join de in context.DestaquesAlimentos on new { x = a.IdAlimento, z = idParceiro } equals new { x = de.IdAlimento, z = de.IdParceiro } into caralhaA4
                                              from subSet in caralhaA4.DefaultIfEmpty()
                                              where a.IdGrupo.Value == idGrupo
                                              select new
                                              {
                                                  IdAlimento = a.IdAlimento,
                                                  NomeAlimento = a.Nome,
                                                  Ativo = a.Ativo,
                                                  Destaque = ((subSet.IdAlimento >= 1) ? true : false),
                                                  IdMedida = m.IdMedida,
                                                  IdFaixaEtaria = fe.IdFaixaEtaria,
                                                  Descricao = fe.Descricao,
                                                  MesesInicial = fe.MesesDeIdadeInicial,
                                                  MesesFinal = fe.MesesDeIdadeFinal,
                                                  MedidaSingular = m.NomeSingular,
                                                  MedidaPlural = m.NomePlural,
                                                  Quantidade = amfe.Quantidade,
                                                  Semaforo = amfe.Semaforo
                                              }).ToListAsync();

                var alimentos = new List<Alimento>();

                foreach (var alimento in retornoAlimentos)
                {
                    var alimentosMedidasFaixasEtarias = new List<AlimentoMedidaFaixaEtaria>();
                    alimentosMedidasFaixasEtarias.Add(new AlimentoMedidaFaixaEtaria()
                    {
                        Medida = new Medida()
                        {
                            IdMedida = alimento.IdMedida,
                            Nome = alimento.MedidaSingular,
                            NomePlural = alimento.MedidaPlural,
                            NomeSingular = alimento.MedidaSingular
                        },
                        FaixasEtaria = new FaixaEtaria()
                        {
                            IdFaixaEtaria = alimento.IdFaixaEtaria,
                            Descricao = alimento.Descricao,
                            MesesDeIdadeFinal = alimento.MesesFinal,
                            MesesDeIdadeInicial = alimento.MesesInicial
                        },
                        Quantidade = alimento.Quantidade,
                        Semaforo = alimento.Semaforo,
                        IdFaixaEtaria = alimento.IdFaixaEtaria
                    });

                    alimentos.Add(new Alimento()
                    {
                        IdAlimento = alimento.IdAlimento,
                        Nome = alimento.NomeAlimento,
                        Ativo = alimento.Ativo,
                        Destaque = alimento.Destaque,
                        AlimentosMedidasFaixasEtarias = alimentosMedidasFaixasEtarias
                    });
                }

                return alimentos;
            }
        }
    }
}
