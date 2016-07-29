using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

namespace DS.Kids.Apps.Droid.Converters
{

	public class LeftMenuVisibilityConverter : MvxBaseVisibilityValueConverter
	{
		#region Methods

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			return System.Convert.ToInt32(value) == System.Convert.ToInt32(parameter) ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
