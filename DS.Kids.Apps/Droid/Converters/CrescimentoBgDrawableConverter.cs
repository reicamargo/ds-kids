using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class CrescimentoBgDrawableConverter : MvxValueConverter<TipoCrescimento, int>
	{
		#region Methods

		protected override int Convert(TipoCrescimento value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == TipoCrescimento.Normal ? Resource.Drawable.green_rounded_corners : Resource.Drawable.red_rounded_corners;
		}

		#endregion
	}

}
