using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Plugins;
using BRFX.Core.Droid.Views;
using BRFX.Core.Plugins;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.ViewModels;

using Debug = System.Diagnostics.Debug;

namespace DS.Kids.Apps.Droid.Views
{

	[Activity(Label = "DS Kids", LaunchMode = LaunchMode.SingleTop, Theme = "@style/Theme.Dskids", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize,  WindowSoftInputMode = SoftInput.AdjustPan)]
	public class MainView : NavigationDrawerBaseView, ILocationListener
	{
		#region Public Methods and Operators

		public MainView()
		{
			AllowEmptyNavigationStack = true;
			AnimateAlpha = false;
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			var baseReturn = base.OnCreateOptionsMenu(menu);
			
			return false;
		}

		/// <summary>
		/// Called when the activity is starting.
		/// </summary>
		/// <remarks>
		/// <para tool="javadoc-to-mdoc">
		/// Called when the activity is starting.  This is where most initialization
		///  should go: calling <c><see cref="M:Android.App.Activity.SetContentView(System.Int32)"/></c> to inflate the
		///  activity's UI, using <c><see cref="M:Android.App.Activity.FindViewById(System.Int32)"/></c> to programmatically interact
		///  with widgets in the UI, calling
		///  <c><see cref="M:Android.App.Activity.ManagedQuery(Android.Net.Uri, System.String[], System.String[], System.String[], System.String[])"/></c> to retrieve
		///  cursors for data being displayed, etc.
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// You can call <c><see cref="M:Android.App.Activity.Finish"/></c> from within this function, in
		///  which case onDestroy() will be immediately called without any of the rest
		///  of the activity lifecycle (<c><see cref="M:Android.App.Activity.OnStart"/></c>, <c><see cref="M:Android.App.Activity.OnResume"/></c>,
		///  <c><see cref="M:Android.App.Activity.OnPause"/></c>, etc) executing.
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// <i>Derived classes must call through to the super class's
		///  implementation of this method.  If they do not, an exception will be
		///  thrown.</i>
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// <format type="text/html"><a href="http://developer.android.com/reference/android/app/Activity.html#onCreate(android.os.Bundle)" target="_blank">[Android Documentation]</a></format>
		/// </para>
		/// </remarks>
		/// <since version="Added in API level 1"/><altmember cref="M:Android.App.Activity.OnStart"/><altmember cref="M:Android.App.Activity.OnSaveInstanceState(Android.OS.Bundle)"/><altmember cref="M:Android.App.Activity.OnRestoreInstanceState(Android.OS.Bundle)"/><altmember cref="M:Android.App.Activity.OnPostCreate(Android.OS.Bundle)"/>
		protected override void OnCreate(Bundle bundle)
		{
			var scale = Resources.DisplayMetrics.Density;

			DefaultActionBarElevation = (int)(4 * scale + 0.5f);

			Setup setup = null;
			try
			{
				setup = Mvx.GetSingleton<IMvxAndroidGlobals>() as Setup;
			}
			catch
			{
				// ignored
			}

			if (setup == null || setup.State != MvxSetup.MvxSetupState.Initialized)
			{
				OnCreateForNotifications(bundle);

				Finish();
			}
			else
			{
				base.OnCreate(bundle);

				var facebookPlugin = (FacebookPlugin)Mvx.GetSingleton<IFacebook>();

				facebookPlugin.CurrentActivity = this;
			}

			//PackageInfo info = PackageManager.GetPackageInfo("br.com.DSKids", PackageInfoFlags.Signatures);

			//foreach (var signature in info.Signatures)
			//{
			//	var md = Java.Security.MessageDigest.GetInstance("SHA");
			//	md.Update(signature.ToByteArray());
			//	System.Diagnostics.Debug.WriteLine("KeyHash:" + Android.Util.Base64.EncodeToString(md.Digest(), Android.Util.Base64Flags.Default));
			//}
		}

		public void OnLocationChanged(Location location)
		{
			Debug.WriteLine(location);
		}
		public void OnProviderDisabled(string provider)
		{
		}
		public void OnProviderEnabled(string provider)
		{
		}
		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}

