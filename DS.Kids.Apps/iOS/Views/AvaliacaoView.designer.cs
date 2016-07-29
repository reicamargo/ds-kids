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
	[Register ("AvaliacaoView")]
	partial class AvaliacaoView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnComecar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView graphArea { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView txtDescricao { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnComecar != null) {
				btnComecar.Dispose ();
				btnComecar = null;
			}
			if (graphArea != null) {
				graphArea.Dispose ();
				graphArea = null;
			}
			if (txtDescricao != null) {
				txtDescricao.Dispose ();
				txtDescricao = null;
			}
		}
	}
}
