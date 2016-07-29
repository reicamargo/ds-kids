using Newtonsoft.Json;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma brincadeira.
    /// </summary>
    public class Brincadeira : Support.BaseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }                
        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }        
        /// <summary>
        /// Lista de instru��es sobre a brincadeira
        /// </summary>
        public string Instrucoes { get; set; }
        /// <summary>
        /// Faixa et�ria recomendada
        /// </summary>
        public string FaixaEtaria { get; set; }
        /// <summary>
        /// Ambiente onde se pode fazer a brincadeira
        /// </summary>
        public TipoAmbiente Ambiente { get; set; }
        /// <summary>
        /// Nome da imagem
        /// </summary>
        public string NomeImagem { get; set; }
        /// <summary>
        /// Imagem da brincadeira
        /// </summary>
        [JsonIgnore]
        public byte[] Imagem { get; set; }
        /// <summary>
        /// A brincadeira est� ativa ou n�o
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Url imagem
        /// </summary>
        public virtual string UrlImagem
        {
            get { return string.Format("imagens/brincadeiras/{0}_{1}", this.Id, this.NomeImagem.Remove(this.NomeImagem.LastIndexOf('.'))); }
        }
        
        /// <summary>
        /// Lista de objetivos da brincadeira
        /// </summary>
        public ICollection<Model.Objetivo> Objetivos { get; set; }
        /// <summary>
        /// Materiais Necess�rios
        /// </summary>
        public ICollection<Model.Material> Materiais { get; set; }
        
        /// <summary>
        /// Construtor Padr�o
        /// </summary>
        public Brincadeira()
        {
            this.Objetivos = new List<Objetivo>();
            this.Materiais = new List<Material>();
        }
    }
}