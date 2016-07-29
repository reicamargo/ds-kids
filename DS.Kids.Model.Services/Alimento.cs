using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model.Repositories;

namespace DS.Kids.Model.Services
{
    public class Alimento : IAlimento
    {
        private readonly IAlimentos _alimentos;

        public Alimento(IAlimentos alimentos)
        {
            Throw.IfIsNull(alimentos);

            _alimentos = alimentos;
        }

        public async Task<Result<IEnumerable<Model.Alimento>>> ObterPorGrupoAlimentar(int mesesDeIdade, int idGrupo)
        {
            var alimentos = await _alimentos.ObterPorGrupoAlimentar(idGrupo);
            Func<AlimentoMedidaFaixaEtaria, bool> faixaEtariaValida = x => x.FaixasEtaria.MesesDeIdadeInicial <= mesesDeIdade &&
                                                                            mesesDeIdade <= x.FaixasEtaria.MesesDeIdadeFinal;

            var data = alimentos as IList<Model.Alimento> ?? alimentos.ToList();
            foreach (var alimento in data)
            {
                alimento.AlimentosMedidasFaixasEtarias = alimento.AlimentosMedidasFaixasEtarias.Where(faixaEtariaValida).ToList();

                // Removendo propriedades desnecessárias no retorno do serviço
                var alimentoMedidaFaixaEtaria = alimento.AlimentosMedidasFaixasEtarias.FirstOrDefault();
                if(alimentoMedidaFaixaEtaria != null)
                {
                    alimentoMedidaFaixaEtaria.FaixasEtaria = null;
                    alimentoMedidaFaixaEtaria.Medida.RefreshNome(alimentoMedidaFaixaEtaria.Quantidade);
                    alimentoMedidaFaixaEtaria.Medida.RefeicoesItens = null;
                    alimentoMedidaFaixaEtaria.Medida.AlimentosMedidasFaixasEtarias = null;
                }
                alimento.RefeicoesGrupos = null;
                alimento.RefeicoesItens = null;
            }

            var retornoAlimentos = data.Where(amfe => amfe.AlimentoMedidaFaixaEtaria != null).OrderBy( a => a.Nome).OrderByDescending(d => d.Destaque);

            return new Result<IEnumerable<Model.Alimento>>(retornoAlimentos);
        }
    }
}
