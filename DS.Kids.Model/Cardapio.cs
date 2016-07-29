namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um Cardápio
    /// </summary>
    public class Cardapio
    {
        /// <summary>
        /// Refeição Café da Manhã
        /// </summary>
        public Refeicao CafeDaManha { get; set; }
        /// <summary>
        /// Refeição Lanche da Manhã
        /// </summary>
        public Refeicao LancheDaManha { get; set; }
        /// <summary>
        /// Refeição Almoço
        /// </summary>
        public Refeicao Almoco { get; set; }
        /// <summary>
        /// Refeição Lanche da Tarde
        /// </summary>
        public Refeicao LancheDaTarde { get; set; }
        /// <summary>
        /// Refeição Jantar
        /// </summary>
        public Refeicao Jantar { get; set; }
        /// <summary>
        /// Refeição Lanche da Noite
        /// </summary>
        public Refeicao LancheNoite { get; set; }

        public Cardapio()
        {
            CafeDaManha = new Refeicao();
            LancheDaManha = new Refeicao();
            Almoco = new Refeicao();
            LancheDaTarde = new Refeicao();
            Jantar = new Refeicao();
            LancheNoite = new Refeicao();
        }
    }
}