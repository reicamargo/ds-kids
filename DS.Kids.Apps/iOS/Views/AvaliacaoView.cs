using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using CorePlot;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	public partial class AvaliacaoView : BaseView, IMvxModalTouchView 
	{

		private CPTGraphHostingView _hostView;

		private CPTXYGraph _graph;

		private GraphHelper.MyDataSource _dataSource;

		public AvaliacaoView(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			NavigationController.NavigationBarHidden = true;

			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<AvaliacaoView, AvaliacaoViewModel>();

			bindingSet.Bind(txtDescricao).To(vm => vm.Descricao);
			bindingSet.Bind(btnComecar).To(vm => vm.ComecarCommand);

			txtDescricao.ShouldBeginEditing += view => false;
			txtDescricao.ShouldChangeText += (field, range, replacementString) => false;

			bindingSet.Apply();

			GraphHelper.InitPlot(out _hostView, out _graph, out _dataSource, graphArea.Frame, 6, false);
			graphArea.AddSubview(_hostView);
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

	}

}
