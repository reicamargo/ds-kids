using System;

using BRFX.Core.IOS.Controls;
using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

using CoreAnimation;

using CoreGraphics;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class BrincadeiraView : BaseView
	{
		#region Constructors and Destructors

		private UIView _headerContainer;

		private MvxImageView _imageView;

		public BrincadeiraView(IntPtr handle)
			: base(handle)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<BrincadeiraView, BrincadeiraViewModel>();

			var source = new BrincadeiraTableViewSource(tableView);
			tableView.Source = source;
			source.ScrollChanged += SourceOnScrollChanged;

			_imageView = new MvxImageView(new CGRect(0, 0, 320, 200), () =>
				{
					if(_imageView.Image != null)
					{
						var imageSize = _imageView.Image.Size;
						_imageView.SizeThatFits(imageSize);
						var imageViewCenter = _imageView.Center;
						imageViewCenter.X = View.Frame.GetMidX();
						_imageView.Center = imageViewCenter;
					}
				})
							{
								UserInteractionEnabled = false
							};

			_headerContainer = new UIView(new CGRect(0, 0, 320, 200))
									{
										UserInteractionEnabled = false
									};
			_headerContainer.AddSubview(_imageView);

			var gradientView = new UIView(new CGRect(0, 115, 320, 85))
									{
										UserInteractionEnabled = false
									};
			var gradient = (CAGradientLayer)CAGradientLayer.Create();
			gradient.Frame = gradientView.Bounds;
			gradient.Colors = new[]
									{
										UIColor.Clear.CGColor,
										UIColor.FromRGBA(0, 0, 0, 0.8f).CGColor
									};
			gradientView.Layer.InsertSublayer(gradient, 0);
			_headerContainer.AddSubview(gradientView);

			var label = new BRFXLabel
			{
				CustomFont = "Hangyaboly",
				CustomFontSize = 36,
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.White,
				Frame = new CGRect(0, 115, 320, 85),
				UserInteractionEnabled = false
			};
			label.AwakeFromNib();
			_headerContainer.AddSubview(label);

			View.AddSubview(_headerContainer);

			bindingSet.Bind(source).For(v => v.Brincadeira).To(vm => vm.Brincadeira);
			bindingSet.Bind(_imageView).For(v => v.ImageUrl).To(vm => vm.Brincadeira.UrlImagem).WithConversion("RelativeToAbsoluteUrl");
			bindingSet.Bind(label).To(vm => vm.Brincadeira.Titulo);

			bindingSet.Apply();

			tableView.ReloadData();
		}

		private void SourceOnScrollChanged(object sender, EventArgs eventArgs)
		{
			var frame = _headerContainer.Frame;
			frame.Y = -tableView.ContentOffset.Y - 85;
			if(frame.Y < -115)
			{
				frame.Y = -115;
			}
			_headerContainer.Frame = frame;
		}

		#endregion
	}

}
