using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

using CoreAnimation;

using CoreGraphics;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public sealed class BrincadeirasCollectionViewSource : MvxCollectionViewSource
	{
		#region Fields

		#endregion

		#region Constructors and Destructors

		public BrincadeirasCollectionViewSource(UICollectionView collectionView)
			: base(collectionView, new NSString("BrincadeiraCollectionViewCell"))
		{
			CollectionView.RegisterClassForCell(typeof(BrincadeiraCollectionViewCell), DefaultCellIdentifier);
		}

		#endregion

		public sealed class BrincadeiraCollectionViewCell : MvxCollectionViewCell
		{
			#region Fields

			private readonly MvxImageView _imageView;

			private readonly UILabel _nomeLabel;

			#endregion

			#region Constructors and Destructors

			[Export("initWithFrame:")]
			public BrincadeiraCollectionViewCell(System.Drawing.RectangleF frame)
				: base(frame)
			{
				this.CreateBindingContext("NomeText Titulo; Image RelativeToAbsoluteUrl(UrlImagem)");

				_imageView = new MvxImageView(new CGRect(0, 0, 158, 158), () =>
					{
						if(_imageView.Image != null)
						{
							var imageSize = _imageView.Image.Size;
							_imageView.SizeThatFits(imageSize);
							var imageViewCenter = _imageView.Center;
							imageViewCenter.X = ContentView.Frame.GetMidX();
							_imageView.Center = imageViewCenter;
						}
					})
								{
									ContentMode = UIViewContentMode.ScaleAspectFill,
									ClipsToBounds = true
								};

				ContentView.AddSubview(_imageView);

				var gradientView = new UIView(new CGRect(0, 118, 158, 40));
				var gradient = (CAGradientLayer)CAGradientLayer.Create();
				gradient.Frame = gradientView.Bounds;
				gradient.Colors = new[]
									{
										UIColor.Clear.CGColor,
										UIColor.FromRGBA(0, 0, 0, 0.8f).CGColor
									};
				gradientView.Layer.InsertSublayer(gradient, 0);
				ContentView.AddSubview(gradientView);

				_nomeLabel = new UILabel(new CGRect(0, 132, 158, 20))
				{
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.White,
					Font = UIFont.SystemFontOfSize(15)
				};
				ContentView.AddSubview(_nomeLabel);
			}

			#endregion

			#region Public Properties

			public string Image
			{
				get
				{
					return _imageView.ImageUrl;
				}
				set
				{
					_imageView.ImageUrl = value;
				}
			}

			public string NomeText
			{
				get
				{
					return _nomeLabel.Text;
				}
				set
				{
					_nomeLabel.Text = value;
				}
			}

			#endregion
		}

	}

}