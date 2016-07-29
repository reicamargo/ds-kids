using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{

	public class StringFormatValueConverter : MvxValueConverter
	{
		#region Public Methods and Operators

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value == null)
			{
				return null;
			}

			if(parameter == null)
			{
				return value;
			}

			var format = "{0:" + parameter + "}";

			return string.Format(format, value);
		}

		#endregion
	}

}
