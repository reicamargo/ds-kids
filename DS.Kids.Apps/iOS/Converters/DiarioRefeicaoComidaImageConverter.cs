using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using Foundation;

namespace DS.Kids.Apps.iOS.Converters
{

	[Preserve(AllMembers = true)]
	public class DiarioRefeicaoComidaImageConverter : MvxValueConverter<bool, string>
	{
		#region Methods

		protected override string Convert(bool tipoRefeicaoRealizada, Type targetType, object parameter, CultureInfo culture)
		{
		    return tipoRefeicaoRealizada ? "res:ico_check_ok" : "res:ico_check_miss";
		}

	    #endregion
	}

}
