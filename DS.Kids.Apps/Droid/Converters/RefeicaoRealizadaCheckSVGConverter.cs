using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{
	public class RefeicaoRealizadaCheckSVGConverter : MvxValueConverter<bool, string>
	{
		#region Methods

		protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			//return string.Format("svg/ico_{0}_{1}.svg", System.Convert.ToInt32(parameter) == 0 ? "d" : "check", value ? "ok" : "miss");

            return string.Format("svg/ico_check_{0}.svg", value ? "ok" : "miss");
        }

		#endregion
	}

}
