using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um Material para uma ou mais Brincadeiras
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Id co Material
        /// </summary>
        public int IdMaterial { get; set; }
        /// <summary>
        /// Descrição do Material
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Brincadeiras que usam esse material
        /// </summary>
        public ICollection<Brincadeira> Brincadeiras { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public Material()
        {
            this.Brincadeiras = new List<Brincadeira>();
        }

        /// <summary>
        /// Retorna a Descrição do Material
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Descricao;
        }
    }
}