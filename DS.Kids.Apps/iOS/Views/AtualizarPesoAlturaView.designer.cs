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
	[Register ("AtualizarPesoAlturaView")]
	partial class AtualizarPesoAlturaView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAtualizar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnClose { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblInformativo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		TPKeyboardAvoiding.TPKeyboardAvoidingScrollView ScrollView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtAltura { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPeso { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAtualizar != null) {
				btnAtualizar.Dispose ();
				btnAtualizar = null;
			}
			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}
			if (lblInformativo != null) {
				lblInformativo.Dispose ();
				lblInformativo = null;
			}
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
			if (txtAltura != null) {
				txtAltura.Dispose ();
				txtAltura = null;
			}
			if (txtPeso != null) {
				txtPeso.Dispose ();
				txtPeso = null;
			}
		}
	}
}
