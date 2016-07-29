
using System;
using System.Globalization;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class TipoCrescimentoColorConverter : MvxValueConverter<TipoCrescimento, object>
	{
		#region Fields

		private IMvxNativeColor _colorConverter;
		private static readonly MvxColor _textRed = new MvxColor(253, 75, 65);
		private static readonly MvxColor _textGreen = new MvxColor(40, 188, 6);
		private static readonly MvxColor _backgroundRed = new MvxColor(254, 76, 66);
		private static readonly MvxColor _backgroundGreen = new MvxColor(107, 211, 0);
						
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

		protected override object Convert(TipoCrescimento value, Type targetType, object parameter, CultureInfo culture)
		{
			var isBackground = System.Convert.ToInt32(parameter) == (int)TipoCor.Fundo;
			
			switch(value)
			{
				case TipoCrescimento.Normal:
					return ColorConverter.ToNative(isBackground ? _backgroundGreen : _textGreen);
				default:
					return ColorConverter.ToNative(isBackground ? _backgroundRed : _textRed);
			}
		}

		private enum TipoCor
		{
		    // ReSharper disable once UnusedMember.Local
			Texto,
			Fundo
		}

		#endregion
	}
}