using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Faixa Etária da Criança.
    /// Através dessa faixa etária, é obtido o cardápio sugerido para a criança
    /// </summary>
    public class FaixaEtaria
    {
        /// <summary>
        /// Id da Faixa Etária
        /// </summary>
        public int IdFaixaEtaria { get; set; }
        /// <summary>
        /// Meses de Idade Inicial
        /// </summary>
        public int MesesDeIdadeInicial { get; set; }
        /// <summary>
        /// Meses de Idade Final
        /// </summary>
        public int MesesDeIdadeFinal { get; set; }
        /// <summary>
        /// Descrição da Faixa Etária
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Refeições para essa faixa etária
        /// </summary>
        public virtual List<Refeicao> Refeicoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<AlimentoMedidaFaixaEtaria> AlimentosMedidasFaixasEtarias { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public FaixaEtaria()
        {
            this.AlimentosMedidasFaixasEtarias = new List<AlimentoMedidaFaixaEtaria>();
            this.Refeicoes = new List<Refeicao>();
        }
    }
}
