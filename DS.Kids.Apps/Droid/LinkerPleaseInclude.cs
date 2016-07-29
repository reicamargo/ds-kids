using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

using Android.Graphics;
using Android.Views;
using Android.Widget;

using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Binding.Droid.Binders;
using Cirrious.MvvmCross.Binding.Droid.Views;

using Switch = Android.Widget.Switch;

namespace DS.Kids.Apps.Droid
{
	// This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
	// are preserved in the deployed app
	public class LinkerPleaseInclude
	{

		public void Include(Button button)
		{
			button.Click += (s, e) => button.Text = button.Text + "";
		}

		public void Include(CheckBox checkBox)
		{
			checkBox.CheckedChange += (sender, args) => checkBox.Checked = !checkBox.Checked;
		}

		public void Include(View view)
		{
			view.Click += (s, e) => view.ContentDescription = view.ContentDescription + "";
		}

		public void Include(TextView text)
		{
			text.TextChanged += (sender, args) => text.Text = "" + text.Text;
			text.Hint = "" + text.Hint;
		}

		public void Include(CompoundButton cb)
		{
			cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked;
		}

		public void Include(SeekBar sb)
		{
			sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1;
		}

		public void Include(INotifyCollectionChanged changed)
		{
			changed.CollectionChanged += (s, e) =>
				{
					var test = string.Format("{0}{1}{2}{3}{4}", e.Action, e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex);
					Debug.WriteLine(test);
				};
		}

		public void Include(ICommand command)
		{
			command.CanExecuteChanged += (s, e) => { if(command.CanExecute(null)) command.Execute(null); };
		}

		public void Include(MvxPropertyInjector injector)
		{
			if(injector == null)
			{
				throw new ArgumentNullException("injector");
			}

			injector = new MvxPropertyInjector();
			injector.Inject(this);
		}

		public void Include(MvxListView listView)
		{
			if(listView == null)
			{
				throw new ArgumentNullException("listView");
			}

			listView = new MvxListView(null, null);
			listView.ChildViewAdded += (sender, args) =>
				{

				};
			listView = new MvxListView(null, null, null);
			listView.ChildViewAdded += (sender, args) =>
				{

				};
		}

