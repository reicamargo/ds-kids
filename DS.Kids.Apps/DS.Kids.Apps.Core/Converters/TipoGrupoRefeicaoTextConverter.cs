using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class TipoGrupoRefeicaoTextConverter : MvxValueConverter<TipoGrupoRefeicao, string>
	{
		#region Methods

		protected override string Convert(TipoGrupoRefeicao value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert(value);
		}

		public static string Convert(TipoGrupoRefeicao value)
		{
			switch (value)
			{
				case TipoGrupoRefeicao.CereaisTuberculosERaizes:
					return "Cereais, tubérculos e raízes";
				case TipoGrupoRefeicao.FeijoesESimilares:
					return "Feijões e similares";
				case TipoGrupoRefeicao.Frutas:
					return "Frutas";
				case TipoGrupoRefeicao.VerdurasELegumes:
					return "Verduras e legumes";
				case TipoGrupoRefeicao.CarnesEOvos:
					return "Carnes e ovos";
				case TipoGrupoRefeicao.LeitesIogurtesEQueijos:
					return "Leites, iogurtes e queijos";
				case TipoGrupoRefeicao.OleosEGorduras:
					return "Óleos e gorduras";
				case TipoGrupoRefeicao.AcucaresEDoces:
					return "Açúcares e doces";
                case TipoGrupoRefeicao.Bebidas:
                    return "Bebidas";
            }
			return "";
		}

		#endregion
	}

}
