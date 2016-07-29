using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Converters
{

	public class LeftMenuSVGArrowConverter : MvxValueConverter<LeftMenuViewModel.VisibleMenuIndex, string>
	{
		#region Methods

		protected override string Convert(LeftMenuViewModel.VisibleMenuIndex value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.Format("svg/bt_seta_{0}.svg", value == LeftMenuViewModel.VisibleMenuIndex.Menu ? "baixo" : "cima");
		}

		#endregion
	}

}
