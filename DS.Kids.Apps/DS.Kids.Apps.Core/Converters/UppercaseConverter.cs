using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Core.Converters
{

	public class UppercaseConverter : MvxValueConverter<string, string>
	{
		#region Methods

		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToUpper();
		}

		#endregion
	}

}
