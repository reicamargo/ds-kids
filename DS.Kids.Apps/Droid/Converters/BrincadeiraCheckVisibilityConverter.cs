using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Converters
{

	public class BrincadeiraCheckVisibilityConverter : MvxBaseVisibilityValueConverter
	{
		#region Methods

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			var itemType = (BrincadeiraItemType) value;
			return itemType == BrincadeiraItemType.Material ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
