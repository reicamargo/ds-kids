using DS.Kids.Model.Support;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public partial class RefeicaoGrupo : BaseModel
    {

        private int _idRefeicaoGrupo;

        public RefeicaoGrupo()
        {
            Alimentos = new List<Alimento>();
        }

        public int IdRefeicaoGrupo
        {
            get
            {
                return _idRefeicaoGrupo;
            }
            set
            {
                _idRefeicaoGrupo = value;
                Notify("RefeicaoRealizada");
            }
        }

        public int IdRefeicao { get; set; }

        public int IdGrupo { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual RefeicaoDiario RefeicaoDiario { get; set; }
        public virtual IList<Alimento> Alimentos { get; set; }
    }
}
