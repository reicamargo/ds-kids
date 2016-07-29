using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

using Foundation;

namespace DS.Kids.Apps.iOS.Converters
{

	[Preserve(AllMembers = true)]
	public class CarinhaImageConverter : MvxValueConverter<Carinha, string>
	{
		#region Methods

		protected override string Convert(Carinha value, Type targetType, object parameter, CultureInfo culture)
		{
			switch(value)
			{
				case Carinha.Triste:
					return "res:smile1_triste";
				case Carinha.Medio:
					return "res:smile2_medio";
				case Carinha.Feliz:
					return "res:smile3_feliz";
				case Carinha.Cheio:
					return "res:smile4_cheio";
			}
			return "";
		}

		#endregion
	}

}
