using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public partial class RefeicaoDiario
    {
        public RefeicaoDiario()
        {
            this.RefeicoesGrupos = new List<RefeicaoGrupo>();
        }

        public int IdRefeicao { get; set; }
        public int IdTipoRefeicao { get; set; }
        public int IdCrianca { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public virtual TipoRefeicao TipoRefeicao { get; set; }
        public virtual Crianca Crianca { get; set; }
        public virtual IList<RefeicaoGrupo> RefeicoesGrupos { get; set; }
    }
}
