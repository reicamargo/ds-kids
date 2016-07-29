using System;
using System.Linq;
using System.Collections.Generic;

using DS.Kids.Model.Support;

namespace DS.Kids.Model
{
	public class StatusCarinha : BaseModel
	{
		public static readonly Dictionary<TipoGrupoRefeicao, int> Feliz2A3 =
			new Dictionary<TipoGrupoRefeicao, int>
				{
					{TipoGrupoRefeicao.CereaisTuberculosERaizes, 5},
					{TipoGrupoRefeicao.VerdurasELegumes, 3},
					{TipoGrupoRefeicao.Frutas, 3},
					{TipoGrupoRefeicao.LeitesIogurtesEQueijos, 3},
					{TipoGrupoRefeicao.CarnesEOvos, 2},
					{TipoGrupoRefeicao.FeijoesESimilares, 1},
					{TipoGrupoRefeicao.OleosEGorduras, 2},
					{TipoGrupoRefeicao.AcucaresEDoces, 1}
				};
		public static readonly Dictionary<TipoGrupoRefeicao, int> Feliz4A10 =
			new Dictionary<TipoGrupoRefeicao, int>
				{
					{TipoGrupoRefeicao.CereaisTuberculosERaizes, 5},
					{TipoGrupoRefeicao.VerdurasELegumes, 3},
					{TipoGrupoRefeicao.Frutas, 3},
					{TipoGrupoRefeicao.LeitesIogurtesEQueijos, 3},
					{TipoGrupoRefeicao.CarnesEOvos, 2},
					{TipoGrupoRefeicao.FeijoesESimilares, 1},
					{TipoGrupoRefeicao.OleosEGorduras, 1},
					{TipoGrupoRefeicao.AcucaresEDoces, 1}
				};

		public static readonly Dictionary<TipoGrupoRefeicao, int> FelizMax2A3 =
			new Dictionary<TipoGrupoRefeicao, int>
				{
					{TipoGrupoRefeicao.CereaisTuberculosERaizes, 6},
					{TipoGrupoRefeicao.VerdurasELegumes, 6},
					{TipoGrupoRefeicao.Frutas, 6},
					{TipoGrupoRefeicao.LeitesIogurtesEQueijos, 4},
					{TipoGrupoRefeicao.CarnesEOvos, 2},
					{TipoGrupoRefeicao.FeijoesESimilares, 2},
					{TipoGrupoRefeicao.OleosEGorduras, 2},
					{TipoGrupoRefeicao.AcucaresEDoces, 1}
				};
		public static readonly Dictionary<TipoGrupoRefeicao, int> FelizMax4A10 =
			new Dictionary<TipoGrupoRefeicao, int>
				{
					{TipoGrupoRefeicao.CereaisTuberculosERaizes, 6},
					{TipoGrupoRefeicao.VerdurasELegumes, 6},
					{TipoGrupoRefeicao.Frutas, 5},
					{TipoGrupoRefeicao.LeitesIogurtesEQueijos, 4},
					{TipoGrupoRefeicao.CarnesEOvos, 2},
					{TipoGrupoRefeicao.FeijoesESimilares, 2},
					{TipoGrupoRefeicao.OleosEGorduras, 1},
					{TipoGrupoRefeicao.AcucaresEDoces, 1}
				};

		private Carinha _carinha;

		private int _count;

		private int _max;

		private TipoGrupoRefeicao _tipoGrupoRefeicao;

		public TipoGrupoRefeicao TipoGrupoRefeicao
		{
			get
			{
				return _tipoGrupoRefeicao;
			}
			set
			{
				_tipoGrupoRefeicao = value;
				Notify();
			}
		}

		public Carinha Carinha
		{
			get
			{
				return _carinha;
			}
			set
			{
				_carinha = value;
				Notify();
			}
		}

		public int Count
		{
			get
			{
				return _count;
			}
			set
			{
				_count = value;
				Notify();
			}
		}

		public int Max
		{
			get
			{
				return _max;
			}
			set
			{
				_max = value;
				Notify();
			}
		}

		public static int GetCount(DateTime dataNascimento, IList<RefeicaoDiario> tiposRefeicoesDiario, TipoGrupoRefeicao tipoGrupoRefeicao)
		{
			int feliz;

			var countGrupo = tiposRefeicoesDiario.Sum(trd => trd.CountRefeicoes(tipoGrupoRefeicao));

			if (dataNascimento.GetIdade() <= 3)
			{
				feliz = Feliz2A3[tipoGrupoRefeicao];
			}
			else
			{
				feliz = Feliz4A10[tipoGrupoRefeicao];
			}

			return Math.Min(countGrupo, feliz);
		}

		public static int GetMax(DateTime dataNascimento, TipoGrupoRefeicao tipoGrupoRefeicao)
		{
			if (dataNascimento.GetIdade() <= 3)
			{
				return Feliz2A3[tipoGrupoRefeicao];
			}

			return Feliz4A10[tipoGrupoRefeicao];
		}

		public static Carinha GetCarinha(DateTime dataNascimento, IList<RefeicaoDiario> tiposRefeicoesDiario, TipoGrupoRefeicao tipoGrupoRefeicao, int idParceiro = 7)
		{
			int feliz;
			int felizMax;

			if (dataNascimento.GetIdade() <= 3)
			{
				feliz = Feliz2A3[tipoGrupoRefeicao];
				felizMax = FelizMax2A3[tipoGrupoRefeicao];
			}
			else
			{
				feliz = Feliz4A10[tipoGrupoRefeicao];
				felizMax = FelizMax4A10[tipoGrupoRefeicao];
			}

			var countGrupo = tiposRefeicoesDiario.Sum(trd => trd.CountRefeicoes(tipoGrupoRefeicao));

            // A AdeS pediu para que não mostrasse a carinha triste. Para qualquer outro parceiro, irá mostrar
            var idParceiroAdes = 8;

            if (countGrupo == 0 && idParceiro != idParceiroAdes)
            {
                return Carinha.Triste;
            }
            if (countGrupo > felizMax)
			{
				return Carinha.Cheio;
			}
			if (countGrupo >= feliz)
			{
				return Carinha.Feliz;
			}

			return Carinha.Medio;
		}

	}
}
