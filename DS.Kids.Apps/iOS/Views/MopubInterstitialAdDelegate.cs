using MoPubSDK;

namespace DS.Kids.Apps.iOS.Views
{
    internal class MopubInterstitialAdDelegate : MPInterstitialAdControllerDelegate
    {
        private BrincadeirasView brincadeirasView;

        public MopubInterstitialAdDelegate(BrincadeirasView brincadeirasView)
        {
            this.brincadeirasView = brincadeirasView;
        }

        public override void InterstitialDidLoadAd(MPInterstitialAdController interstitial)
        {
            interstitial.ShowFromViewController(brincadeirasView);
        }
    }
}