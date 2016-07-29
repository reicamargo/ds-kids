using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class CardapioFooterVisibilityConverter : MvxBaseVisibilityValueConverter<TipoRefeicao>
	{
		#region Methods

		protected override MvxVisibility Convert(TipoRefeicao value, object parameter, CultureInfo culture)
		{
			return (value == TipoRefeicao.LancheDaNoite) == (bool)parameter ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
