using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.iOS.Views
{

	partial class SobreWebView : ProgressView
	{
		#region Constructors and Destructors

		public SobreWebView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<SobreWebView, SobreWebViewModel>();

			bindingSet.Bind(this).For(v => v.Title).To(vm => vm.Title);
			bindingSet.Bind(webView).For(v => v.Url).To(vm => vm.Url);

			webView.LoadFinished += WebViewOnLoadFinished;

			var sobreWebViewModel = ViewModel as SobreWebViewModel;
			if(sobreWebViewModel != null)
			{
				sobreWebViewModel.WebViewLoadStarted();
			}

			bindingSet.Apply();
		}

		#endregion

		#region Methods

		private void WebViewOnLoadFinished(object sender, EventArgs eventArgs)
		{
			webView.EvaluateJavascript(string.Format("document.querySelector('meta[name=viewport]').setAttribute('content', 'width={0};', false); ", (int)webView.Frame.Size.Width));
			var sobreWebViewModel = ViewModel as SobreWebViewModel;
			if(sobreWebViewModel != null)
			{
				sobreWebViewModel.WebViewLoadFinished();
			}
		}

		#endregion
	}

}
