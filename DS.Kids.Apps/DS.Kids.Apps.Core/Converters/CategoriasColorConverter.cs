using System;
using System.Globalization;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class CategoriasColorConverter : MvxValueConverter<Categoria, object>
	{
		#region Static Fields

		private static readonly MvxColor _destaqueColor = new MvxColor(245, 136, 22);

		private static readonly MvxColor _normalColor = new MvxColor(69, 102, 205);

		#endregion

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

		#region Public Methods and Operators

		public static MvxColor Convert(Categoria categoria)
		{
			return categoria.Destaque ? _destaqueColor : _normalColor;
		}

		#endregion

		#region Methods

		protected override object Convert(Categoria categoria, Type targetType, object parameter, CultureInfo culture)
		{
			return ColorConverter.ToNative(Convert(categoria));
		}

		#endregion
	}

}
