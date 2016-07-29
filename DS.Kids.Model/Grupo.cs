using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public partial class Grupo
    {
        public Grupo()
        {
            this.Alimentos = new List<Alimento>();
            this.RefeicoesGrupos = new List<RefeicaoGrupo>();
        }

        public int GrupoId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Alimento> Alimentos { get; set; }
        public virtual ICollection<RefeicaoGrupo> RefeicoesGrupos { get; set; }
    }
}
