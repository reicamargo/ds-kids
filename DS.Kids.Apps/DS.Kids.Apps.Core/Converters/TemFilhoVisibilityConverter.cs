using System.Globalization;

using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Visibility;

using DS.Kids.Apps.Core.Helpers;

namespace DS.Kids.Apps.Core.Converters
{

	public class TemFilhoVisibilityConverter : MvxBaseVisibilityValueConverter
	{
		#region Methods

		protected override MvxVisibility Convert(object value, object parameter, CultureInfo culture)
		{
			return (LoginHelper.IsLoggedin() && LoginHelper.CurrentCrianca != null) ? MvxVisibility.Visible : MvxVisibility.Collapsed;
		}

		#endregion
	}

}
