using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class TipoAmbienteConverter : MvxValueConverter<TipoAmbiente, string>
	{
		#region Methods

		protected override string Convert(TipoAmbiente value, Type targetType, object parameter, CultureInfo culture)
		{
			switch(value)
			{
				case TipoAmbiente.Interno:
					return "Interno";
				case TipoAmbiente.Externo:
					return "Externo";
				default:	
					return "Interno ou Externo";
			}
		}

		#endregion
	}

}
