using System;
using System.Globalization;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

namespace DS.Kids.Apps.Core.Converters
{

	public class BackgroundColorIndexConverter : MvxValueConverter<int, object>
	{
		#region Fields

		private IMvxNativeColor _colorConverter;

		#endregion

		#region Properties

		private IMvxNativeColor ColorConverter
		{
			get
			{
				return _colorConverter ?? (_colorConverter = Mvx.Resolve<IMvxNativeColor>());
			}
		}

		#endregion

		#region Methods

		protected override object Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			//Transparencia para todos menos o indice selecionado
			var color = new MvxColor(242, 242, 242, value == System.Convert.ToInt32(parameter) ? 255 : 0);
			return ColorConverter.ToNative(color);
		}

		#endregion
	}

}
