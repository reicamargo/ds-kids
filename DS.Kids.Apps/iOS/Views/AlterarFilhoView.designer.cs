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
	[Register ("AlterarFilhoView")]
	partial class AlterarFilhoView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCamera { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnExcluir { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgFilho { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Cirrious.MvvmCross.Binding.Touch.Views.MvxImageView imgFotoFilho { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		TPKeyboardAvoiding.TPKeyboardAvoidingScrollView ScrollView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDataNascimento { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNome { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}
			if (btnExcluir != null) {
				btnExcluir.Dispose ();
				btnExcluir = null;
			}
			if (imgFilho != null) {
				imgFilho.Dispose ();
				imgFilho = null;
			}
			if (imgFotoFilho != null) {
				imgFotoFilho.Dispose ();
				imgFotoFilho = null;
			}
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
			if (txtDataNascimento != null) {
				txtDataNascimento.Dispose ();
				txtDataNascimento = null;
			}
			if (txtNome != null) {
				txtNome.Dispose ();
				txtNome = null;
			}
		}
	}
}
