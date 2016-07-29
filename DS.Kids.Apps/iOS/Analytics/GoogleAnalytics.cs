using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;

using GoogleAnalytics.iOS;

namespace DS.Kids.Apps.iOS.Analytics
{

	public class GoogleAnalytics : IAnalytics
	{
		#region Fields

		private readonly IGAITracker _tracker;

		#endregion

		#region Constructors and Destructors

		public GoogleAnalytics()
		{
			// Optional: set Google Analytics dispatch interval to e.g. 20 seconds.
			GAI.SharedInstance.DispatchInterval = 20;

			// Optional: automatically send uncaught exceptions to Google Analytics.
			GAI.SharedInstance.TrackUncaughtExceptions = true;

			// Initialize tracker.
			_tracker = GAI.SharedInstance.GetTracker(SettingsHelper.GoogleAnalyticsId);

			_tracker.Set(GAIConstants.AppName, "DSKids");
			_tracker.Set(GAIConstants.AppVersion, SettingsHelper.Version.ToString());
			GAI.SharedInstance.TrackUncaughtExceptions = true;
		}

		#endregion

		#region Public Methods and Operators

		public void SendException(string description, bool isFatal)
		{
			if (_tracker == null)
			{
				return;
			}

			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			_tracker.Send(GAIDictionaryBuilder.CreateException(description, isFatal).Build());
		}

		public void SendView(string screenName)
		{
			if (_tracker == null)
			{
				return;
			}

			_tracker.Set(GAIConstants.ScreenName, SettingsHelper.GoogleAnalyticsViewPrefix + screenName);
			_tracker.Send(GAIDictionaryBuilder.CreateScreenView().Build());
		}

		#endregion
	}

}
