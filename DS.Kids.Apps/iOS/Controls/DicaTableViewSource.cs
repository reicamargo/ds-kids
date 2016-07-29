using System;
using System.Collections.Generic;
using System.Linq;

using BRFX.Core.IOS.Controls;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Plugins.Color.Touch;

using CoreGraphics;

using DS.Kids.Apps.Core.Converters;
using DS.Kids.Model;
using DS.Kids.Model.Communication;

using Foundation;

using MediaPlayer;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public sealed class DicaTableViewSource : MvxBaseTableViewSource
	{
		#region Fields

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly IEnumerable<MvxBindingDescription> _bindingImageDescriptions;

		private readonly IEnumerable<MvxBindingDescription> _bindingVideoDescriptions;

		private readonly NSString _cellIdentifier = new NSString("DicaTableViewCell");

		private readonly NSString _cellImageIdentifier = new NSString("DicaImagemTableViewCell");

		private readonly NSString _cellVideoIdentifier = new NSString("DicaVideoTableViewCell");

		private Dica _dica;

		#endregion

		#region Constructors and Destructors

		public DicaTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Texto");
			_bindingImageDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("Image RelativeToAbsoluteUrl(UrlImagem)");
			_bindingVideoDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("Video Video");
			TableView.SectionHeaderHeight = 0.0f;
			TableView.SectionFooterHeight = 0.0f;
		}

		#endregion

		#region Public Properties

		public Dica Dica
		{
			get
			{
				return _dica;
			}
			set
			{
				_dica = value;
				if(_dica != null)
				{
					ReloadTableData();
				}
			}
		}

		#endregion

		#region Public Methods and Operators

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			return 60;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
            return 60;
			//if(Dica == null)
			//{
			//	return 0;
			//}

			//var cellFont = UIFont.FromName("Hangyaboly", 32);
			//var constraintSize = new CGSize(262, float.MaxValue);
			//var labelSize = Dica.Titulo.StringSize(cellFont, constraintSize, UILineBreakMode.WordWrap);

			//const int upperViewHeight = 80;
			//const int spacing = 30;
			//return labelSize.Height + upperViewHeight + spacing * 2;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var paragrafo = GetItemAt(indexPath) as Paragrafo;
			if(paragrafo != null)
			{
				switch(paragrafo.TipoParagrafo)
				{
					case TipoParagrafo.Imagem:
					case TipoParagrafo.Video:
						return 215;
					case TipoParagrafo.Texto:
						var cellFont = UIFont.SystemFontOfSize(15);
						var constraintSize = new CGSize(262, float.MaxValue);
						var labelSize = paragrafo.Texto.StringSize(cellFont, constraintSize, UILineBreakMode.WordWrap);

						return labelSize.Height + 29;
				}
			}

			return 0;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
            //if(Dica == null)
            //{
            //	return null;
            //}

            //var view = new UIView(new CGRect(0, 0, 320, GetHeightForHeader(tableView, section)));

            //var color = CategoriasColorConverter.Convert(Dica.Categoria).ToNativeColor();

            //var upperView = new UIView(new CGRect(0, 0, 320, 80))
            //					{
            //						BackgroundColor = color
            //					};
            //upperView.AddSubview(new MvxImageView
            //						{
            //							Frame = new CGRect(122, 16, 77, 48),
            //							ContentMode = UIViewContentMode.ScaleToFill,
            //							ImageUrl = Endpoints.BASE + Dica.Categoria.UrlImagem
            //						});

            //var label = new BRFXLabel
            //				{
            //					CustomFont = "Hangyaboly",
            //					CustomFontSize = 32,
            //					Text = Dica.Titulo,
            //					TextAlignment = UITextAlignment.Center,
            //					TextColor = color,
            //					LineBreakMode = UILineBreakMode.WordWrap,
            //					Lines = 0,
            //					Frame = new CGRect(29, upperView.Frame.Height, 262, view.Frame.Height - upperView.Frame.Height)
            //				};
            //label.AwakeFromNib();
            //upperView.AddSubview(label);

            //view.AddSubview(upperView);

            System.Drawing.SizeF size = new System.Drawing.SizeF(320, 50);
            MoPubSDK.MPAdView _mpAdView = new MoPubSDK.MPAdView(Ads.MoPubconfig.AD_UNIT_ID_BANNER, size);

            _mpAdView.Frame = new System.Drawing.RectangleF(0, 0, 320, 50);
            _mpAdView.LoadAd();
            
            return _mpAdView;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

        public override UIView GetViewForFooter(UITableView tableView, nint section)
        {
            var view = new UIView(new CGRect(0, 0, 320, 1))
            {
                BackgroundColor = UIColor.White
            };

            var disclaimerLabel = new UILabel
            {
                Frame = new CGRect(5, 5, 310, 30),
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.FromRGB(102, 102, 102),
                Font = UIFont.BoldSystemFontOfSize(9),
                Text = NSBundle.MainBundle.LocalizedString("Disclaimer", null),
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };

            view.AddSubview(disclaimerLabel);
            return view;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if(Dica == null)
			{
				return 0;
			}

			return Dica.Paragrafos.Count;
		}

		#endregion

		#region Methods

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			return Dica.Paragrafos.ElementAt(indexPath.Row);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var paragrafo = item as Paragrafo;
			if(paragrafo != null)
			{
				switch(paragrafo.TipoParagrafo)
				{
					case TipoParagrafo.Video:
						var reuseVideo = tableView.DequeueReusableCell(_cellVideoIdentifier) as DicaVideoTableViewCell;
						if(reuseVideo != null)
						{
							return reuseVideo;
						}

						var cellVideo = new DicaVideoTableViewCell(_bindingVideoDescriptions, UITableViewCellStyle.Default, _cellVideoIdentifier);

						return cellVideo;
					case TipoParagrafo.Imagem:
						var reuseImage = tableView.DequeueReusableCell(_cellImageIdentifier) as DicaImagemTableViewCell;
						if(reuseImage != null)
						{
							return reuseImage;
						}

						var cellImage = new DicaImagemTableViewCell(_bindingImageDescriptions, UITableViewCellStyle.Default, _cellImageIdentifier);

						return cellImage;
					case TipoParagrafo.Texto:
						var dicaTableViewCell = tableView.DequeueReusableCell(_cellIdentifier) as DicaTableViewCell;
						if(dicaTableViewCell != null)
						{
							return dicaTableViewCell;
						}

						return CreateDefaultBindableCell(tableView, indexPath, item);
				}
			}
			return null;
		}

		private DicaTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = new DicaTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellIdentifier);

			return cell;
		}

		#endregion

		public sealed class DicaImagemTableViewCell : MvxStandardTableViewCell
		{
			#region Fields

			private readonly MvxImageView _imageView;

			#endregion

			#region Constructors and Destructors

			public DicaImagemTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public DicaImagemTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				BackgroundColor = UIColor.White;

				_imageView = new MvxImageView(new CGRect(29, 0, 262, 186), () =>
					{
						if(_imageView != null && _imageView.Image != null)
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
									ClipsToBounds = true,
									DefaultImagePath = "res:carregando",
									ErrorImagePath = "res:erro"
								};

				ContentView.AddSubview(_imageView);
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

			#endregion
		}

		public sealed class DicaTableViewCell : MvxStandardTableViewCell
		{
			#region Constructors and Destructors

			public DicaTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public DicaTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier)
				: base(bindingDescriptions, cellStyle, cellIdentifier, UITableViewCellAccessory.None)
			{
				TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
				TextLabel.TextAlignment = UITextAlignment.Left;
				TextLabel.Font = UIFont.SystemFontOfSize(15);
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				TextLabel.Lines = 0;

				BackgroundColor = UIColor.White;
			}

			#endregion

			#region Public Methods and Operators

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				var cellFont = TextLabel.Font;
				var constraintSize = new CGSize(262, float.MaxValue);
				var labelSize = TextLabel.Text.StringSize(cellFont, constraintSize, UILineBreakMode.WordWrap);

				TextLabel.Frame = new CGRect(29, 0, 262, labelSize.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;
			}

			#endregion
		}

		public sealed class DicaVideoTableViewCell : MvxStandardTableViewCell
		{
			#region Fields

			private readonly MPMoviePlayerController _moviePlayerController;

			#endregion

			#region Constructors and Destructors

			public DicaVideoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public DicaVideoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				BackgroundColor = UIColor.White;

				_moviePlayerController = new MPMoviePlayerController();
				_moviePlayerController.View.Frame = new CGRect(29, 0, 262, 186);

				ContentView.AddSubview(_moviePlayerController.View);

				var playButton = new UIButton(UIButtonType.Custom)
									{
										Frame = _moviePlayerController.View.Frame
									};
				playButton.TouchUpInside += PlayButtonOnTouchUpInside;

				ContentView.AddSubview(playButton);
			}

			private void PlayButtonOnTouchUpInside(object sender, EventArgs eventArgs)
			{
				_moviePlayerController.SetFullscreen(true, true);
				_moviePlayerController.Play();
			}

			#endregion

			#region Public Properties

			public string Video
			{
				get
				{
					return _moviePlayerController.ContentUrl.AbsoluteString;
				}
				set
				{
					_moviePlayerController.ContentUrl = string.IsNullOrEmpty(value) ? null : new NSUrl(value);
				}
			}

			#endregion
		}

	}

}
