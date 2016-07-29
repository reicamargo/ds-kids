using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Playground.Importacoes
{
	public class Percentis
	{
		private static List<string> LerArquivo()
		{
			var caminho = "../../../Support/Percentil.csv";
			return System.IO.File.ReadAllLines(caminho).ToList();
		}

		private static void FazerParseParaPercentis(List<string> linhas)
		{
			linhas.RemoveAt(0);

			var percentis = new List<DS.Kids.Model.Percentil.PercentilTipoCrescimento>();
			percentis.AddRange(Extrair(linhas));

			EscreveAgrupamento(percentis);
		}

		private static void EscreveAgrupamento(List<DS.Kids.Model.Percentil.PercentilTipoCrescimento> percentis)
		{
			var template = "new PercentilTipoCrescimento(\"{0}\", {1}, {2}m, {3}m, {4}m, {5}m, {6}m),";

			Func<DS.Kids.Model.Percentil.PercentilTipoCrescimento, string> escreve = item =>
			{
				var x = string.Format(template, item.Sexo, item.MesesDeIdade, item.ImcP01.ToString(CultureInfo.InvariantCulture), item.ImcP3.ToString(CultureInfo.InvariantCulture), item.ImcP50.ToString(CultureInfo.InvariantCulture), item.ImcP97.ToString(CultureInfo.InvariantCulture), item.ImcP999.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine(x);
				return x;
			};

			foreach (var percentil in percentis)
			{
				escreve(percentil);
			}
		}

		private static List<DS.Kids.Model.Percentil.PercentilTipoCrescimento> Extrair(List<string> linhas)
		{
			var percentis = new List<DS.Kids.Model.Percentil.PercentilTipoCrescimento>();
			foreach (var linha in linhas)
			{
				var colunas = linha.Split(',');
				var percentil = new DS.Kids.Model.Percentil.PercentilTipoCrescimento
									{
										MesesDeIdade = int.Parse(colunas[0]),
										Sexo = colunas[2],
										ImcP01 = decimal.Parse(colunas[3], CultureInfo.InvariantCulture),
										ImcP3 = decimal.Parse(colunas[5], CultureInfo.InvariantCulture),
										ImcP50 = decimal.Parse(colunas[13], CultureInfo.InvariantCulture),
										ImcP97 = decimal.Parse(colunas[17], CultureInfo.InvariantCulture),
										ImcP999 = decimal.Parse(colunas[19], CultureInfo.InvariantCulture)
									};

				// Não adicionar percentis com 11 anos ou mais
				if(percentil.MesesDeIdade < 11 * 12)
				{
					percentis.Add(percentil);
				}
			}

			return percentis;
		}

		public void Executar()
		{
			try
			{
				var linhas = LerArquivo();
				FazerParseParaPercentis(linhas);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
