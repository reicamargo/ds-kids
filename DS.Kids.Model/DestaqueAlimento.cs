using System;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    public partial class DestaqueAlimento
    {
        public int IdDestaqueAlimento { get; set; }
        public int IdParceiro { get; set; }
        public int IdAlimento { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual Parceiro Parceiro { get; set; }
    }
}
