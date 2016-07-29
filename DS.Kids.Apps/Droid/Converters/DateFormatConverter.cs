using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{
	public class DateFormatConverter: MvxValueConverter<DateTime, string>
	{
		#region Public Methods and Operators

		protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString ("d", CultureInfo.CurrentUICulture);
		}

		protected override DateTime ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrEmpty(value) ? DateTime.Now : DateTime.Parse(value);
		}

		#endregion
	}
}