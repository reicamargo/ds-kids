using System;

using BRFX.Core.IOS;
using BRFX.Core.IOS.Plugins;

using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.iOS.Plugins;

using UIKit;

namespace DS.Kids.Apps.iOS
{

	public class Setup<T, TU> : BRFX.Core.IOS.Setup<T, TU>
		where T : MvxViewModel where TU : IMvxTouchView
	{
		#region Constructors and Destructors

		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window, string storyBoard)
			: base(applicationDelegate, new CustomDisposerPresenter(applicationDelegate, window), storyBoard)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void LoadPlugins(IMvxPluginManager pluginManager)
		{
			base.LoadPlugins(pluginManager);
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.DownloadCache.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.File.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Visibility.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.PictureChooser.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Color.PluginLoader>();
			pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader>();
		}

		#endregion

		#region Methods

		protected override void AddPluginsLoaders(MvxLoaderPluginRegistry registry)
		{
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Visibility.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.DownloadCache.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.File.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.PictureChooser.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.Color.Touch.Plugin>();
			registry.AddConventionalPlugin<Cirrious.MvvmCross.Plugins.WebBrowser.Touch.Plugin>();

			base.AddPluginsLoaders(registry);
		}

		protected override void InitializeLastChance()
		{
			Cirrious.MvvmCross.Plugins.DownloadCache.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.File.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Json.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Visibility.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.PictureChooser.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Color.PluginLoader.Instance.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.WebBrowser.PluginLoader.Instance.EnsureLoaded();

			base.InitializeLastChance();

			ServicesHelpers.Initialize();

			IAnalytics googleAnalytics = new Analytics.GoogleAnalytics();
			Mvx.RegisterSingleton(googleAnalytics);

			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
				{
					googleAnalytics.SendException(e.ExceptionObject.ToString(), false);
					System.Diagnostics.Debug.WriteLine("AppDomain.CurrentDomain.UnhandledException: {0}. IsTerminating: {1}", e.ExceptionObject, e.IsTerminating);
				};

			var facebookPlugin = new FacebookPlugin();
			facebookPlugin.Initialize(SettingsHelper.FacebookAppId);

			var fbDataPlugin = new FbData();
			fbDataPlugin.Initialize();

			var localNotifications = new LocalNotifications();
			localNotifications.Initialize();
		}

		#endregion
	}

}
