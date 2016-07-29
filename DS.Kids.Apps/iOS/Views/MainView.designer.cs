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
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCadastrar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnFacebook { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCadastrar != null) {
				btnCadastrar.Dispose ();
				btnCadastrar = null;
			}
			if (btnEmail != null) {
				btnEmail.Dispose ();
				btnEmail = null;
			}
			if (btnFacebook != null) {
				btnFacebook.Dispose ();
				btnFacebook = null;
			}
		}
	}
}
