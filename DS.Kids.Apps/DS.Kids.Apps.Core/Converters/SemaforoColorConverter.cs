using System;
using System.Globalization;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

    public class SemaforoColorConverter : MvxValueConverter<Semaforo, object>
	{
		#region Static Fields

		private static readonly MvxColor _colorGreen = new MvxColor(57, 201, 52);

		private static readonly MvxColor _colorRed = new MvxColor(248, 82, 82);

		private static readonly MvxColor _colorYellow = new MvxColor(244, 209, 3);

		#endregion

		#region Fields

		private static IMvxNativeColor _colorConverter;

		#endregion

		#region Properties

		private static IMvxNativeColor ColorConverter
		{
			get
			{
				return _colorConverter ?? (_colorConverter = Mvx.Resolve<IMvxNativeColor>());
			}
		}

		#endregion

		#region Methods

        protected override object Convert(Semaforo value, Type targetType, object parameter, CultureInfo culture)
		{
			return ColorConverter.ToNative(Convert(value));
		}

		public static MvxColor Convert(Semaforo? value)
		{
            switch (value)
            {
                case Semaforo.Verde:
                    return _colorGreen;
                case Semaforo.Vermelho:
                    return _colorRed;
                //case Semaforo.Amarelo:
                default:
                    return _colorYellow;
            }
		}

		#endregion
	}

}
