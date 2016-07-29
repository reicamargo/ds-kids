using Android.App;
using Android.Content.PM;

using Cirrious.MvvmCross.Droid.Views;

namespace DS.Kids.Apps.Droid
{
	[Activity(Label = "DS Kids", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		#region Constructors and Destructors

		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		#endregion
	}
}
