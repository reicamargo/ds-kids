package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (!initialized) {
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				initialized = true;
			}
		}
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		"DS.Kids.Apps.Droid.dll",
		"BRFX.Core.dll",
		"BRFX.Core.Droid.dll",
		"Cirrious.CrossCore.dll",
		"Cirrious.CrossCore.Droid.dll",
		"Cirrious.MvvmCross.Binding.dll",
		"Cirrious.MvvmCross.Binding.Droid.dll",
		"Cirrious.MvvmCross.dll",
		"Cirrious.MvvmCross.Droid.dll",
		"Cirrious.MvvmCross.Droid.Fragging.dll",
		"Cirrious.MvvmCross.Localization.dll",
		"Cirrious.MvvmCross.Plugins.Color.dll",
		"Cirrious.MvvmCross.Plugins.Color.Droid.dll",
		"Cirrious.MvvmCross.Plugins.DownloadCache.dll",
		"Cirrious.MvvmCross.Plugins.DownloadCache.Droid.dll",
		"Cirrious.MvvmCross.Plugins.File.dll",
		"Cirrious.MvvmCross.Plugins.File.Droid.dll",
		"Cirrious.MvvmCross.Plugins.Json.dll",
		"Cirrious.MvvmCross.Plugins.Messenger.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.Droid.dll",
		"Cirrious.MvvmCross.Plugins.Visibility.dll",
		"Cirrious.MvvmCross.Plugins.Visibility.Droid.dll",
		"Cirrious.MvvmCross.Plugins.WebBrowser.dll",
		"Cirrious.MvvmCross.Plugins.WebBrowser.Droid.dll",
		"Crashlytics.Droid.dll",
		"DS.Kids.Apps.Core.dll",
		"DS.Kids.Model.Communication.dll",
		"DS.Kids.Model.dll",
		"MoPubAndroid.dll",
		"Newtonsoft.Json.dll",
		"OxyPlot.dll",
		"OxyPlot.Xamarin.Android.dll",
		"Parse.Android.dll",
		"SvgAndroid.dll",
		"System.Net.Http.Extensions.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.CardView.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Facebook.dll",
		"Xamarin.GooglePlayServices.Analytics.dll",
		"Xamarin.GooglePlayServices.Base.dll",
		"ICSharpCode.SharpZipLib.Portable.dll",
		"System.ServiceModel.Internals.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}
