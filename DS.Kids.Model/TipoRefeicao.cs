namespace DS.Kids.Model
{
	/// <summary>
	/// Entidade que representa uma criança.
	/// </summary>
	public enum TipoRefeicao
	{
		/// <summary>
		/// Café da Manhã
		/// </summary>
		CafeDaManha = 1,
		/// <summary>
		/// Lanche da Manhã
		/// </summary>
		LancheDaManha = 2,
		/// <summary>
		/// Almoço
		/// </summary>
		Almoco = 3,
		/// <summary>
		/// Lanche da Tarde
		/// </summary>
		LancheDaTarde = 4,
		/// <summary>
		/// Jantar
		/// </summary>
		Jantar = 5,
		/// <summary>
		/// Lanche da Noite
		/// </summary>
		LancheDaNoite = 6
	}

	public static class TipoRefeicaoExtensions
	{
		public static string GetString(this TipoRefeicao self)
		{
			switch (self)
			{
				case TipoRefeicao.CafeDaManha:
					return "Café da manhã";
				case TipoRefeicao.LancheDaManha:
					return "Lanche/Lancheira da manhã";
				case TipoRefeicao.Almoco:
					return "Almoço";
				case TipoRefeicao.LancheDaTarde:
					return "Lanche/Lancheira da tarde";
				case TipoRefeicao.Jantar:
					return "Jantar";
				case TipoRefeicao.LancheDaNoite:
					return "Lanche da noite";
			}

			return "";
		}
	}
}