using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class TipoRefeicaoHeaderImageConverter : MvxValueConverter<TipoRefeicao, string>
	{
		#region Methods

		protected override string Convert(TipoRefeicao value, Type targetType, object parameter, CultureInfo culture)
		{
			return "reftit_0" + (int)value;
		}

		#endregion
	}

}
