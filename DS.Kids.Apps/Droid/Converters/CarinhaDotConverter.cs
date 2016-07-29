using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{
	public class CarinhaDotConverter: MvxValueConverter<int, int>
	{
		#region Methods

		protected override int Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			return value < System.Convert.ToInt32 (parameter) ? 
				Resource.Drawable.carinha_bullet_off:
				Resource.Drawable.carinha_bullet_on;
		}

		#endregion
	}
}

