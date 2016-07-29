using System;
using System.Globalization;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Core.Converters
{

	public class BrincadeiraItemColorConverter : MvxValueConverter<BrincadeiraItemType, object>
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

		protected override object Convert(BrincadeiraItemType value, Type targetType, object parameter, CultureInfo culture)
		{
			MvxColor color = null;

			switch(value)
			{
				case BrincadeiraItemType.Material:
					color = new MvxColor(255, 159, 28);
					break;
				case BrincadeiraItemType.Objetivo:
					color = new MvxColor(70, 102, 205);
					break;
				case BrincadeiraItemType.Instrucao:
					color = new MvxColor(74, 159, 92);
					break;
			}

			return ColorConverter.ToNative(color);
		}

		#endregion
	}

}
