using System;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Plugins;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Plugins
{

	public class LocalNotifications : ILocalNotifications
	{
		#region Constants

		public const string TimerNameKey = "Id";

		#endregion

		#region Public Methods and Operators

		public void CancelAllNotifications()
		{
			UIApplication.SharedApplication.CancelAllLocalNotifications();
		}

		public void CancelNotification(string notificationId)
		{
			foreach(var ln in UIApplication.SharedApplication.ScheduledLocalNotifications)
			{
				var id = (NSString)ln.UserInfo[TimerNameKey];
				if(notificationId == id)
				{
					UIApplication.SharedApplication.CancelLocalNotification(ln);
				}
			}
		}

		public void Initialize()
		{
			Mvx.RegisterSingleton<ILocalNotifications>(this);
		}

		public void ScheduleNotification(string id, string body, DateTime startTime)
		{
			var userInfo = NSDictionary.FromObjectsAndKeys(
				new object[]
					{
						id
					},
				new object[]
					{
						TimerNameKey
					});
			var notification = new UILocalNotification
									{
										AlertBody = body,
										FireDate = (NSDate)startTime,
										UserInfo = userInfo,
										TimeZone = NSTimeZone.DefaultTimeZone
									};
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
		}

		#endregion
	}

	public class LocalNotification : ILocalNotification
	{
		#region Constructors and Destructors

		internal LocalNotification(string id, string body, DateTime startTime)
		{
			Id = id;
			Body = body;
			StartTime = startTime;
		}

		#endregion

		#region Public Properties

		public string Body { get; private set; }

		public string Id { get; private set; }

		public DateTime StartTime { get; private set; }

		#endregion
	}

}
