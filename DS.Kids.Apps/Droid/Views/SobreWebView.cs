using Android.OS;
using Android.Views;
using Android.Webkit;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Views
{

	internal class SobreWebView : BaseView
	{

		private WebView _webView;

		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			var sobreWebViewModel = ViewModel as SobreWebViewModel;
			if(sobreWebViewModel != null)
			{
				Title = sobreWebViewModel.Title;
			}
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(Resource.Layout.SobreWebView, null);

			_webView = view.FindViewById<WebView>(Resource.Id.sobreWebView_webView);

			var sobreWebViewModel = ViewModel as SobreWebViewModel;
			if(sobreWebViewModel != null)
			{
				Title = sobreWebViewModel.Title;

				sobreWebViewModel.WebViewLoadStarted();

				_webView.SetWebViewClient(new MyWebViewClient(sobreWebViewModel));

				_webView.LoadUrl(sobreWebViewModel.Url);
			}

			return view;
		}

		private class MyWebViewClient : WebViewClient
		{
			#region Fields

			private readonly SobreWebViewModel _sobreWebViewModel;

			#endregion

			#region Constructors and Destructors

			public MyWebViewClient(SobreWebViewModel sobreWebViewModel)
			{
				_sobreWebViewModel = sobreWebViewModel;
			}

			#endregion

			#region Public Methods and Operators

			public override void OnPageFinished(WebView webView, string url)
			{
				_sobreWebViewModel.WebViewLoadFinished();

				base.OnPageFinished(webView, url);
			}

			#endregion
		}

		#endregion
	}

}
