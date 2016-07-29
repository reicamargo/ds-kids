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
	[Register ("SobreWebView")]
	partial class SobreWebView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		BRFX.Core.IOS.Controls.BRFXWebView webView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (webView != null) {
				webView.Dispose ();
				webView = null;
			}
		}
	}
}
