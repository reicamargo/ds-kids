using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

using Android.App;
using Android.Content;
using Android.Runtime;

using BRFX.Core;
using BRFX.Core.Droid;
using BRFX.Core.Droid.Helpers;
using BRFX.Core.Droid.Plugins;
using BRFX.Core.Droid.Views;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Plugins.DownloadCache;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.Droid.Analytics;
using DS.Kids.Apps.Droid.Plugins;
using DS.Kids.Apps.Droid.Views;

using Parse;

using File = Java.IO.File;

// NOTE: Facebook SDK rquires that the 'Value' point to a string resource
//       in your values/ folder (eg: strings.xml file).
//       It will not allow you to use the app_id value directly here!
[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]

namespace DS.Kids.Apps.Droid
{

	public class Setup : Setup<MainViewModel, MainView, MainViewModel>
	{
		#region Constructors and Destructors

		public Setup(Context applicationContext)
			: base(applicationContext)
		{
			ParseClient.Initialize(SettingsHelper.ParseApplicationId, SettingsHelper.ParseDotNetKey);
			ParsePush.ParsePushNotificationReceived += ParsePush.DefaultParsePushNotificationReceivedHandler;

#if !DEBUG
			IO.Fabric.Sdk.Android.Fabric.With(applicationContext, new Com.Crashlytics.Android.Crashlytics());
#endif
			
			FontManager.Initialize(applicationContext, new Dictionary<string, string>
															{
																{ "Hangyaboly", "fonts/Hangyaboly.ttf" },
															});
		}

		#endregion

		protected override IList<Assembly> AndroidViewAssemblies
		{
			get
			{
				var assemblies = base.AndroidViewAssemblies;
				assemblies.Add(typeof(MainView).Assembly);
				return assemblies;
			}
		}

		protected override IMvxApplication CreateApp()
		{
			return new PortableApp<MainViewModel>(Platform.Android);
		}

		protected override IMvxAndroidViewPresenter CreateViewPresenter()
		{
			if(FileExists("login.dat"))
			{
				var customPresenter = new CustomViewPresenter<DiarioViewModel>();
				Mvx.RegisterSingleton<ICustomPresenter>(customPresenter);
				return customPresenter;
			}
			else
			{
				var customPresenter = new CustomViewPresenter<MainViewModel>();
				Mvx.RegisterSingleton<ICustomPresenter>(customPresenter);
				return customPresenter;
			}
		}

		private bool FileExists(string fileName)
		{
			var path = Path.Combine(ApplicationContext.FilesDir.Path, fileName);

			File file = new File(path);
			return file.Exists();
		}

		#region Methods

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			pluginManager.EnsurePluginLoaded<PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.File.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.PictureChooser.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Color.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader>();
		}

		protected override void InitializeLastChance()
		{
			PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.PictureChooser.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Color.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();

			base.InitializeLastChance();

			ServicesHelpers.Initialize();

			var googleAnalytics = new GoogleAnalytics();
			Mvx.RegisterSingleton<IAnalytics>(googleAnalytics);

			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
				{
					Debug.WriteLine("AppDomain.CurrentDomain.UnhandledException: {0}. IsTerminating: {1}", e.ExceptionObject, e.IsTerminating);
					googleAnalytics.SendException(e.ExceptionObject.ToString(), false);
#if !DEBUG
					if(Com.Crashlytics.Android.Crashlytics.Instance != null)
					{
						Com.Crashlytics.Android.Crashlytics.Instance.Core.LogException(Java.Lang.Throwable.FromException((Exception)e.ExceptionObject));
					}
#endif
				};

			AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
			{
				Debug.WriteLine("AndroidEnvironment.UnhandledExceptionRaiser: {0}. IsTerminating: {1}", e.Exception, e.Handled);
				googleAnalytics.SendException(e.Exception.ToString(), true);
#if !DEBUG
				if(Com.Crashlytics.Android.Crashlytics.Instance != null)
				{
					Com.Crashlytics.Android.Crashlytics.Instance.Core.LogException(Java.Lang.Throwable.FromException(e.Exception));
				}
#endif
				e.Handled = true;
			};

			var facebookPlugin = new FacebookPlugin();
			facebookPlugin.Initialize(ApplicationContext.Resources.GetString(Resource.String.app_id));

			var fbDataPlugin = new FbData();
			fbDataPlugin.Initialize();

			var localNotifications = new LocalNotifications();
			localNotifications.Initialize();
		}

		#endregion
	}

}
