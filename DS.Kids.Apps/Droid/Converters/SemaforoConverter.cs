using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;
using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class SemaforoConverter : MvxValueConverter<Semaforo, int>
	{
		#region Methods

        protected override int Convert(Semaforo value, Type targetType, object parameter, CultureInfo culture)
		{
			switch (value)
			{
				case Semaforo.Verde:
					return Resource.Drawable.green_bullet;
                case Semaforo.Amarelo:
					return Resource.Drawable.yellow_bullet;			
				default:
					return Resource.Drawable.red_bullet;
			}
		}

		#endregion
	}

}




