using Newtonsoft.Json;

namespace DS.Kids.Model
{
    public partial class AlimentoMedidaFaixaEtaria
    {
        [JsonIgnore]
        public int IdAlimentoMedidaFaixaEtaria { get; set; }
        [JsonIgnore]
        public int IdAlimento { get; set; }
        [JsonIgnore]
        public int IdMedida { get; set; }
        [JsonIgnore]
        public int IdFaixaEtaria { get; set; }
        public decimal Quantidade { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual Medida Medida { get; set; }
        public virtual FaixaEtaria FaixasEtaria { get; set; }
        public virtual Semaforo? Semaforo { get; set; }
    }
}