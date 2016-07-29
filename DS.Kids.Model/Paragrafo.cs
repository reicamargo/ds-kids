using Newtonsoft.Json;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public class Paragrafo
    {
        public int IdParagrafo { get; set; }
        public int IdDica { get; set; }
        public virtual Dica Dica { get; set; }
        public TipoParagrafo TipoParagrafo { get; set; }
        public string Texto { get; set; }
        public string Video { get; set; }
        [JsonIgnore]
        public byte[] Imagem { get; set; }
        /// <summary>
        /// Url imagem
        /// </summary>
        public virtual string UrlImagem
        {
            get {
                if (this.TipoParagrafo == Model.TipoParagrafo.Imagem)
                    return string.Format("imagens/paragrafos/{0}", this.IdParagrafo);
                else
                    return string.Empty;
            }
        }
        public bool Ativo { get; set; }
    }
}
