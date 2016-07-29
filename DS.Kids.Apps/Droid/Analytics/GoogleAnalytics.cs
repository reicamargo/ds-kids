using System.Collections.Generic;

using Android.Gms.Analytics;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;

namespace DS.Kids.Apps.Droid.Analytics
{

	public class GoogleAnalytics : IAnalytics
	{
		#region Fields

		private readonly Tracker _tracker;

		#endregion

		#region Constructors and Destructors

		public GoogleAnalytics()
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			if(activity == null)
			{
				return;
			}

			_tracker = Android.Gms.Analytics.GoogleAnalytics.GetInstance(activity).NewTracker(SettingsHelper.GoogleAnalyticsId);
			_tracker.SetAppName("DSKids");
			_tracker.SetAppVersion(SettingsHelper.Version.ToString());
			_tracker.EnableExceptionReporting(true);
		}

		#endregion

		#region Public Methods and Operators

		public void SendException(string description, bool isFatal)
		{
			if(_tracker == null)
			{
				return;
			}

			_tracker.Send(new HitBuilders.ExceptionBuilder().SetDescription(description).SetFatal(isFatal).Build());
		}

		public void SendView(string screenName)
		{
			if(_tracker == null)
			{
				return;
			}

			_tracker.SetScreenName(SettingsHelper.GoogleAnalyticsViewPrefix + screenName);

			var build = new HitBuilders.ScreenViewBuilder().Build();
			var build2 = new Dictionary<string, string>();
			foreach(var key in build.Keys)
			{
				build2.Add(key, build[key]);
			}

			_tracker.Send(build2);
		}

		#endregion
	}

}
