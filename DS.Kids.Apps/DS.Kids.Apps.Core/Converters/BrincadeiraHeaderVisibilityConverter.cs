using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Core.Converters
{

	public class BrincadeiraHeaderVisibilityConverter : MvxBaseVisibilityValueConverter<BrincadeiraItemType>
	{
		#region Methods

		protected override MvxVisibility Convert(BrincadeiraItemType value, object parameter, CultureInfo culture)
		{
			return value == BrincadeiraItemType.Material ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
