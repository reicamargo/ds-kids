using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

namespace DS.Kids.Apps.Droid.Converters
{
	public class CarinhaDotVisibilityConverter: MvxBaseVisibilityValueConverter
	{
		#region Methods

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			return System.Convert.ToInt32(parameter) > System.Convert.ToInt32(value) ? MvxVisibility.Collapsed : MvxVisibility.Visible;
		}

		#endregion
	}
}

