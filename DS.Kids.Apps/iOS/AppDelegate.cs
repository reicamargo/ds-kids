using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using BRFX.Core.IOS;
using BRFX.Core.IOS.Views;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using Cirrious.MvvmCross.ViewModels;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Plugins;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Plugins;
using DS.Kids.Apps.iOS.Views;
using Facebook.CoreKit;
using Foundation;

using Parse;

using UIKit;
using Task = System.Threading.Tasks.Task;

namespace DS.Kids.Apps.iOS
{

	[Register("AppDelegate")]
	public class AppDelegate : MvxApplicationDelegate
	{

		public AppDelegate()
		{
			// Initialize the Parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseClient.Initialize(SettingsHelper.ParseApplicationId, SettingsHelper.ParseDotNetKey);
		}

		private UIWindow _window;

		public static bool startedFromNotification;

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			CancelNotification(notification);

			var presenter = Mvx.Resolve<IMvxTouchViewPresenter>() as CustomDisposerPresenter;
			if(presenter != null && presenter.MasterNavigationController != null)
			{
				var baseTabView = presenter.MasterNavigationController.TopViewController as BaseTabView;
				if(baseTabView != null && baseTabView.ViewControllers != null)
				{
					baseTabView.SelectedViewController = baseTabView.ViewControllers[1];
				}
			}
		}

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
#if !DEBUG
			Crashlytics.Crashlytics.StartWithAPIKey(SettingsHelper.CrashlyticsApiKey);
#endif

			// Register for Push Notitications
			if(UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				UIUserNotificationType notificationTypes = (UIUserNotificationType.Alert |
															UIUserNotificationType.Badge |
															UIUserNotificationType.Sound);
				var settings = UIUserNotificationSettings.GetSettingsForTypes(notificationTypes, new NSSet());
				UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications();
			}
			else
			{
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			}

			// Handle Push Notifications
			ParsePush.ParsePushNotificationReceived += ParsePushOnParsePushNotificationReceived;

			// create a new window instance based on the screen size
			_window = new UIWindow(UIScreen.MainScreen.Bounds);

#if DEBUG
			DebugTrace.ShowDebugTrace = true;
#endif

			Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
			Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

			InitCustomAppearances();

			UILocalNotification notification = null;
			if(options != null)
			{
				notification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;

				if(notification != null)
				{
					startedFromNotification = true;
				}
			}

#if DEBUG
			try
			{
#endif
				const string viewStoryboard = "ViewsStoryBoard";
				MvxTouchSetup setup;
				if(FileExists("login.dat"))
				{
					setup = new Setup<HomeViewModel, HomeView>(this, _window, viewStoryboard);
				}
				else
				{
					setup = new Setup<MainViewModel, MainView>(this, _window, viewStoryboard);
				}

				setup.Initialize();

				var startup = Mvx.Resolve<IMvxAppStart>();

				startup.Start();
#if DEBUG
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex);
			}
