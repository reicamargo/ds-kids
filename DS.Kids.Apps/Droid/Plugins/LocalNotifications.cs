using System;
using System.IO;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Plugins
{

	public class LocalNotifications : ILocalNotifications
	{
		#region Constants

		public const string TimerNameKey = "Id";

		#endregion

		#region Public Methods and Operators

		public void CancelAllNotifications()
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			var simpleDataFile = ReadSimpleDataFile(activity);

			if(simpleDataFile != null)
			{
				CancelNotification(CrescimentoHelpers.GetCrescimentoNotificationId(simpleDataFile.CurrentCriancaId));
			}
		}

		public void CancelNotification(string notificationId)
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			var alarmManager = (AlarmManager)activity.GetSystemService(Context.AlarmService);
			var intent = CreateScheduledIntent(notificationId, "", activity);
			alarmManager.Cancel(intent);
		}

		public void Initialize()
		{
			Mvx.RegisterSingleton<ILocalNotifications>(this);
		}

		public void ScheduleNotification(string id, string body, DateTime startTime)
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;

			ScheduleNotificationWithContext(activity, id, body, startTime);
		}

		internal static void ScheduleNotificationWithContext(Context context, string id, string body, DateTime startTime)
		{
			var alarmIntent = CreateScheduledIntent(id, body, context);

			var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
			var dif = startTime - DateTime.UtcNow;
			if(dif.TotalMilliseconds > 0)
			{
				alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + (int)dif.TotalMilliseconds, alarmIntent);
			}
		}

		#endregion

		#region Methods

		private static PendingIntent CreateScheduledIntent(string id, string body, Context context)
		{
			var bundle = new Bundle();
			bundle.PutString("id", id);
			bundle.PutString("body", body);

			var intent = new Intent(context, typeof(AlarmReceiver));
			intent.PutExtras(bundle);

			return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
		}

		public static SimpleDataFile ReadSimpleDataFile(Context context)
		{
			var path = Path.Combine(context.FilesDir.Path, LoginHelper.SimpleDataFile);

			using(var file = OpenRead(path))
			{
				if(file != null)
				{
					var data = new byte[file.Length];
					file.Read(data, 0, data.Length);
					var fileString = Encoding.UTF8.GetString(data, 0, data.Length);
					return fileString.FromJson<SimpleDataFile>();
				}
			}

			return null;
		}

		public static Stream OpenRead(string fullPath)
		{
			if(!File.Exists(fullPath))
			{
				return null;
			}

			return File.OpenRead(fullPath);
		}

		#endregion
	}

	[BroadcastReceiver(Enabled = true)]
	public class AlarmReceiver : BroadcastReceiver
	{
		#region Public Methods and Operators

		public override void OnReceive(Context pContext, Intent pIntent)
		{
			var setupSingleton = MvxAndroidSetupSingleton.EnsureSingletonAvailable(pContext);
			setupSingleton.EnsureInitialized();

			var request = MvxViewModelRequest<MainViewModel>.GetDefaultRequest();
			var translator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator>();
			var notificationIntent = translator.GetIntentFor(request);

			var contentIntent = PendingIntent.GetActivity(pContext, 0, notificationIntent, 0);

			var notificationBuilder = new NotificationCompat.Builder(pContext)
				.SetSmallIcon(Resource.Drawable.ic_launcher)
				.SetContentTitle("DS Kids")
				.SetContentText(pIntent.GetStringExtra("body"))
				.SetAutoCancel(true)
				.SetContentIntent(contentIntent);

			var notificationManager = (NotificationManager)pContext.GetSystemService(Context.NotificationService);
			notificationManager.Notify(1, notificationBuilder.Build());
		}

		#endregion
	}

	[BroadcastReceiver(Enabled = true)]
	[IntentFilter(new[]
					{
						"android.intent.action.BOOT_COMPLETED"
					})]
	public class OnBootReceiver : BroadcastReceiver
	{
		#region Public Methods and Operators

		public override void OnReceive(Context context, Intent intent)
		{
			var simpleDataFile = LocalNotifications.ReadSimpleDataFile(context);

			if(simpleDataFile != null)
			{
				LocalNotifications.ScheduleNotificationWithContext(context, CrescimentoHelpers.GetCrescimentoNotificationId(simpleDataFile.CurrentCriancaId), simpleDataFile.Body, simpleDataFile.StartTime);
			}
		}

		#endregion
	}

}
