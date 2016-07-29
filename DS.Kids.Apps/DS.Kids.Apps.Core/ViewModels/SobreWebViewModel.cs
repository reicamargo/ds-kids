using BRFX.Core.ViewModels;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Resources;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class SobreWebViewModel : ProgressViewModel<SobreWebViewModelParams>
	{
		#region Fields

		private string _title;

		private string _url;

		#endregion

		#region Constructors and Destructors

		public SobreWebViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("PoliticaPrivacidadeView");
		}

		#endregion

		#region Public Properties

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				Set(ref _title, value);
			}
		}

		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				Set(ref _url, value);
			}
		}

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		public void WebViewLoadFinished()
		{
			StopLoading();
		}

		public void WebViewLoadStarted()
		{
			StartLoading();
		}

		#endregion

		#region Methods

		protected override void GetParams(SobreWebViewModelParams args)
		{
			Url = args.Url;
			Title = args.Title;
		}

		#endregion
	}

	public class SobreWebViewModelParams
	{
		#region Constructors and Destructors

		public SobreWebViewModelParams(string title, string url)
		{
			Title = title;
			Url = url;
		}

		#endregion

		#region Public Properties

		public string Title { get; private set; }

		public string Url { get; private set; }

		#endregion
	}

}
