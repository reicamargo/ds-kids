using System;
using System.Globalization;

using Android.Text;

using Cirrious.CrossCore.Converters;

namespace DS.Kids.Apps.Droid.Converters
{

	public class BrincadeiraIdadeConverter : MvxValueConverter<string, ISpanned>
	{
		#region Methods

		protected override ISpanned Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return Html.FromHtml("<b> Idade </b> \n" + value);
		}

		#endregion
	}

}
