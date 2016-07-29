using System;
using System.Globalization;

using Android.Text;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class CrescimentoFormattedTextConverter : MvxValueConverter<Crescimento, ISpanned>
	{
		#region Methods

		protected override ISpanned Convert(Crescimento value, Type targetType, object parameter, CultureInfo culture)
		{
			return Html.FromHtml(String.Format("{0:F2}kg / {1:F2}m", value.Peso, value.Altura));
		}

		#endregion
	}

}

