using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model.Communication;

namespace DS.Kids.Apps.Core.Converters
{

	public class RelativeToAbsoluteUrlConverter : MvxValueConverter<string, string>
	{
		#region Methods

		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			if(string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return Endpoints.BASE + value;
		}

		#endregion
	}

}