		/// <summary>
		/// Perform any final cleanup before an activity is destroyed.
		/// </summary>
		/// <remarks>
		/// <para tool="javadoc-to-mdoc">
		/// Perform any final cleanup before an activity is destroyed.  This can
		///  happen either because the activity is finishing (someone called
		///  <c><see cref="M:Android.App.Activity.Finish"/></c> on it, or because the system is temporarily destroying
		///  this instance of the activity to save space.  You can distinguish
		///  between these two scenarios with the <c><see cref="P:Android.App.Activity.IsFinishing"/></c> method.
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// <i>Note: do not count on this method being called as a place for
		///  saving data! For example, if an activity is editing data in a content
		///  provider, those edits should be committed in either <c><see cref="M:Android.App.Activity.OnPause"/></c> or
		///  <c><see cref="M:Android.App.Activity.OnSaveInstanceState(Android.OS.Bundle)"/></c>, not here.</i> This method is usually implemented to
		///  free resources like threads that are associated with an activity, so
		///  that a destroyed activity does not leave such things around while the
		///  rest of its application is still running.  There are situations where
		///  the system will simply kill the activity's hosting process without
		///  calling this method (or any others) in it, so it should not be used to
		///  do things that are intended to remain around after the process goes
		///  away.
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// <i>Derived classes must call through to the super class's
		///  implementation of this method.  If they do not, an exception will be
		///  thrown.</i>
		/// </para>
		/// <para tool="javadoc-to-mdoc">
		/// <format type="text/html"><a href="http://developer.android.com/reference/android/app/Activity.html#onDestroy()" target="_blank">[Android Documentation]</a></format>
		/// </para>
		/// </remarks>
		/// <since version="Added in API level 1"/><altmember cref="M:Android.App.Activity.OnPause"/><altmember cref="M:Android.App.Activity.OnStop"/><altmember cref="M:Android.App.Activity.Finish"/><altmember cref="P:Android.App.Activity.IsFinishing"/>
		protected override void OnDestroy()
		{
			Setup setup = null;
			try
			{
				setup = Mvx.GetSingleton<IMvxAndroidGlobals>() as Setup;
			}
			catch
			{
				// ignored
			}

			if (setup == null || setup.State != MvxSetup.MvxSetupState.Initialized)
			{
				OnDestroyForNotifications();
				var intent = new Intent(this, typeof(SplashScreen));
				intent.SetFlags(ActivityFlags.SingleTop);
				StartActivity(intent);

				var newSetup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(this);
				newSetup.EnsureInitialized();

				var starter = Mvx.Resolve<IMvxAppStart>();
				starter.Start();

				return;
			}

			base.OnDestroy();

			var facebookPlugin = (FacebookPlugin)Mvx.GetSingleton<IFacebook>();
			facebookPlugin.CurrentActivity = null;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			var facebookPlugin = (FacebookPlugin)Mvx.GetSingleton<IFacebook>();
			if(facebookPlugin.CallbackManager != null)
			{
				facebookPlugin.CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
			}
		}

		#endregion

		#region Methods

		/// <summary>
		///     Usar este método para inicializar a view com os layouts
		/// </summary>
		protected override NavigationDrawerViewParams InitParams()
		{
			var viewParams = new NavigationDrawerViewParams
								{
									InitialLoadingViewType = typeof(MainLoadingView),

									ContentHostResourceId = Resource.Id.content_frame,
									DisplayHomeAsUpEnabled = false,
									DrawerLayoutViewResourceId = Resource.Id.drawer_layout,

									DrawerLeftHolderResourceId = Resource.Id.frame_left_layout,
									DrawerLeftViewModelType = typeof(LeftMenuViewModel),

									DrawerToggleCloseStringResourceId = Resource.String.drawer_close,
									DrawerToggleOpenStringResourceId = Resource.String.drawer_open,
									ToolbarResourceId = Resource.Id.toolbar,
									HomeButtonEnabled = true,
									ViewResourceId = Resource.Layout.MainView
								};
			return viewParams;
		}

		#endregion
	}

}
