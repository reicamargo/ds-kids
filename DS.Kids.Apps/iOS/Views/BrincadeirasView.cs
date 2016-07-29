using System;
using System.Threading.Tasks;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

namespace DS.Kids.Apps.iOS.Views
{

	public partial class BrincadeirasView : BaseHomeChildView
	{
		#region Constructors and Destructors

		public BrincadeirasView(IntPtr handle)
			: base(handle)
		{
			this.OnViewCreate();
		}

		#endregion

		#region Public Methods and Operators

		public override sealed void ViewDidLoad()
		{
			base.ViewDidLoad();

            var interstial = new MoPubSDK.MPInterstitialAdController();
            interstial = MoPubSDK.MPInterstitialAdController.InterstitialAdControllerForAdUnitId(Ads.MoPubconfig.AD_UNIT_ID_FULLSCREEN);
            interstial.Delegate = new MopubInterstitialAdDelegate(this);
            interstial.LoadAd();

            System.Drawing.SizeF size = new System.Drawing.SizeF(320, 50);
            MoPubSDK.MPAdView _mpAdView = new MoPubSDK.MPAdView(Ads.MoPubconfig.AD_UNIT_ID_BANNER, size);

            _mpAdView.Frame = new System.Drawing.RectangleF(0, -60, 320, 50);
            _mpAdView.LoadAd();
            collectionView.AddSubview(_mpAdView);

            collectionView.ContentInset = new UIKit.UIEdgeInsets(60, 0, 0, 0);

            var source = new BrincadeirasCollectionViewSource(collectionView);
			collectionView.Source = source;

			var bindingSet = this.CreateBindingSet<BrincadeirasView, BrincadeirasViewModel>();

			bindingSet.Bind(source).To(vm => vm.Brincadeiras);
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.BrincadeiraSelectedCommand);
			bindingSet.Apply();

			collectionView.ReloadData();

			Task.Run(async () =>
				{
					await Task.Delay(100);
					InvokeOnMainThread(() =>
						{
							collectionView.ReloadData();
						});
				});
		}

		#endregion
	}

}