		public void Include(Cirrious.MvvmCross.Plugins.Color.Droid.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.Color.Droid.Plugin x = new Cirrious.MvvmCross.Plugins.Color.Droid.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.DownloadCache.Droid.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.DownloadCache.Droid.Plugin x = new Cirrious.MvvmCross.Plugins.DownloadCache.Droid.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.File.Droid.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.File.Droid.Plugin x = new Cirrious.MvvmCross.Plugins.File.Droid.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.WebBrowser.Droid.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.WebBrowser.Droid.Plugin x = new Cirrious.MvvmCross.Plugins.WebBrowser.Droid.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.Droid.Plugin plugin)
		{
			plugin.Load();
			Cirrious.MvvmCross.Plugins.Visibility.Droid.Plugin x = new Cirrious.MvvmCross.Plugins.Visibility.Droid.Plugin();
			x.Load();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.PluginLoader pluginLoader)
		{
			pluginLoader.EnsureLoaded();
			Cirrious.MvvmCross.Plugins.Visibility.PluginLoader x = new Cirrious.MvvmCross.Plugins.Visibility.PluginLoader();
			x.EnsureLoaded();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Json.PluginLoader pluginLoader)
		{
			pluginLoader.EnsureLoaded();
			pluginLoader = new Cirrious.MvvmCross.Plugins.Json.PluginLoader();
			pluginLoader.EnsureLoaded();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Droid.Plugin x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Droid.Plugin();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Droid.MvxPictureChooserTask x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Droid.MvxPictureChooserTask();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.PictureChooser.Droid.MvxInMemoryImageValueConverter x)
		{
			x.DisposeIfDisposable();
			x = new Cirrious.MvvmCross.Plugins.PictureChooser.Droid.MvxInMemoryImageValueConverter();
			x.DisposeIfDisposable();
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter converter)
		{
			converter.Convert(null, null, null, null);
			Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter x = new Cirrious.MvvmCross.Plugins.Visibility.MvxVisibilityValueConverter();
			x.Convert(null, null, null, null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter converter)
		{
			converter.Convert(null, null, null, null);
			Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter x = new Cirrious.MvvmCross.Plugins.Visibility.MvxInvertedVisibilityValueConverter();
			x.Convert(null, null, null, null);
		}

		public void Include(Cirrious.MvvmCross.Plugins.File.Droid.MvxAndroidFileStore mvxAndroidFileStore)
		{
			mvxAndroidFileStore.DeleteFile("");
			Cirrious.MvvmCross.Plugins.File.Droid.MvxAndroidFileStore x = new Cirrious.MvvmCross.Plugins.File.Droid.MvxAndroidFileStore();
			x.DeleteFile("");
		}

		public void Include(Cirrious.MvvmCross.Plugins.DownloadCache.MvxDynamicImageHelper<Bitmap> imageHelper)
		{
			imageHelper.ImageChanged += (sender, args) => { imageHelper.Dispose(); };
			var x = new Cirrious.MvvmCross.Plugins.DownloadCache.MvxDynamicImageHelper<Bitmap>
						{
							ErrorImagePath = ""
						};
			x.Dispose();
		}

		public void Include(Cirrious.MvvmCross.Binding.Droid.Target.MvxCompoundButtonCheckedTargetBinding mvxCompoundButtonCheckedTargetBinding)
		{
			mvxCompoundButtonCheckedTargetBinding.SubscribeToEvents();
			var x = new Cirrious.MvvmCross.Binding.Droid.Target.MvxCompoundButtonCheckedTargetBinding(null, null);
			x.SubscribeToEvents();
		}

		public void Include(Cirrious.MvvmCross.Binding.Droid.Target.MvxSeekBarProgressTargetBinding targetBinging)
		{
			targetBinging.SubscribeToEvents();
			targetBinging = new Cirrious.MvvmCross.Binding.Droid.Target.MvxSeekBarProgressTargetBinding(null, null);
			targetBinging.SubscribeToEvents();
		}

		public void Include(ScrollView scrollView)
		{
			scrollView.ArrowScroll(FocusSearchDirection.Forward);
			scrollView = new ScrollView(null);
			scrollView.ArrowScroll(FocusSearchDirection.Forward);
			scrollView = new ScrollView(null, null);
			scrollView.ArrowScroll(FocusSearchDirection.Forward);
			scrollView = new ScrollView(null, null, 0);
			scrollView.ArrowScroll(FocusSearchDirection.Forward);
			scrollView = new ScrollView(null, null, 0, 0);
			scrollView.ArrowScroll(FocusSearchDirection.Forward);
		}

		public void Include(Switch @switch)
		{
			@switch.OnMeasure(0, 0);
			@switch = new Switch(null);
			@switch.OnMeasure(0, 0);
			@switch = new Switch(null, null);
			@switch.OnMeasure(0, 0);
			@switch = new Switch(null, null, 0);
			@switch.OnMeasure(0, 0);
			@switch = new Switch(null, null, 0, 0);
			@switch.OnMeasure(0, 0);
		}

		public void Include(Cirrious.MvvmCross.Plugins.WebBrowser.Droid.MvxWebBrowserTask webBrowserTask)
		{
			webBrowserTask.ShowWebPage("");
			webBrowserTask = new Cirrious.MvvmCross.Plugins.WebBrowser.Droid.MvxWebBrowserTask();
			webBrowserTask.ShowWebPage("");
		}

		public void Include(INotifyPropertyChanged notifyPropertyChanged)
		{
			notifyPropertyChanged.PropertyChanged += (sender, args) =>
				{
					notifyPropertyChanged.PropertyChanged -= null;
				};
		}

		public void Include(Cirrious.MvvmCross.Plugins.Json.MvxJsonConverter jsonConverter)
		{
			jsonConverter.SerializeObject(null);
			jsonConverter = new Cirrious.MvvmCross.Plugins.Json.MvxJsonConverter();
			jsonConverter.DeserializeObject<string>("");
		}

		public void Include(MvxBindingLayoutInflatorFactory mvxBindingLayoutInflatorFactory)
		{
			if(mvxBindingLayoutInflatorFactory == null)
			{
				throw new ArgumentNullException("mvxBindingLayoutInflatorFactory");
			}
			mvxBindingLayoutInflatorFactory = new MvxBindingLayoutInflatorFactory(null);
			mvxBindingLayoutInflatorFactory.OnCreateView(null, null, null, null);
		}

		public void Include(Analytics.GoogleAnalytics ga)
		{
			if(ga == null)
			{
				throw new ArgumentNullException("ga");
			}
			ga = new Analytics.GoogleAnalytics();
			ga.SendView(null);
		}

		public void Include()
		{
			Android.Runtime.JNIEnv.CallStaticObjectMethod(IntPtr.Zero, IntPtr.Zero);
		}

	}
}
