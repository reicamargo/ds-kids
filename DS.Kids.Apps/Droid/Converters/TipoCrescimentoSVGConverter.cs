using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class TipoCrescimentoSVGConverter : MvxValueConverter<TipoCrescimento, string>
	{
		#region Methods

		protected override string Convert(TipoCrescimento value, Type targetType, object parameter, CultureInfo culture)
		{
			switch(value)
			{
				case TipoCrescimento.Normal:
					return "svg/rosto_feliz.svg";
				default:
					return "svg/rosto_triste.svg";
			}
		}

		#endregion
	}

}
