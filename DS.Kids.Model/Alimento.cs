using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um Alimento
    /// </summary>
    public class Alimento
    {
        /// <summary>
        /// Id do Alimento
        /// </summary>
        public int IdAlimento { get; set; }
        /// <summary>
        /// Nome do Alimento
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Id da Dica
        /// </summary>
        public int? IdDica { get; set; }

        /// <summary>
        /// id do Grupo
        /// </summary>
        [JsonIgnore]
        public int? IdGrupo { get; set; }

        /// <summary>
        /// Se o alimento está ativo ou não
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Itens da Refeição
        /// </summary>
        public virtual List<RefeicaoItem> RefeicoesItens { get; set; }
        public virtual List<AlimentoMedidaFaixaEtaria> AlimentosMedidasFaixasEtarias { get; set; }

        [JsonIgnore]
        public AlimentoMedidaFaixaEtaria AlimentoMedidaFaixaEtaria
        {
            get { return AlimentosMedidasFaixasEtarias.FirstOrDefault(); }
        }

        public virtual List<RefeicaoGrupo> RefeicoesGrupos { get; set; }

        /// <summary>
        /// Dicas
        /// </summary>
        public virtual Dica Dica { get; set; }

        /// <summary>
        /// Dicas
        /// </summary>
        public virtual Grupo Grupo { get; set; }
        public virtual List<DestaqueAlimento> DestaquesAlimentos { get; set; }
        [JsonIgnore]
        public bool Destaque { get; set; }

        public Alimento()
        {
            this.AlimentosMedidasFaixasEtarias = new List<AlimentoMedidaFaixaEtaria>();
            this.RefeicoesItens = new List<RefeicaoItem>();
            this.RefeicoesGrupos = new List<RefeicaoGrupo>();
            this.DestaquesAlimentos = new List<DestaqueAlimento>();
        }
    }
}
