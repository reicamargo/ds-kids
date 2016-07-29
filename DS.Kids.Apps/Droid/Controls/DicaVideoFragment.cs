
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
using Android.Net;
using Android.Content.Res;
using Android.Content.PM;

namespace DS.Kids.Apps.Droid
{
	public class DicaVideoFragment : Android.Support.V4.App.DialogFragment, Android.Media.MediaPlayer.IOnPreparedListener, Android.Views.View.IOnClickListener
	{
		private readonly Uri uri;
		private FrameLayout mediaWrapper;
		private FrameLayout videoWrapper;
		private VideoView videoView;
		private MediaController mediaController;

		public DicaVideoFragment(string videoURL)
		{
			uri = Uri.Parse(videoURL);
		}

		public override void OnDismiss (IDialogInterface dialog)
		{
			base.OnDismiss (dialog);

			Activity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
		}

		public override Dialog OnCreateDialog(Bundle savedState)
		{
			var activity = this.Activity;
			var dialog = base.OnCreateDialog(savedState);

			var displayMetrics = activity.Resources.DisplayMetrics;
			var height= displayMetrics.WidthPixels;
			var width = displayMetrics.HeightPixels; 	

			activity.RequestedOrientation = ScreenOrientation.Landscape;

			dialog.Window.RequestFeature(WindowFeatures.NoTitle);
			dialog.SetContentView(Resource.Layout.DicaViewFragmentVideo);

			dialog.Window.Attributes.Width = width;
			dialog.Window.Attributes.Height = height - 88;
			dialog.Window.Attributes.HorizontalMargin = 0f;
			dialog.Window.Attributes.VerticalMargin = 0f;

			videoView = dialog.FindViewById<VideoView>(Resource.Id.fragment_videoView);
			mediaWrapper = dialog.FindViewById<FrameLayout>(Resource.Id.mediaWrapper);
			videoWrapper = dialog.FindViewById<FrameLayout>(Resource.Id.videoWrapper);

			videoView.SetOnPreparedListener(this);
			videoView.SetScrollContainer(false);
			videoView.SetVideoURI(uri);

			videoView.SetMinimumWidth(width);
			videoView.SetMinimumHeight(height);

			videoView.Start();

			return dialog;
		}

		public void OnClick (View v)
		{
			if(mediaWrapper.IsShown) {
				mediaWrapper.Visibility = ViewStates.Gone;
			} else { 
				mediaWrapper.Visibility = ViewStates.Visible;
			}
		}

		public void OnPrepared (Android.Media.MediaPlayer mp)
		{
			mediaController = new MediaController(Activity);
			videoView.SetMediaController(mediaController);
			mediaController.SetMediaPlayer(videoView);
			mediaController.SetAnchorView(mediaWrapper);

			(mediaController.Parent as ViewGroup).RemoveView(mediaController);
			mediaWrapper.AddView(mediaController);

			videoWrapper.SetOnClickListener(this);
			mediaController.Hide();
		}
	}
}

