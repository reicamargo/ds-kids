using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Faixa Et�ria da Crian�a.
    /// Atrav�s dessa faixa et�ria, � obtido o card�pio sugerido para a crian�a
    /// </summary>
    public class FaixaEtaria
    {
        /// <summary>
        /// Id da Faixa Et�ria
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
        /// Descri��o da Faixa Et�ria
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Refei��es para essa faixa et�ria
        /// </summary>
        public virtual List<Refeicao> Refeicoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<AlimentoMedidaFaixaEtaria> AlimentosMedidasFaixasEtarias { get; set; }

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public FaixaEtaria()
        {
            this.AlimentosMedidasFaixasEtarias = new List<AlimentoMedidaFaixaEtaria>();
            this.Refeicoes = new List<Refeicao>();
        }
    }
}
