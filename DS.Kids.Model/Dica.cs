using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma dica para um ou mais alimentos
    /// </summary>
    public class Dica
    {
        /// <summary>
        /// Id da Dica
        /// </summary>
        public int IdDica { get; set; }
        /// <summary>
        /// Id da Categoria
        /// </summary>
        public int IdCategoria { get; set; }
        /// <summary>
        /// Categoria
        /// </summary>
        public virtual Categoria Categoria { get; set; }
        /// <summary>
        /// Título da Dica
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Url da Dica
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Indica se a dica está ativa ou não
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Indica se a dica tem destaque ou não
        /// </summary>
        public bool? Destaque { get; set; }
        /// <summary>
        /// Alimentos com essa dica
        /// </summary>
        public List<Alimento> Alimentos { get; set; }
        /// <summary>
        /// Paragrafos
        /// </summary>
        public List<Paragrafo> Paragrafos { get; set; }

        /// <summary>
        /// Id do Parceiro
        /// </summary>
        public int? IdParceiro { get; set; }

        /// <summary>
        /// A dica pode ser patrocinada por um parceiro
        /// </summary>
        public virtual Parceiro Parceiro { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Dica()
        {
            this.Alimentos = new List<Alimento>();
            this.Paragrafos = new List<Paragrafo>();
        }
    }
}
