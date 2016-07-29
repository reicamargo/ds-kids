using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class CategoriaView : ProgressView
	{
		#region Constructors and Destructors

		public CategoriaView(IntPtr handle)
			: base(handle)
		{
			this.OnViewCreate();
		}

		#endregion

		#region Public Methods and Operators

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<CategoriaView, CategoriaViewModel>();

			NavigationItem.BackBarButtonItem = new UIBarButtonItem
													{
														Title = "Dicas"
													};

			var source = new CategoriaTableViewSource(tableView);
			tableView.Source = source;

			bindingSet.Bind(source).For(s => s.Categoria).To(vm => vm.Categoria);
			bindingSet.Bind(source).To(vm => vm.Dicas);
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.DicaSelectedCommand);

			bindingSet.Apply();

			tableView.ReloadData();
		}

		#endregion
	}

}
