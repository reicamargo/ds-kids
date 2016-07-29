using System;

using CoreGraphics;

using DS.Kids.Apps.iOS.Controls;

using Foundation;

using TPKeyboardAvoiding;

using UIKit;

namespace DS.Kids.Apps.iOS
{

	public static class ExtensionMethods
	{
		#region Public Methods and Operators

		public static void AddLeftPadding(this UITextField textField, int width = 5, int height = 5)
		{
			textField.LeftView = new UIView(new CGRect(0, 0, width, height));
			textField.LeftViewMode = UITextFieldViewMode.Always;
			if(string.IsNullOrEmpty(textField.Placeholder) == false)
			{
				textField.AttributedPlaceholder = new NSAttributedString(textField.Placeholder, foregroundColor: UIColor.White);
			}
		}

		public static void AddLeftPadding(params UITextField[] textFields)
		{
			AddLeftPadding(5, 5, textFields);
		}

		public static void AddLeftPadding(int width = 5, int height = 5, params UITextField[] textFields)
		{
			foreach(var textField in textFields)
			{
				textField.AddLeftPadding(width, height);
			}
		}

		public static AlturaPickerViewModel AddAlturaPicker(this UITextField txtAltura)
		{
			var pickerAltura = new UIPickerView();
			var pickerAlturaViewModel = new AlturaPickerViewModel(pickerAltura);
			pickerAltura.Model = pickerAlturaViewModel;
			pickerAltura.ShowSelectionIndicator = true;
			txtAltura.InputView = pickerAltura;
			txtAltura.AddToolbar();

			return pickerAlturaViewModel;
		}

		public static PesoPickerViewModel AddPesoPicker(this UITextField txtPeso)
		{
			var pickerPeso = new UIPickerView();
			var pickerPesoViewModel = new PesoPickerViewModel(pickerPeso);
			pickerPeso.Model = pickerPesoViewModel;
			pickerPeso.ShowSelectionIndicator = true;
			txtPeso.InputView = pickerPeso;
			txtPeso.AddToolbar();

			return pickerPesoViewModel;
		}

		public static UIDatePicker AddDateTimePicker(this UITextField txtDataNascimento)
		{
			var datePicker = new UIDatePicker
			{
				Mode = UIDatePickerMode.Date,
				TimeZone = NSTimeZone.LocalTimeZone
			};
			txtDataNascimento.InputView = datePicker;
			txtDataNascimento.AddToolbar();

			return datePicker;
		}

		private static void AddToolbar(this UITextField textField)
		{
			var inputAccessoryView = new UIToolbar
										{
											BarStyle = UIBarStyle.BlackOpaque
										};
			inputAccessoryView.SizeToFit();

			var cancelItem = new UIBarButtonItem("Cancelar", UIBarButtonItemStyle.Bordered, (sender, e) =>
				{
					textField.ResignFirstResponder();
				});
			var attributes = new UITextAttributes
			{
				TextColor = UIColor.White
			};
			cancelItem.SetTitleTextAttributes(attributes, UIControlState.Normal);

			var flexItem = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			var doneText = textField.ReturnKeyType == UIReturnKeyType.Done ? "Pronto" : "Seguinte";
			var doneItem = new UIBarButtonItem(doneText, UIBarButtonItemStyle.Done, (sender, e) =>
				{
					TPKeyboardAvoidingScrollView tpKeyboardAvoidingScrollView;
					UIView field = textField;
					while((tpKeyboardAvoidingScrollView = field.Superview as TPKeyboardAvoidingScrollView) == null)
					{
						field = field.Superview;
					    if(field == null)
					    {
					        break;
					    }
					}

					if(tpKeyboardAvoidingScrollView != null)
					{
						if(!tpKeyboardAvoidingScrollView.FocusNextTextField)
						{
							textField.ResignFirstResponder();
						}
					}
				});
			doneItem.SetTitleTextAttributes(attributes, UIControlState.Normal);

			inputAccessoryView.SetItems(new[]
											{
												cancelItem, flexItem, doneItem
											}, true);

			textField.InputAccessoryView = inputAccessoryView;
		}

		public static void AddLine(this UIView view, nfloat y)
		{
			var lineView = new UIView(new CGRect(0, y, 320, 0.5f))
			{
				BackgroundColor = new UIColor(0.8f, 0.8f, 0.8f, 1.0f)
			};
			view.AddSubview(lineView);
		}

		#endregion
	}

}
