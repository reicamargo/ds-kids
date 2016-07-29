using System.Globalization;
using System.Linq;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{
	public class DiarioRefeicaoHeaderVisibilityConverter: MvxBaseVisibilityValueConverter
	{
		#region Methods

		private static readonly int RecommendedHeader = 0;

		private static int OtherHeader
		{
			get 
			{
				return DiarioRefeicaoViewModel.StaticRefeicoesGrupo.Count(x => x.Sugerido);
			}
		}

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			var grupoAlimentarIndex = DiarioRefeicaoViewModel.StaticRefeicoesGrupo.IndexOf (value as RefeicaoGrupo);
			var isSpecialHeader = parameter == null && grupoAlimentarIndex == RecommendedHeader || grupoAlimentarIndex == OtherHeader;

			return isSpecialHeader ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}
}
