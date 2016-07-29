using System;
using System.Linq;

namespace DS.Kids.Apps.Core.Helpers
{

	public static class CrescimentoHelpers
	{
		#region Static Fields

		private static readonly TimeSpan _atualizacaoThreshold = new TimeSpan(14, 0, 0, 0); // 14 dias
		// APENAS TESTE, REMOVER
		//private static readonly TimeSpan _atualizacaoThreshold = new TimeSpan(0, 0, 0, 30); // 30 segundos

		#endregion

		#region Public Properties

		internal static DateTime AlertStartTime
		{
			get
			{
				var ultimoCrescimento = LoginHelper.CurrentCrianca.Crescimentos.LastOrDefault();
				if(ultimoCrescimento != null)
				{
					var alertStartTime = ultimoCrescimento.DataCriacao + _atualizacaoThreshold;
					// TODO: especificar horário
					//alertStartTime = alertStartTime.Date;
					return alertStartTime;
				}
				return DateTime.UtcNow;
			}
		}

		internal static string Body
		{
			get
			{
				var body = "Hoje é dia de atualizar o peso e altura d{SEXO} {NOMECRIANCA}!";
				body = FormatarMensagem(body, LoginHelper.CurrentCrianca.Nome, LoginHelper.CurrentUser.Nome, LoginHelper.CurrentCrianca.Sexo);
				return body;
			}
		}

		#endregion

		#region Public Methods and Operators

		public static string FormatarMensagem(string mensagem, string nomeCrianca, string nomeResponsavel, string tipoSexo)
		{
			var sexo = tipoSexo == "M" ? "o" : "a";
			var sexoEleEla = tipoSexo == "M" ? "ele" : "ela";
			mensagem = mensagem
				.Replace("{NOMECRIANCA}", nomeCrianca)
				.Replace("{NOMERESPONSAVEL}", nomeResponsavel)
				.Replace("{ELEELA}", sexoEleEla)
				.Replace("{SEXO}", sexo);
			return mensagem;
		}

		public static string GetCrescimentoNotificationId(int criancaId)
		{
			return "CrescimentoNotification" + criancaId;
		}

		public static bool PrecisaAtualizar()
		{
			if(LoginHelper.IsLoggedin() == false || LoginHelper.CurrentCrianca == null)
			{
				return false;
			}

			if(LoginHelper.CurrentCrianca.Crescimentos == null)
			{
				return true;
			}

			var ultimoCrescimento = LoginHelper.CurrentCrianca.Crescimentos.LastOrDefault();

			//return true;

			return ultimoCrescimento == null || (DateTime.UtcNow.Date - ultimoCrescimento.DataCriacao.Date) >= _atualizacaoThreshold;
			//return ultimoCrescimento == null || (DateTime.UtcNow - ultimoCrescimento.DataCriacao) >= _atualizacaoThreshold;
		}

		#endregion
	}

}
