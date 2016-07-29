using System;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class CategoriasView : BaseHomeChildView
	{
		#region Constructors and Destructors

		public CategoriasView(IntPtr handle)
			: base(handle)
		{
			this.OnViewCreate();
		}

		#endregion

		#region Public Methods and Operators

		public override sealed void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationItem.BackBarButtonItem = new UIBarButtonItem
			{
				Title = "Categorias"
			};

            System.Drawing.SizeF size = new System.Drawing.SizeF(320, 50);
            MoPubSDK.MPAdView _mpAdView = new MoPubSDK.MPAdView(Ads.MoPubconfig.AD_UNIT_ID_BANNER, size);

            _mpAdView.Frame = new System.Drawing.RectangleF(0, -60, 320, 50);
            _mpAdView.LoadAd();
            collectionView.AddSubview(_mpAdView);

            collectionView.ContentInset = new UIKit.UIEdgeInsets(60, 0, 0, 0);

            var source = new CategoriasCollectionViewSource(collectionView);
			collectionView.Source = source;
			collectionView.Delegate = new CategoriasCollectionViewDelegateFlowLayout(source);

			var bindingSet = this.CreateBindingSet<CategoriasView, CategoriasViewModel>();

			bindingSet.Bind(source).To(vm => vm.Categorias);
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.CategoriaSelectedCommand);
			bindingSet.Apply();

			collectionView.ReloadData();
		}

		#endregion
	}

}
