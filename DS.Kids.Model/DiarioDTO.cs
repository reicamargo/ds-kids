using System;

namespace DS.Kids.Model
{
    public class DiarioDTO
    {
        public int IdCrianca { get; set; }
        public DateTime Data { get; set; }
        public TipoGrupoRefeicao IdGrupo { get; set; }
        public TipoRefeicao IdTipoRefeicao { get; set; }
        
        public bool Checked { get; set; }
        public int? IdAlimento { get; set; }
        
        public int? IdRefeicao { get; set; }
        public int? IdRefeicaoGrupo { get; set; }
    }
}
