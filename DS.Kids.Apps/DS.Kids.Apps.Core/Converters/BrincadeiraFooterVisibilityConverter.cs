using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Core.Converters
{

	public class BrincadeiraFooterVisibilityConverter : MvxBaseVisibilityValueConverter<BrincadeiraItemType>
	{
		#region Methods

		protected override MvxVisibility Convert(BrincadeiraItemType value, object parameter, CultureInfo culture)
		{
			return value == BrincadeiraItemType.Instrucao ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
