using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Refei��o. A refei��o � por faixa et�ria
    /// </summary>
    public class Refeicao
    {
        /// <summary>
        /// Id da Refei��o
        /// </summary>
        public int IdRefeicao { get; set; }
        /// <summary>
        /// Id da Faixa Et�ria
        /// </summary>
        public int IdFaixaEtaria { get; set; }
        /// <summary>
        /// Faixa Et�ria para a Refei��o
        /// </summary>
        public virtual FaixaEtaria FaixaEtaria { get; set; }
        /// <summary>
        /// Tipo da Refei��o
        /// </summary>
        public virtual TipoRefeicao TiposRefeicao { get; set; }
        /// <summary>
        /// Data de Cria��o
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Indica se a refei��o est� ativa ou n�o
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Itens da Refei��o
        /// </summary>
        public virtual List<RefeicaoItem> RefeicoesItens { get; set; }
        /// <summary>
        /// Id do Parceiro
        /// </summary>
        public int? IdParceiro { get; set; }
        /// <summary>
        /// Oferecido por...
        /// </summary>
        public virtual Parceiro Parceiro { get; set; }

        /// <summary>
        /// Construtor Padr�o
        /// </summary>
        public Refeicao()
        {
            this.RefeicoesItens = new List<RefeicaoItem>();
        }
    }
}
