using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{

	public class LeftMenuSVGConverter : MvxValueConverter<int, string>
	{
		#region Methods

		protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			var menuId = System.Convert.ToInt32(parameter);
			return string.Format("svg/menu{0:00}{1}.svg", menuId, value == menuId ? "on" : "");
		}

		#endregion
	}

}

