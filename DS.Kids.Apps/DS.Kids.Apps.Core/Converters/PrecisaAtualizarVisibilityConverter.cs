using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.Helpers;

namespace DS.Kids.Apps.Core.Converters
{

	public class PrecisaAtualizarVisibilityConverter : MvxBaseVisibilityValueConverter
	{
		#region Methods

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			return CrescimentoHelpers.PrecisaAtualizar() ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
