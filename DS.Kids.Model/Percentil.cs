using System.Collections.Generic;
using System.Linq;

namespace DS.Kids.Model
{
	/// <summary>
	/// Entidade que representa o Percentil.
	/// O Percentil é um cálculo baseado no Sexo, Meses de Idade e Imc que indica se a criança está desnutrida, com o crescimento normal, Sobrepeso ou obesa.
	/// Esses valores são estáticos, e a tabela extraída da OMS.
	/// </summary>
	public class Percentil : Support.BaseModel
	{
		/// <summary>
		/// Entidade que representa um Percentil e o Tipo de Crescimento
		/// </summary>
		public class PercentilTipoCrescimento
		{
			/// <summary>
			/// Sexo da Criança
			/// </summary>
			public string Sexo { get; set; }
			/// <summary>
			/// Meses de Idade
			/// </summary>
			public int MesesDeIdade { get; set; }
			/// <summary>
			/// Imc do P0.1
			/// </summary>
			public decimal ImcP01 { get; set; }
			/// <summary>
			/// Imc do P3
			/// </summary>
			public decimal ImcP3  { get; set; }
			/// <summary>
			/// Imc do P50
			/// </summary>
			public decimal ImcP50 { get; set; }
			/// <summary>
			/// Imc do P97
			/// </summary>
			public decimal ImcP97  { get; set; }
			/// <summary>
			/// Imc do P99.9
			/// </summary>
			public decimal ImcP999 { get; set; }

			public PercentilTipoCrescimento() { }

			/// <summary>
			/// Inicialização dos Tipos de Crescimento por Percentil
			/// </summary>
			/// <param name="sexo">Sexo da Criança</param>
			/// <param name="mesesDeIdade">Meses de Idade</param>
			/// <param name="imcP01">Imc do P0.1</param>
			/// <param name="imcP3">Imc do P3</param>
			/// <param name="imcP50">Imc do P50</param>
			/// <param name="imcP97">Imc do P97</param>
			/// <param name="imcP999">Imc do P99.9</param>
			public PercentilTipoCrescimento(string sexo, int mesesDeIdade, decimal imcP01, decimal imcP3, decimal imcP50, decimal imcP97, decimal imcP999)
			{
				Sexo = sexo;
				MesesDeIdade = mesesDeIdade;
				ImcP01 = imcP01;
				ImcP3 = imcP3;
				ImcP50 = imcP50;
				ImcP97 = imcP97;
				ImcP999 = imcP999;
			}
		}

