using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using BRFX.Core.Droid.Controls;
using BRFX.Core.ViewModels;
using BRFX.Core.Droid.Views;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using DS.Kids.Apps.Droid.Views;
using Android.Content.PM;

namespace DS.Kids.Apps.Droid
{
	[Register("ds.kids.apps.droid.DicaVideoView")]
	public class DicaVideoView : BRFXSVGImageView
	{
		private string _videoURL;

		public string VideoURL
		{
			get { return _videoURL; }
			set 
			{
				_videoURL = value;
			}
		}

		public DicaVideoView (Context context) :
		base (context)
		{
			Initialize ();
		}

		public DicaVideoView (Context context, IAttributeSet attrs) :
		base (context, attrs)
		{
			Initialize ();
		} 	

		void Initialize ()
		{
			this.Click += (sender, e) => {

				var dialog = new DicaVideoFragment(VideoURL);

				var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity as MainView;

				dialog.Show(activity.SupportFragmentManager, "video");
			};	
		}
	}
}