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
	[Register ("SemaforoView")]
	partial class SemaforoView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView bolaAmarela { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView bolaVerde { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView bolaVermelha { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ConteudoView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView DescricaoView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ResultadoNaoEncontradoLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ResultadoNaoEncontradoView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISearchBar searchBar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tableView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (bolaAmarela != null) {
				bolaAmarela.Dispose ();
				bolaAmarela = null;
			}
			if (bolaVerde != null) {
				bolaVerde.Dispose ();
				bolaVerde = null;
			}
			if (bolaVermelha != null) {
				bolaVermelha.Dispose ();
				bolaVermelha = null;
			}
			if (ConteudoView != null) {
				ConteudoView.Dispose ();
				ConteudoView = null;
			}
			if (DescricaoView != null) {
				DescricaoView.Dispose ();
				DescricaoView = null;
			}
			if (ResultadoNaoEncontradoLabel != null) {
				ResultadoNaoEncontradoLabel.Dispose ();
				ResultadoNaoEncontradoLabel = null;
			}
			if (ResultadoNaoEncontradoView != null) {
				ResultadoNaoEncontradoView.Dispose ();
				ResultadoNaoEncontradoView = null;
			}
			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}
			if (tableView != null) {
				tableView.Dispose ();
				tableView = null;
			}
		}
	}
}
