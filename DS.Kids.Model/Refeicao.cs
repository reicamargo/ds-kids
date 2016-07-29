using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Refeição. A refeição é por faixa etária
    /// </summary>
    public class Refeicao
    {
        /// <summary>
        /// Id da Refeição
        /// </summary>
        public int IdRefeicao { get; set; }
        /// <summary>
        /// Id da Faixa Etária
        /// </summary>
        public int IdFaixaEtaria { get; set; }
        /// <summary>
        /// Faixa Etária para a Refeição
        /// </summary>
        public virtual FaixaEtaria FaixaEtaria { get; set; }
        /// <summary>
        /// Tipo da Refeição
        /// </summary>
        public virtual TipoRefeicao TiposRefeicao { get; set; }
        /// <summary>
        /// Data de Criação
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Indica se a refeição está ativa ou não
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Itens da Refeição
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
        /// Construtor Padrão
        /// </summary>
        public Refeicao()
        {
            this.RefeicoesItens = new List<RefeicaoItem>();
        }
    }
}
