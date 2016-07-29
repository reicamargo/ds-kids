using System;

using Cirrious.MvvmCross.Binding.Touch.Views;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public class AlturaPickerViewModel : MvxPickerViewModel
	{

		public AlturaPickerViewModel(UIPickerView pickerView)
			: base(pickerView)
		{
			ReloadOnAllItemsSourceSets = true;
		}

		protected override string RowTitle(nint row, object item)
		{
			var decimalItem = (decimal)item;
			return decimalItem.ToString("0.00 m");
		}

	}

}