#endif

			if(app.RespondsToSelector(new ObjCRuntime.Selector("registerUserNotificationSettings:")))
			{
				app.RegisterUserNotificationSettings(
					UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, null));
			}
			else
			{
				var myTypes = UIRemoteNotificationType.Alert;
				app.RegisterForRemoteNotificationTypes(myTypes);
			}

			if(notification != null)
			{
				CancelNotification(notification);
			}

			// make the window visible
			_window.MakeKeyAndVisible();

            return ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);
		}

		private static void InitCustomAppearances()
		{
			var backgroundColor = new UIColor(249 / 255.0f, 249 / 255.0f, 249 / 255.0f, 1);
			UINavigationBar.Appearance.SetBackgroundImage(backgroundColor.ToImage(320, 22), UIBarPosition.Any, UIBarMetrics.Default);
			UINavigationBar.Appearance.ShadowImage = new UIImage();
			UINavigationBar.Appearance.BackgroundColor = backgroundColor;
			var textColor = new UIColor(51 / 255.0f, 51 / 255.0f, 51 / 255.0f, 1);
			UINavigationBar.Appearance.BarTintColor = UINavigationBar.Appearance.BackgroundColor;

			var blueColor = new UIColor(86 / 255.0f, 125 / 255.0f, 215 / 255.0f, 1);
			var attributes = new UITextAttributes
								{
									TextColor = textColor
								};
			UINavigationBar.Appearance.SetTitleTextAttributes(attributes);
			UINavigationBar.Appearance.TintColor = blueColor;

			UITabBar.Appearance.TintColor = blueColor;

			/*
			//Old Blue
			[[UIApplication sharedApplication] setStatusBarStyle:UIStatusBarStyleBlackOpaque];
			UINavigationBar *navigationBar = [UINavigationBar appearance];
			UIBarButtonItem *barButtonItem = [UIBarButtonItem appearance];
			UISearchBar *uiSearchBar = [UISearchBar appearance];
			id uiSegmentedControl = [UISegmentedControl appearance];
			id uiToolBarAppearance = [UIToolbar appearance];
			id uiTextFieldAppearance = [UITextField appearance];

			// Load resources for iOS 7 or later
			//New Blue
			UIColor *newBlue = [UIColor colorWithRed:28/255.0 green:134/255.0 blue:238/255.0 alpha:1.0];
			[navigationBar setTintColor:[UIColor whiteColor]];
			[uiTextFieldAppearance setTintColor:[UIColor grayColor]];
			[navigationBar setBarTintColor:newBlue];
			[uiToolBarAppearance setTintColor:newBlue];
			[uiToolBarAppearance setBarTintColor:[UIColor whiteColor]];
	   
	   
	   
			NSDictionary *attributes = [NSDictionary dictionaryWithObject:[DSSignikaFont fontForScopeButtons]
																	forKey:NSFontAttributeName];
			NSDictionary *highlightedAttributes = [NSDictionary dictionaryWithObject:[UIColor whiteColor]
																				forKey:NSForegroundColorAttributeName];
			[uiSegmentedControl setTitleTextAttributes:attributes forState:UIControlStateNormal];
			[uiSegmentedControl setTitleTextAttributes:highlightedAttributes forState:UIControlStateHighlighted];

			NSDictionary *navigationBarTitleAttributes = [NSDictionary dictionaryWithObjectsAndKeys:
					[DSSignikaFont fontForNavigationBarTitle], NSFontAttributeName,
					[UIColor whiteColor], NSForegroundColorAttributeName,
					nil];
			[navigationBar setTitleTextAttributes:navigationBarTitleAttributes];

			NSDictionary *navigationBarButtomsAttributes = [NSDictionary dictionaryWithObjectsAndKeys:
					[DSSignikaFont fontForNavigationBarButtons], NSFontAttributeName,
					nil];
			[barButtonItem setTitleTextAttributes:navigationBarButtomsAttributes forState:UIControlStateNormal];
			*/
		}

		private static void ParsePushOnParsePushNotificationReceived(object sender, ParsePushNotificationEventArgs e)
		{
			Debug.WriteLine(e.StringPayload);
			new UIAlertView("DS Kids", e.StringPayload, null, "OK", null).Show();
		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			application.RegisterForRemoteNotifications();
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
#if DEBUG
			if(ObjCRuntime.Runtime.Arch != ObjCRuntime.Arch.SIMULATOR)
			{
				new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
			}
#endif
		}

		public override async void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			ParseInstallation installation = ParseInstallation.CurrentInstallation;
			installation.SetDeviceTokenFromData(deviceToken);

            //if(LoginHelper.IsLoggedin())
            //{
            //	installation["IdResponsavel"] = LoginHelper.CurrentUser.IdResponsavel;
            //	installation["NomeResponsavel"] = LoginHelper.CurrentUser.Nome;
            //}
            //else
            //{
            //	installation["IdResponsavel"] = 0;
            //	installation["NomeResponsavel"] = "";
            //}

            //Minha solução para o crash maléfico ha ha ha
            try
            {
                await installation.SaveAsync();
            }
            catch { }

			

			//var query = from item in ParseObject.GetQuery(Crianca.ClassName)
			//			orderby item.CreatedAt
			//			select item;

			//try
			//{
			//	var allCriancas = from item in await query.FindAsync()
			//					select new Crianca(item);
			//}
			//catch(Exception ex)
			//{

			//}
		}

		public class Crianca
		{
			public static readonly string ClassName = "Crianca";

			public Crianca() : this(new ParseObject(ClassName)) { }
			public Crianca(ParseObject backingObject)
			{
				if(backingObject.ClassName != ClassName)
				{
					throw new ArgumentException("Must create Crianca with the proper ClassName");
				}
				BackingObject = backingObject;
			}

			public ParseObject BackingObject { get; private set; }

			public ParseInstallation Responsavel
			{
				get
				{
					return BackingObject.ContainsKey("Responsavel") ? BackingObject.Get<ParseInstallation>("Responsavel") : null;
				}
				set
				{
					if(value != Responsavel)
					{
						BackingObject["Responsavel"] = value;
					}
				}
			}

			public int IdCrianca
			{
				get
				{
					return BackingObject.ContainsKey("IdCrianca") ? BackingObject.Get<int>("IdCrianca") : 0;
				}
				set
				{
					if(value != IdCrianca)
					{
						BackingObject["IdCrianca"] = value;
					}
				}
			}

			public string NomeCrianca
			{
				get
				{
					return BackingObject.ContainsKey("NomeCrianca") ? BackingObject.Get<string>("NomeCrianca") : null;
				}
				set
				{
					if(value != NomeCrianca)
					{
						BackingObject["NomeCrianca"] = value;
					}
				}
			}

			public DateTime DataAniversario
			{
				get
				{
					return BackingObject.ContainsKey("DataAniversario") ? BackingObject.Get<DateTime>("DataAniversario") : DateTime.MinValue;
				}
				set
				{
					if(value != DataAniversario)
					{
						BackingObject["DataAniversario"] = value;
					}
				}
			}

			public async Task SaveAsync()
			{
				await BackingObject.SaveAsync();
			}

			public void Revert()
			{
				BackingObject.Revert();
			}
		}

		public class Responsavel
		{
			public static readonly string ClassName = "User";

			public Responsavel() : this(new ParseObject(ClassName)) { }
			public Responsavel(ParseObject backingObject)
			{
				if(backingObject.ClassName != ClassName)
				{
					throw new ArgumentException("Must create Responsavel with the proper ClassName");
				}
				BackingObject = backingObject;
			}

			public ParseObject BackingObject { get; private set; }

			public ParseInstallation Installation
			{
				get
				{
					return BackingObject.ContainsKey("Responsavel") ? BackingObject.Get<ParseInstallation>("Responsavel") : null;
				}
				set
				{
					if(value != Installation)
					{
						BackingObject["Responsavel"] = value;
					}
				}
			}

			public int IdCrianca
			{
				get
				{
					return BackingObject.ContainsKey("IdCrianca") ? BackingObject.Get<int>("IdCrianca") : 0;
				}
				set
				{
					if(value != IdCrianca)
					{
						BackingObject["IdCrianca"] = value;
					}
				}
			}

			public string NomeCrianca
			{
				get
				{
					return BackingObject.ContainsKey("NomeCrianca") ? BackingObject.Get<string>("NomeCrianca") : null;
				}
				set
				{
					if(value != NomeCrianca)
					{
						BackingObject["NomeCrianca"] = value;
					}
				}
			}

			public DateTime DataAniversario
			{
				get
				{
					return BackingObject.ContainsKey("DataAniversario") ? BackingObject.Get<DateTime>("DataAniversario") : DateTime.MinValue;
				}
				set
				{
					if(value != DataAniversario)
					{
						BackingObject["DataAniversario"] = value;
					}
				}
			}

			public async Task SaveAsync()
			{
				await BackingObject.SaveAsync();
			}

			public void Revert()
			{
				BackingObject.Revert();
			}
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			// We need this to fire userInfo into ParsePushNotificationReceived.
			ParsePush.HandlePush(userInfo);
		}

		private static void CancelNotification(UILocalNotification notification)
		{
			var localNotifications = Mvx.Resolve<ILocalNotifications>();
			var id = (NSString)notification.UserInfo[LocalNotifications.TimerNameKey];
			localNotifications.CancelNotification(id);
		}

		private static bool FileExists(string fileName)
		{
			var documentsPath = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0];
			var fullFileName = Path.Combine(documentsPath, fileName);
			return NSFileManager.DefaultManager.FileExists(fullFileName);
		}

		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
		    return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}

		public override void OnActivated(UIApplication application)
		{
			// We need to properly handle activation of the application with regards to SSO
			// (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
            AppEvents.ActivateApp();
		}
	}

}
