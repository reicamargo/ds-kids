using System;

using BRFX.Core.IOS.Views;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Views;
using Cirrious.MvvmCross.Plugins.Messenger;

using CoreGraphics;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model.Communication;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	public class BaseHomeChildView : ProgressView
	{

		#region Fields

		private ImageViewLoader _imageHelper;

		private UIButton _backButton;

		private UIBarButtonItem _backBarButtonItem;

		private MvxSubscriptionToken _currentCriancaChangedToken;

		#endregion

		#region Constructors and Destructors

		public BaseHomeChildView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void OnUnload()
		{
			base.OnUnload();

			if(_imageHelper != null)
			{
				_imageHelper.Dispose();
				_imageHelper = null;

				_backButton = null;
				_backBarButtonItem = null;
			}

			if(_currentCriancaChangedToken != null)
			{
				var messenger = Mvx.Resolve<IMvxMessenger>();
				messenger.Unsubscribe<CurrentCriancaChangedMessage>(_currentCriancaChangedToken);
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_backButton = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(0, 0, 34, 34),
				ShowsTouchWhenHighlighted = true,
				ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 0)
			};
			_backButton.SetBackgroundImage(UIImage.FromBundle("avatar-default"), UIControlState.Normal);

			_imageHelper = new ImageViewLoader(image =>
			{
				if(image != null && image.RenderingMode != UIImageRenderingMode.AlwaysOriginal)
				{
					_backButton.Layer.CornerRadius = 17;
					_backButton.Layer.BorderColor = UIColor.White.CGColor;
					_backButton.Layer.BorderWidth = 1;
					_backButton.Layer.MasksToBounds = true;
					_backButton.ClipsToBounds = true;
					_backButton.SetImage(image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
					_backBarButtonItem = new UIBarButtonItem(_backButton);
				}
				else
				{
					_backButton.SetImage(null, UIControlState.Normal);
				}
			});

			_backBarButtonItem = new UIBarButtonItem(_backButton);

			var messenger = Mvx.Resolve<IMvxMessenger>();
			_currentCriancaChangedToken = messenger.Subscribe<CurrentCriancaChangedMessage>(ReceiveCurrentCriancaChangedMessage);

			ReceiveCurrentCriancaChangedMessage(null);

			var bindingSet = this.CreateBindingSet<BaseHomeChildView, BaseHomeChildViewModel>();

			bindingSet.Bind(_backButton).To(vm => vm.ConfiguracoesCommand);

			bindingSet.Apply();
		}

		private void ReceiveCurrentCriancaChangedMessage(CurrentCriancaChangedMessage obj)
		{
			if(LoginHelper.CurrentCrianca != null)
			{
				if(string.IsNullOrEmpty(LoginHelper.CurrentCrianca.NomeImagem))
				{
					_imageHelper.ImageUrl = null;
				}
				else
				{
					_imageHelper.ImageUrl = Endpoints.BASE + LoginHelper.CurrentCrianca.UrlImagem;
				}
			}

			NavigationItem.SetRightBarButtonItem(_backBarButtonItem, true);
		}

		#endregion

		public class ImageViewLoader : MvxBaseImageViewLoader<UIImage>
		{
			#region Constructors and Destructors

			public ImageViewLoader(Action<UIImage> afterImageChangeAction = null)
				: base(afterImageChangeAction)
			{
			}

			#endregion
		}

	}

}
