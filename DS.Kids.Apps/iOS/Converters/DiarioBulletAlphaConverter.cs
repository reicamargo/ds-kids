using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using Foundation;

namespace DS.Kids.Apps.iOS.Converters
{

    [Preserve(AllMembers = true)]
    public class DiarioBulletAlphaConverter : MvxValueConverter<int, nfloat>
    {
        #region Methods

        protected override nfloat Convert(int count, Type targetType, object parameter, CultureInfo culture)
        {
            var bulletIndex = (int)parameter;

            return bulletIndex < count ? 1 : 0.5f;
        }

        #endregion
    }

}
