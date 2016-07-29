using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MoPub.MobileAds;
//using MvvmCross.Platform.Platform;

namespace DS.Kids.Apps.Droid.Ads
{
    public class MoPubconfig
    {
#if DEBUG
        public const string AD_UNIT_ID_BANNER = "799363dfd38040fca73115d74a9e1fc3";
        public const string AD_UNIT_ID_MEDIUM = "b81ba2cb71584a3c88fc44b051127e0d";
        public const string AD_UNIT_ID_FULLSCREEN = "4f1fac0eb86246c7a87882c4ec053342";
#else
        public const string AD_UNIT_ID_BANNER = "a79b0ef41a22498892b391c871750c1d";
        public const string AD_UNIT_ID_SQUARE = "b81ba2cb71584a3c88fc44b051127e0d";
        public const string AD_UNIT_ID_FULLSCREEN = "6e53f5f89bcb40399370825211d2dcfa";
#endif
    }

    public class Ad
    {
        private readonly MoPubView _moPubView;
        public Ad(MoPubView view, string adUnitId)
        {
            _moPubView = view;
            _moPubView.AdUnitId = adUnitId;
        }

        public void Load()
        {
            _moPubView.LoadAd();
        }

        public void Destroy()
        {
            _moPubView.Destroy();
        }
    }

    public class Interstitial : MoPubInterstitial.IInterstitialAdListener
    {
        MoPubInterstitial _interstitial;

        public IntPtr Handle
        {
            get
            {
                return IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            if(_interstitial != null) _interstitial.Destroy();
        }

        public Interstitial(Activity activity, string adUnitId)
        {
            _interstitial = new MoPubInterstitial(activity, adUnitId);
            _interstitial.InterstitialAdListener = this;
        }

        public void Load()
        {
            _interstitial.Load();
        }

        public void Destroy()
        {
            this.Dispose();
        }

        public void OnInterstitialClicked(MoPubInterstitial interstitial)
        {
        }

        public void OnInterstitialDismissed(MoPubInterstitial interstitial)
        {
        }

        public void OnInterstitialFailed(MoPubInterstitial interstitial, MoPubErrorCode error)
        {
        }

        public void OnInterstitialLoaded(MoPubInterstitial interstitial)
        {
        }

        public void OnInterstitialShown(MoPubInterstitial interstitial)
        {
            if (interstitial.IsReady) interstitial.Show();
        }
    }
}