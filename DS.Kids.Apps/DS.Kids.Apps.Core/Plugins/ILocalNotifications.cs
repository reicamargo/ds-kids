using System;

namespace DS.Kids.Apps.Core.Plugins
{

	public interface ILocalNotifications
	{
		#region Public Methods and Operators

		void CancelAllNotifications();

		void CancelNotification(string notificationId);

		void ScheduleNotification(string id, string body, DateTime startTime);

		#endregion
	}

	public interface ILocalNotification
	{
		#region Public Properties

		// ReSharper disable UnusedMemberInSuper.Global
		string Body { get; }

		string Id { get; }

		DateTime StartTime { get; }
		// ReSharper restore UnusedMemberInSuper.Global

		#endregion
	}

}
