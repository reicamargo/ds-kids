namespace DS.Kids.Model
{
    public class RefeicaoItem
    {
        public int IdRefeicaoItem { get; set; }
        public int IdRefeicao { get; set; }
        public int IdAlimento { get; set; }
        public int IdMedida { get; set; }
        public decimal Quantidade { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual Medida Medida { get; set; }
        public virtual Refeicao Refeicao { get; set; }

        public void RefreshMedida()
        {
            Medida.RefreshNome(Quantidade);
        }
    }
}
