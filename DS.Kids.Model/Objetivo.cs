namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa o Objetido da Brincadeira
    /// </summary>
	public class Objetivo
	{
		/// <summary>
		/// Id do objetivo
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Id  da brincadeira
		/// </summary>
		public int IdBrincadeira { get; set; }
		/// <summary>
		/// Objeto brincadeira
		/// </summary>
		public virtual Brincadeira Brincadeira { get; set; }
		/// <summary>
		/// Descricao do objetivo
		/// </summary>
		public string Descricao { get; set; }

        /// <summary>
        /// Retorna a descrição do Objetivo
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return Descricao;
		}
	}
}
