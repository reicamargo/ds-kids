using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{
	public class DiarioRefeicaoHeaderTextConverter: MvxValueConverter<RefeicaoGrupo, string>
	{
		private static readonly int RecommendedHeader = 0;

	    #region Methods

		protected override string Convert(RefeicaoGrupo value, Type targetType, object parameter, CultureInfo culture)
		{
			var grupoAlimentarIndex = DiarioRefeicaoViewModel.StaticRefeicoesGrupo.IndexOf (value as RefeicaoGrupo);
			return grupoAlimentarIndex == RecommendedHeader ? "Grupos recomendados" : "Outros grupos";
		}

		#endregion
	}
}