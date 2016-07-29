using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public class Parceiro
    {
        public int IdParceiro { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public TipoParceiro Tipo { get; set; }
        /// <summary>
        /// Refeicao oferecida por esse parceiro
        /// </summary>
        public virtual List<Refeicao> Refeicoes { get; set; }
        /// <summary>
        /// Dica oferecida por esse parceiro
        /// </summary>
        public virtual List<Dica> Dicas { get; set; }
        [JsonIgnore]
        public byte[] Imagem { get; set; }
        [JsonIgnore]
        public byte[] Icone { get; set; }
        public string NomeIcone { get; set; }
        public string NomeImagem { get; set; }
        public virtual string UrlImagem
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NomeImagem))
                    return string.Format("imagens/parceiros/{0}_{1}", this.IdParceiro, this.NomeImagem.Remove(this.NomeImagem.LastIndexOf('.')));
                else
                    return string.Empty;
            }
        }
        public virtual string UrlIcone
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NomeIcone))
                    return string.Format("imagens/parceiros/{0}_{1}_ico", this.IdParceiro, this.NomeIcone.Remove(this.NomeIcone.LastIndexOf('.')));
                else
                    return string.Empty;
            }
        }
        public bool Destaque { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<DestaqueAlimento> DestaquesAlimentos { get; set; }

        public Parceiro()
        {
            this.DestaquesAlimentos = new List<DestaqueAlimento>();
            this.Refeicoes = new List<Refeicao>();
            this.Dicas = new List<Dica>();
        }
    }
}
