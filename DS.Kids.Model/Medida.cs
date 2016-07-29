using Newtonsoft.Json;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma medida
    /// </summary>
    public class Medida
    {
        /// <summary>
        /// Id da Medida
        /// </summary>
        [JsonIgnore]
        public int IdMedida { get; set; }
        /// <summary>
        /// Nome da Medida
        /// </summary>        
        public string Nome { get; set; }
        /// <summary>
        /// Nome da Medida no singular
        /// </summary>
        [JsonIgnore]
        public string NomeSingular { get; set; }
        /// <summary>
        /// Nome no plural da Medida
        /// </summary>
        [JsonIgnore]
        public string NomePlural { get; set; }
        /// <summary>
        /// Refeições que usam essa medida
        /// </summary>
        public virtual List<RefeicaoItem> RefeicoesItens { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<AlimentoMedidaFaixaEtaria> AlimentosMedidasFaixasEtarias { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public Medida()
        {
            AlimentosMedidasFaixasEtarias = new List<AlimentoMedidaFaixaEtaria>();
            RefeicoesItens = new List<RefeicaoItem>();
        }

        public void RefreshNome(decimal quantidade)
        {
            Nome = quantidade >= 2 ? NomePlural : NomeSingular;
        }
    }
}