		public static readonly List<PercentilTipoCrescimento> Percentis = new List<PercentilTipoCrescimento>
			{
				new PercentilTipoCrescimento("M", 24, 12.8m, 13.9m, 16.0m, 18.7m, 20.8m),
				new PercentilTipoCrescimento("F", 24, 12.3m, 13.5m, 15.7m, 18.5m, 20.8m),
				new PercentilTipoCrescimento("M", 25, 12.8m, 13.9m, 16.0m, 18.6m, 20.7m),
				new PercentilTipoCrescimento("F", 25, 12.3m, 13.4m, 15.7m, 18.5m, 20.8m),
				new PercentilTipoCrescimento("M", 26, 12.7m, 13.8m, 15.9m, 18.6m, 20.6m),
				new PercentilTipoCrescimento("F", 26, 12.3m, 13.4m, 15.6m, 18.5m, 20.7m),
				new PercentilTipoCrescimento("M", 27, 12.7m, 13.8m, 15.9m, 18.5m, 20.6m),
				new PercentilTipoCrescimento("F", 27, 12.2m, 13.4m, 15.6m, 18.4m, 20.7m),
				new PercentilTipoCrescimento("M", 28, 12.6m, 13.8m, 15.9m, 18.5m, 20.5m),
				new PercentilTipoCrescimento("F", 28, 12.2m, 13.4m, 15.6m, 18.4m, 20.7m),
				new PercentilTipoCrescimento("M", 29, 12.6m, 13.7m, 15.8m, 18.4m, 20.5m),
				new PercentilTipoCrescimento("F", 29, 12.2m, 13.4m, 15.6m, 18.4m, 20.6m),
				new PercentilTipoCrescimento("M", 30, 12.5m, 13.7m, 15.8m, 18.4m, 20.4m),
				new PercentilTipoCrescimento("F", 30, 12.2m, 13.3m, 15.5m, 18.3m, 20.6m),
				new PercentilTipoCrescimento("M", 31, 12.5m, 13.7m, 15.8m, 18.4m, 20.3m),
				new PercentilTipoCrescimento("F", 31, 12.2m, 13.3m, 15.5m, 18.3m, 20.6m),
				new PercentilTipoCrescimento("M", 32, 12.5m, 13.6m, 15.7m, 18.3m, 20.3m),
				new PercentilTipoCrescimento("F", 32, 12.1m, 13.3m, 15.5m, 18.3m, 20.5m),
				new PercentilTipoCrescimento("M", 33, 12.4m, 13.6m, 15.7m, 18.3m, 20.2m),
				new PercentilTipoCrescimento("F", 33, 12.1m, 13.3m, 15.5m, 18.3m, 20.5m),
				new PercentilTipoCrescimento("M", 34, 12.4m, 13.5m, 15.7m, 18.2m, 20.2m),
				new PercentilTipoCrescimento("F", 34, 12.1m, 13.2m, 15.4m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 35, 12.4m, 13.5m, 15.6m, 18.2m, 20.2m),
				new PercentilTipoCrescimento("F", 35, 12.1m, 13.2m, 15.4m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 36, 12.3m, 13.5m, 15.6m, 18.2m, 20.1m),
				new PercentilTipoCrescimento("F", 36, 12.0m, 13.2m, 15.4m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 37, 12.3m, 13.5m, 15.6m, 18.1m, 20.1m),
				new PercentilTipoCrescimento("F", 37, 12.0m, 13.2m, 15.4m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 38, 12.3m, 13.4m, 15.5m, 18.1m, 20.1m),
				new PercentilTipoCrescimento("F", 38, 12.0m, 13.2m, 15.4m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 39, 12.2m, 13.4m, 15.5m, 18.1m, 20.0m),
				new PercentilTipoCrescimento("F", 39, 12.0m, 13.1m, 15.3m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 40, 12.2m, 13.4m, 15.5m, 18.1m, 20.0m),
				new PercentilTipoCrescimento("F", 40, 11.9m, 13.1m, 15.3m, 18.2m, 20.5m),
				new PercentilTipoCrescimento("M", 41, 12.2m, 13.3m, 15.5m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 41, 11.9m, 13.1m, 15.3m, 18.2m, 20.6m),
				new PercentilTipoCrescimento("M", 42, 12.1m, 13.3m, 15.4m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 42, 11.9m, 13.1m, 15.3m, 18.2m, 20.6m),
				new PercentilTipoCrescimento("M", 43, 12.1m, 13.3m, 15.4m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 43, 11.9m, 13.0m, 15.3m, 18.2m, 20.6m),
				new PercentilTipoCrescimento("M", 44, 12.1m, 13.3m, 15.4m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 44, 11.8m, 13.0m, 15.3m, 18.2m, 20.6m),
				new PercentilTipoCrescimento("M", 45, 12.1m, 13.2m, 15.4m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 45, 11.8m, 13.0m, 15.3m, 18.3m, 20.7m),
				new PercentilTipoCrescimento("M", 46, 12.1m, 13.2m, 15.4m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 46, 11.8m, 13.0m, 15.3m, 18.3m, 20.7m),
				new PercentilTipoCrescimento("M", 47, 12.0m, 13.2m, 15.3m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 47, 11.8m, 13.0m, 15.3m, 18.3m, 20.7m),
				new PercentilTipoCrescimento("M", 48, 12.0m, 13.2m, 15.3m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 48, 11.7m, 12.9m, 15.3m, 18.3m, 20.8m),
				new PercentilTipoCrescimento("M", 49, 12.0m, 13.2m, 15.3m, 18.0m, 20.0m),
				new PercentilTipoCrescimento("F", 49, 11.7m, 12.9m, 15.3m, 18.3m, 20.8m),
				new PercentilTipoCrescimento("M", 50, 12.0m, 13.2m, 15.3m, 18.0m, 20.1m),
				new PercentilTipoCrescimento("F", 50, 11.7m, 12.9m, 15.3m, 18.3m, 20.9m),
				new PercentilTipoCrescimento("M", 51, 12.0m, 13.1m, 15.3m, 18.0m, 20.1m),
				new PercentilTipoCrescimento("F", 51, 11.7m, 12.9m, 15.3m, 18.4m, 20.9m),
				new PercentilTipoCrescimento("M", 52, 12.0m, 13.1m, 15.3m, 18.0m, 20.1m),
				new PercentilTipoCrescimento("F", 52, 11.7m, 12.9m, 15.2m, 18.4m, 21.0m),
				new PercentilTipoCrescimento("M", 53, 11.9m, 13.1m, 15.3m, 18.0m, 20.1m),
				new PercentilTipoCrescimento("F", 53, 11.6m, 12.9m, 15.3m, 18.4m, 21.0m),
				new PercentilTipoCrescimento("M", 54, 11.9m, 13.1m, 15.3m, 18.0m, 20.2m),
				new PercentilTipoCrescimento("F", 54, 11.6m, 12.9m, 15.3m, 18.4m, 21.0m),
				new PercentilTipoCrescimento("M", 55, 11.9m, 13.1m, 15.2m, 18.0m, 20.2m),
				new PercentilTipoCrescimento("F", 55, 11.6m, 12.9m, 15.3m, 18.4m, 21.1m),
				new PercentilTipoCrescimento("M", 56, 11.9m, 13.1m, 15.2m, 18.0m, 20.3m),
				new PercentilTipoCrescimento("F", 56, 11.6m, 12.8m, 15.3m, 18.5m, 21.1m),
				new PercentilTipoCrescimento("M", 57, 11.9m, 13.0m, 15.2m, 18.0m, 20.3m),
				new PercentilTipoCrescimento("F", 57, 11.6m, 12.8m, 15.3m, 18.5m, 21.2m),
				new PercentilTipoCrescimento("M", 58, 11.9m, 13.0m, 15.2m, 18.0m, 20.3m),
				new PercentilTipoCrescimento("F", 58, 11.6m, 12.8m, 15.3m, 18.5m, 21.2m),
				new PercentilTipoCrescimento("M", 59, 11.9m, 13.0m, 15.2m, 18.1m, 20.4m),
				new PercentilTipoCrescimento("F", 59, 11.6m, 12.8m, 15.3m, 18.5m, 21.3m),
				new PercentilTipoCrescimento("M", 60, 11.9m, 13.0m, 15.2m, 18.1m, 20.5m),
				new PercentilTipoCrescimento("F", 60, 11.6m, 12.8m, 15.3m, 18.6m, 21.3m),
				new PercentilTipoCrescimento("M", 61, 12.0m, 13.1m, 15.3m, 18.1m, 20.4m),
				new PercentilTipoCrescimento("F", 61, 11.7m, 12.9m, 15.2m, 18.6m, 21.6m),
				new PercentilTipoCrescimento("M", 62, 12.0m, 13.1m, 15.3m, 18.1m, 20.4m),
				new PercentilTipoCrescimento("F", 62, 11.7m, 12.9m, 15.2m, 18.6m, 21.7m),
				new PercentilTipoCrescimento("M", 63, 12.0m, 13.1m, 15.3m, 18.1m, 20.4m),
				new PercentilTipoCrescimento("F", 63, 11.7m, 12.9m, 15.2m, 18.6m, 21.7m),
				new PercentilTipoCrescimento("M", 64, 12.0m, 13.1m, 15.3m, 18.1m, 20.5m),
				new PercentilTipoCrescimento("F", 64, 11.7m, 12.9m, 15.2m, 18.7m, 21.8m),
				new PercentilTipoCrescimento("M", 65, 12.0m, 13.1m, 15.3m, 18.1m, 20.5m),
				new PercentilTipoCrescimento("F", 65, 11.7m, 12.8m, 15.2m, 18.7m, 21.9m),
				new PercentilTipoCrescimento("M", 66, 12.0m, 13.1m, 15.3m, 18.1m, 20.6m),
				new PercentilTipoCrescimento("F", 66, 11.7m, 12.8m, 15.2m, 18.7m, 21.9m),
				new PercentilTipoCrescimento("M", 67, 12.0m, 13.1m, 15.3m, 18.2m, 20.6m),
				new PercentilTipoCrescimento("F", 67, 11.7m, 12.8m, 15.2m, 18.8m, 22.0m),
				new PercentilTipoCrescimento("M", 68, 12.0m, 13.1m, 15.3m, 18.2m, 20.7m),
				new PercentilTipoCrescimento("F", 68, 11.7m, 12.8m, 15.3m, 18.8m, 22.1m),
				new PercentilTipoCrescimento("M", 69, 12.0m, 13.1m, 15.3m, 18.2m, 20.7m),
				new PercentilTipoCrescimento("F", 69, 11.6m, 12.8m, 15.3m, 18.8m, 22.2m),
				new PercentilTipoCrescimento("M", 70, 12.1m, 13.1m, 15.3m, 18.2m, 20.8m),
				new PercentilTipoCrescimento("F", 70, 11.6m, 12.8m, 15.3m, 18.9m, 22.3m),
				new PercentilTipoCrescimento("M", 71, 12.1m, 13.2m, 15.3m, 18.3m, 20.8m),
				new PercentilTipoCrescimento("F", 71, 11.6m, 12.8m, 15.3m, 18.9m, 22.4m),
				new PercentilTipoCrescimento("M", 72, 12.1m, 13.2m, 15.3m, 18.3m, 20.9m),
				new PercentilTipoCrescimento("F", 72, 11.6m, 12.8m, 15.3m, 18.9m, 22.4m),
				new PercentilTipoCrescimento("M", 73, 12.1m, 13.2m, 15.3m, 18.3m, 21.0m),
				new PercentilTipoCrescimento("F", 73, 11.6m, 12.8m, 15.3m, 19.0m, 22.5m),
				new PercentilTipoCrescimento("M", 74, 12.1m, 13.2m, 15.3m, 18.4m, 21.0m),
				new PercentilTipoCrescimento("F", 74, 11.6m, 12.8m, 15.3m, 19.0m, 22.6m),
				new PercentilTipoCrescimento("M", 75, 12.1m, 13.2m, 15.3m, 18.4m, 21.1m),
				new PercentilTipoCrescimento("F", 75, 11.6m, 12.8m, 15.3m, 19.0m, 22.7m),
				new PercentilTipoCrescimento("M", 76, 12.1m, 13.2m, 15.4m, 18.4m, 21.2m),
				new PercentilTipoCrescimento("F", 76, 11.6m, 12.8m, 15.3m, 19.1m, 22.8m),
				new PercentilTipoCrescimento("M", 77, 12.1m, 13.2m, 15.4m, 18.5m, 21.3m),
				new PercentilTipoCrescimento("F", 77, 11.6m, 12.8m, 15.3m, 19.1m, 22.9m),
				new PercentilTipoCrescimento("M", 78, 12.1m, 13.2m, 15.4m, 18.5m, 21.3m),
				new PercentilTipoCrescimento("F", 78, 11.6m, 12.8m, 15.3m, 19.2m, 23.0m),
				new PercentilTipoCrescimento("M", 79, 12.1m, 13.2m, 15.4m, 18.5m, 21.4m),
				new PercentilTipoCrescimento("F", 79, 11.6m, 12.8m, 15.3m, 19.2m, 23.1m),
				new PercentilTipoCrescimento("M", 80, 12.1m, 13.2m, 15.4m, 18.6m, 21.5m),
				new PercentilTipoCrescimento("F", 80, 11.7m, 12.8m, 15.3m, 19.3m, 23.2m),
				new PercentilTipoCrescimento("M", 81, 12.1m, 13.2m, 15.4m, 18.6m, 21.6m),
				new PercentilTipoCrescimento("F", 81, 11.7m, 12.8m, 15.4m, 19.3m, 23.3m),
				new PercentilTipoCrescimento("M", 82, 12.2m, 13.2m, 15.4m, 18.7m, 21.7m),
				new PercentilTipoCrescimento("F", 82, 11.7m, 12.9m, 15.4m, 19.3m, 23.4m),
				new PercentilTipoCrescimento("M", 83, 12.2m, 13.3m, 15.5m, 18.7m, 21.8m),
				new PercentilTipoCrescimento("F", 83, 11.7m, 12.9m, 15.4m, 19.4m, 23.6m),
				new PercentilTipoCrescimento("M", 84, 12.2m, 13.3m, 15.5m, 18.8m, 21.9m),
				new PercentilTipoCrescimento("F", 84, 11.7m, 12.9m, 15.4m, 19.4m, 23.7m),
				new PercentilTipoCrescimento("M", 85, 12.2m, 13.3m, 15.5m, 18.8m, 21.9m),
				new PercentilTipoCrescimento("F", 85, 11.7m, 12.9m, 15.4m, 19.5m, 23.8m),
				new PercentilTipoCrescimento("M", 86, 12.2m, 13.3m, 15.5m, 18.8m, 22.0m),
				new PercentilTipoCrescimento("F", 86, 11.7m, 12.9m, 15.4m, 19.6m, 23.9m),
				new PercentilTipoCrescimento("M", 87, 12.2m, 13.3m, 15.5m, 18.9m, 22.1m),
				new PercentilTipoCrescimento("F", 87, 11.7m, 12.9m, 15.5m, 19.6m, 24.0m),
				new PercentilTipoCrescimento("M", 88, 12.2m, 13.3m, 15.6m, 18.9m, 22.2m),
				new PercentilTipoCrescimento("F", 88, 11.7m, 12.9m, 15.5m, 19.7m, 24.2m),
				new PercentilTipoCrescimento("M", 89, 12.2m, 13.3m, 15.6m, 19.0m, 22.3m),
				new PercentilTipoCrescimento("F", 89, 11.7m, 12.9m, 15.5m, 19.7m, 24.3m),
				new PercentilTipoCrescimento("M", 90, 12.2m, 13.3m, 15.6m, 19.0m, 22.5m),
				new PercentilTipoCrescimento("F", 90, 11.7m, 12.9m, 15.5m, 19.8m, 24.4m),
				new PercentilTipoCrescimento("M", 91, 12.3m, 13.4m, 15.6m, 19.1m, 22.6m),
				new PercentilTipoCrescimento("F", 91, 11.7m, 12.9m, 15.5m, 19.8m, 24.6m),
				new PercentilTipoCrescimento("M", 92, 12.3m, 13.4m, 15.6m, 19.2m, 22.7m),
				new PercentilTipoCrescimento("F", 92, 11.7m, 13.0m, 15.6m, 19.9m, 24.7m),
				new PercentilTipoCrescimento("M", 93, 12.3m, 13.4m, 15.7m, 19.2m, 22.8m),
				new PercentilTipoCrescimento("F", 93, 11.8m, 13.0m, 15.6m, 20.0m, 24.8m),
				new PercentilTipoCrescimento("M", 94, 12.3m, 13.4m, 15.7m, 19.3m, 22.9m),
				new PercentilTipoCrescimento("F", 94, 11.8m, 13.0m, 15.6m, 20.0m, 25.0m),
				new PercentilTipoCrescimento("M", 95, 12.3m, 13.4m, 15.7m, 19.3m, 23.0m),
				new PercentilTipoCrescimento("F", 95, 11.8m, 13.0m, 15.7m, 20.1m, 25.1m),
				new PercentilTipoCrescimento("M", 96, 12.3m, 13.4m, 15.7m, 19.4m, 23.1m),
				new PercentilTipoCrescimento("F", 96, 11.8m, 13.0m, 15.7m, 20.2m, 25.3m),
				new PercentilTipoCrescimento("M", 97, 12.3m, 13.4m, 15.8m, 19.4m, 23.2m),
				new PercentilTipoCrescimento("F", 97, 11.8m, 13.0m, 15.7m, 20.2m, 25.4m),
				new PercentilTipoCrescimento("M", 98, 12.3m, 13.5m, 15.8m, 19.5m, 23.4m),
				new PercentilTipoCrescimento("F", 98, 11.8m, 13.1m, 15.7m, 20.3m, 25.6m),
				new PercentilTipoCrescimento("M", 99, 12.4m, 13.5m, 15.8m, 19.5m, 23.5m),
				new PercentilTipoCrescimento("F", 99, 11.8m, 13.1m, 15.8m, 20.4m, 25.7m),
				new PercentilTipoCrescimento("M", 100, 12.4m, 13.5m, 15.8m, 19.6m, 23.6m),
				new PercentilTipoCrescimento("F", 100, 11.9m, 13.1m, 15.8m, 20.4m, 25.9m),
				new PercentilTipoCrescimento("M", 101, 12.4m, 13.5m, 15.9m, 19.7m, 23.8m),
				new PercentilTipoCrescimento("F", 101, 11.9m, 13.1m, 15.8m, 20.5m, 26.0m),
				new PercentilTipoCrescimento("M", 102, 12.4m, 13.5m, 15.9m, 19.7m, 23.9m),
				new PercentilTipoCrescimento("F", 102, 11.9m, 13.1m, 15.9m, 20.6m, 26.2m),
				new PercentilTipoCrescimento("M", 103, 12.4m, 13.5m, 15.9m, 19.8m, 24.0m),
				new PercentilTipoCrescimento("F", 103, 11.9m, 13.2m, 15.9m, 20.7m, 26.3m),
				new PercentilTipoCrescimento("M", 104, 12.4m, 13.5m, 15.9m, 19.9m, 24.2m),
				new PercentilTipoCrescimento("F", 104, 11.9m, 13.2m, 15.9m, 20.7m, 26.5m),
				new PercentilTipoCrescimento("M", 105, 12.4m, 13.6m, 16.0m, 19.9m, 24.3m),
				new PercentilTipoCrescimento("F", 105, 12.0m, 13.2m, 16.0m, 20.8m, 26.7m),
				new PercentilTipoCrescimento("M", 106, 12.5m, 13.6m, 16.0m, 20.0m, 24.4m),
				new PercentilTipoCrescimento("F", 106, 12.0m, 13.2m, 16.0m, 20.9m, 26.8m),
				new PercentilTipoCrescimento("M", 107, 12.5m, 13.6m, 16.0m, 20.0m, 24.6m),
				new PercentilTipoCrescimento("F", 107, 12.0m, 13.3m, 16.1m, 21.0m, 27.0m),
				new PercentilTipoCrescimento("M", 108, 12.5m, 13.6m, 16.0m, 20.1m, 24.7m),
				new PercentilTipoCrescimento("F", 108, 12.0m, 13.3m, 16.1m, 21.1m, 27.1m),
				new PercentilTipoCrescimento("M", 109, 12.5m, 13.6m, 16.1m, 20.2m, 24.9m),
				new PercentilTipoCrescimento("F", 109, 12.0m, 13.3m, 16.1m, 21.1m, 27.3m),
				new PercentilTipoCrescimento("M", 110, 12.5m, 13.7m, 16.1m, 20.2m, 25.0m),
				new PercentilTipoCrescimento("F", 110, 12.1m, 13.3m, 16.2m, 21.2m, 27.5m),
				new PercentilTipoCrescimento("M", 111, 12.5m, 13.7m, 16.1m, 20.3m, 25.2m),
				new PercentilTipoCrescimento("F", 111, 12.1m, 13.4m, 16.2m, 21.3m, 27.6m),
				new PercentilTipoCrescimento("M", 112, 12.6m, 13.7m, 16.2m, 20.4m, 25.3m),
				new PercentilTipoCrescimento("F", 112, 12.1m, 13.4m, 16.3m, 21.4m, 27.8m),
				new PercentilTipoCrescimento("M", 113, 12.6m, 13.7m, 16.2m, 20.5m, 25.5m),
				new PercentilTipoCrescimento("F", 113, 12.1m, 13.4m, 16.3m, 21.5m, 28.0m),
				new PercentilTipoCrescimento("M", 114, 12.6m, 13.7m, 16.2m, 20.5m, 25.7m),
				new PercentilTipoCrescimento("F", 114, 12.1m, 13.4m, 16.3m, 21.6m, 28.1m),
				new PercentilTipoCrescimento("M", 115, 12.6m, 13.7m, 16.3m, 20.6m, 25.8m),
				new PercentilTipoCrescimento("F", 115, 12.2m, 13.5m, 16.4m, 21.6m, 28.3m),
				new PercentilTipoCrescimento("M", 116, 12.6m, 13.8m, 16.3m, 20.7m, 26.0m),
				new PercentilTipoCrescimento("F", 116, 12.2m, 13.5m, 16.4m, 21.7m, 28.4m),
				new PercentilTipoCrescimento("M", 117, 12.6m, 13.8m, 16.3m, 20.8m, 26.1m),
				new PercentilTipoCrescimento("F", 117, 12.2m, 13.5m, 16.5m, 21.8m, 28.6m),
				new PercentilTipoCrescimento("M", 118, 12.7m, 13.8m, 16.4m, 20.8m, 26.3m),
				new PercentilTipoCrescimento("F", 118, 12.2m, 13.6m, 16.5m, 21.9m, 28.8m),
				new PercentilTipoCrescimento("M", 119, 12.7m, 13.8m, 16.4m, 20.9m, 26.5m),
				new PercentilTipoCrescimento("F", 119, 12.3m, 13.6m, 16.6m, 22.0m, 28.9m),
				new PercentilTipoCrescimento("M", 120, 12.7m, 13.9m, 16.4m, 21.0m, 26.6m),
				new PercentilTipoCrescimento("F", 120, 12.3m, 13.6m, 16.6m, 22.1m, 29.1m),
				new PercentilTipoCrescimento("M", 121, 12.7m, 13.9m, 16.5m, 21.1m, 26.8m),
				new PercentilTipoCrescimento("F", 121, 12.3m, 13.6m, 16.7m, 22.2m, 29.3m),
				new PercentilTipoCrescimento("M", 122, 12.7m, 13.9m, 16.5m, 21.1m, 27.0m),
				new PercentilTipoCrescimento("F", 122, 12.3m, 13.7m, 16.7m, 22.2m, 29.4m),
				new PercentilTipoCrescimento("M", 123, 12.8m, 13.9m, 16.6m, 21.2m, 27.2m),
				new PercentilTipoCrescimento("F", 123, 12.4m, 13.7m, 16.8m, 22.3m, 29.6m),
				new PercentilTipoCrescimento("M", 124, 12.8m, 14.0m, 16.6m, 21.3m, 27.3m),
				new PercentilTipoCrescimento("F", 124, 12.4m, 13.7m, 16.8m, 22.4m, 29.7m),
				new PercentilTipoCrescimento("M", 125, 12.8m, 14.0m, 16.6m, 21.4m, 27.5m),
				new PercentilTipoCrescimento("F", 125, 12.4m, 13.8m, 16.9m, 22.5m, 29.9m),
				new PercentilTipoCrescimento("M", 126, 12.8m, 14.0m, 16.7m, 21.5m, 27.7m),
				new PercentilTipoCrescimento("F", 126, 12.5m, 13.8m, 16.9m, 22.6m, 30.1m),
				new PercentilTipoCrescimento("M", 127, 12.8m, 14.0m, 16.7m, 21.6m, 27.9m),
				new PercentilTipoCrescimento("F", 127, 12.5m, 13.9m, 17.0m, 22.7m, 30.2m),
				new PercentilTipoCrescimento("M", 128, 12.9m, 14.1m, 16.8m, 21.6m, 28.0m),
				new PercentilTipoCrescimento("F", 128, 12.5m, 13.9m, 17.0m, 22.8m, 30.4m),
				new PercentilTipoCrescimento("M", 129, 12.9m, 14.1m, 16.8m, 21.7m, 28.2m),
				new PercentilTipoCrescimento("F", 129, 12.5m, 13.9m, 17.1m, 22.9m, 30.5m),
				new PercentilTipoCrescimento("M", 130, 12.9m, 14.1m, 16.8m, 21.8m, 28.4m),
				new PercentilTipoCrescimento("F", 130, 12.6m, 14.0m, 17.1m, 23.0m, 30.7m),
				new PercentilTipoCrescimento("M", 131, 12.9m, 14.2m, 16.9m, 21.9m, 28.6m),
				new PercentilTipoCrescimento("F", 131, 12.6m, 14.0m, 17.2m, 23.1m, 30.8m)
			};

