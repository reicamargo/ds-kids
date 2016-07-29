using Newtonsoft.Json;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma categoria de dica
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Id da Categoria
        /// </summary>
        public int IdCategoria { get; set; }
        /// <summary>
        /// Nome da Categoria
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Indica se a categoria � destaque
        /// </summary>
        public bool Destaque { get; set; }
        /// <summary>
        /// Indica se a categoria est� ativa ou n�o.
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Array de Bytes que representa uma imagem. Essa propriedade n�o � transitada entre o backend e frontend
        /// </summary>
        [JsonIgnore]
        public byte[] Imagem { get; set; }
        /// <summary>
        /// Nome da Imagem da Categoria
        /// </summary>
        public string NomeImagem { get; set; }
        /// <summary>
        /// Url do handler da Imagem da Categoria
        /// </summary>
        public virtual string UrlImagem
        {
            get { return string.Format("imagens/categorias/{0}_{1}", this.IdCategoria, this.NomeImagem.Remove(this.NomeImagem.LastIndexOf('.'))); }
        }
        /// <summary>
        /// Dicas
        /// </summary>
        public List<Dica> Dicas { get; set; }

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public Categoria()
        {
            this.Dicas = new List<Dica>();
        }
    }
}
