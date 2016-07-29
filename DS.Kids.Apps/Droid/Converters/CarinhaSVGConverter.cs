using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{
	public class CarinhaSVGConverter: MvxValueConverter<Carinha, string>
	{
		#region Methods

		protected override string Convert(Carinha value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.Format("svg/smile{0}.svg", (int) value);
		}

		#endregion
	}
}