		/// <summary>
		/// Obtém o tipo de crescimento
		/// </summary>
		/// <param name="sexo">Sexo da Criança</param>
		/// <param name="mesesDeIdade">Meses de Idade da Criança</param>
		/// <param name="imc">Imc da Criança</param>
		/// <returns></returns>
		public static TipoCrescimento ObterTipoCrescimento(string sexo, int mesesDeIdade, decimal imc)
		{
			if(imc <= 12.8m && sexo == "M") return TipoCrescimento.Desnutricao;
			if(imc <= 12.3m && sexo == "F") return TipoCrescimento.Desnutricao;
			if(imc >= 30.8m && sexo == "M") return TipoCrescimento.Obesidade;
			if(imc >= 32.8m && sexo == "F") return TipoCrescimento.Obesidade;

			var percentil = Percentis.FirstOrDefault(p => p.Sexo == sexo && p.MesesDeIdade == mesesDeIdade);
			if(percentil == null)
				return TipoCrescimento.ValorNaoEncontrado;

			if(imc > percentil.ImcP999)
			{
				return TipoCrescimento.Obesidade;
			}
			if(imc > percentil.ImcP97)
			{
				return TipoCrescimento.Sobrepeso;
			}
			if(imc >= percentil.ImcP3)
			{
				return TipoCrescimento.Normal;
			}

			return TipoCrescimento.Desnutricao;
		}
	}
}