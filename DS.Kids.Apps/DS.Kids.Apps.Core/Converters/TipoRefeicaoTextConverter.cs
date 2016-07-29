using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class TipoRefeicaoTextConverter : MvxValueConverter<TipoRefeicao, string>
	{
		#region Methods

		protected override string Convert(TipoRefeicao value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert(value);
		}

		public static string Convert(TipoRefeicao value)
		{
			return value.GetString();
		}

		#endregion
	}

}
