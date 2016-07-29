namespace DS.Kids.Model.Communication
{
	public static class Endpoints
	{
		// ReSharper disable InconsistentNaming
#if DEBUG // Não existe variável de precompilação RELEASE, apenas DEBUG. Não commitar mudanças nesta linha.
		public const string BASE = "http://dskidsapidev.azurewebsites.net/v1/";
#else
		public const string BASE = "http://api.dskids.com.br/v1/";
#endif

		public const string LOGIN = BASE + "Login";
		public const string LOGIN_SOCIAL = BASE + "LoginSocial";
		public const string LOGOFF = BASE + "Logoff/{0}";
		public const string RESPONSAVEIS = BASE + "Responsaveis";
		public const string CRIANCAS = BASE + "Criancas/{0}";
		public const string CRESCIMENTOS = BASE + "Crescimentos";
		public const string DIARIO = BASE + "Diario?criancaId={0}&data={1}";
		public const string UPDATE_DIARIO = BASE + "Diario";
		public const string DELETE_REFEICAO_DIARIO = BASE + "Diario?idRefeicaoGrupo={0}&idAlimento={1}";
		public const string ESQUECI_MINHA_SENHA = BASE + "EsqueciMinhaSenha";
		public const string TROCA_DE_SENHA = BASE + "TrocaDeSenha";
		public const string CARDAPIOS = BASE + "Cardapios?mesesIdade={0}";
		public const string CARDAPIOS_POR_TIPO_REFEICAO = CARDAPIOS + "&tipoRefeicao={1}";
		public const string BRINCADEIRA = BASE + "Brincadeiras/{0}";
		public const string BRINCADEIRAS = BASE + "Brincadeiras?pageSize={0}&pageNumber={1}";
		public const string CATEGORIA = BASE + "Categorias/{0}";
		public const string CATEGORIAS = BASE + "Categorias";
		public const string DICAS = BASE + "Categorias/{0}/dicas";
		public const string DICA = BASE + "Dicas/{0}";
		public const string PARCEIROS = BASE + "Parceiros";
		public const string PARCEIROS_POR_TIPO = PARCEIROS + "/{0}";
		public const string OPTIN = BASE + "Optin";
		public const string ALIMENTOS = BASE + "Alimentos?mesesDeIdade={0}&idGrupo={1}";
	}
}
