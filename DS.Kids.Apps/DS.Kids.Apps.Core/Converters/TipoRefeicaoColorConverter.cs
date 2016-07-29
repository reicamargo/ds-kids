using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model.Communication;
using Cirrious.CrossCore.UI;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class TipoRefeicaoColorConverter : MvxValueConverter<TipoRefeicao, object>
	{
		#region Methods

		private IMvxNativeColor _colorConverter;

		private IMvxNativeColor colorConverter 
		{ 
			get { return _colorConverter ?? (_colorConverter = Cirrious.CrossCore.Mvx.Resolve<IMvxNativeColor>()); } 
		}


		protected override object Convert(TipoRefeicao value, Type targetType, object parameter, CultureInfo culture)
		{
			MvxColor color = null;
			switch(value)
			{
			case TipoRefeicao.CafeDaManha:
				color = new MvxColor (255, 159, 27);
				break;
			case TipoRefeicao.LancheDaManha:
			case TipoRefeicao.LancheDaTarde:
				color = new MvxColor(237, 76, 90);
				break;
			case TipoRefeicao.Almoco:
				color = new MvxColor(88, 173, 109);
				break;
			case TipoRefeicao.Jantar:
				color = new MvxColor(4, 63, 132);
				break;
			case TipoRefeicao.LancheDaNoite:
				color = new MvxColor(91, 64, 132);
				break;
			}

			return colorConverter.ToNative(color);
		}

		#endregion
	}

}
