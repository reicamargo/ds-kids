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
	[Register ("CrescimentoView")]
	partial class CrescimentoView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView graphArea { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tableView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView viewLeaderBoard { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (graphArea != null) {
				graphArea.Dispose ();
				graphArea = null;
			}
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
			if (viewLeaderBoard != null) {
				viewLeaderBoard.Dispose ();
				viewLeaderBoard = null;
			}
		}
	}
}
