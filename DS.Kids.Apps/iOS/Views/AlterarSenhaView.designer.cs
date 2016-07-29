// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DS.Kids.Apps.iOS.Views
{
	[Register ("AlterarSenhaView")]
	partial class AlterarSenhaView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		TPKeyboardAvoiding.TPKeyboardAvoidingScrollView ScrollView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtCurrentPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNewPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNewPasswordConfirmation { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
			if (txtCurrentPassword != null) {
				txtCurrentPassword.Dispose ();
				txtCurrentPassword = null;
			}
			if (txtNewPassword != null) {
				txtNewPassword.Dispose ();
				txtNewPassword = null;
			}
			if (txtNewPasswordConfirmation != null) {
				txtNewPasswordConfirmation.Dispose ();
				txtNewPasswordConfirmation = null;
			}
		}
	}
}
