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
	[Register ("AdicionarFilhoView")]
	partial class AdicionarFilhoView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCamera { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnClose { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnConcluir { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSexoFem { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSexoMasc { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgFilho { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		TPKeyboardAvoiding.TPKeyboardAvoidingScrollView ScrollView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView SexoBox { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtAltura { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDataNascimento { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNome { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPeso { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}
			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}
			if (btnConcluir != null) {
				btnConcluir.Dispose ();
				btnConcluir = null;
			}
			if (btnSexoFem != null) {
				btnSexoFem.Dispose ();
				btnSexoFem = null;
			}
			if (btnSexoMasc != null) {
				btnSexoMasc.Dispose ();
				btnSexoMasc = null;
			}
			if (imgFilho != null) {
				imgFilho.Dispose ();
				imgFilho = null;
			}
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
			if (SexoBox != null) {
				SexoBox.Dispose ();
				SexoBox = null;
			}
			if (txtAltura != null) {
				txtAltura.Dispose ();
				txtAltura = null;
			}
			if (txtDataNascimento != null) {
				txtDataNascimento.Dispose ();
				txtDataNascimento = null;
			}
			if (txtNome != null) {
				txtNome.Dispose ();
				txtNome = null;
			}
			if (txtPeso != null) {
				txtPeso.Dispose ();
				txtPeso = null;
			}
		}
	}
}
