using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BRFX.Core.IOS;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Touch.Views;

using CoreGraphics;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Model;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public class CrescimentosTableViewSource : MvxTableViewSource
	{
		#region Fields

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("CrescimentoTableViewCell");

		//private readonly UIImage _charGreen;

		//private readonly UIImage _charRed;

		#endregion

		#region Constructors and Destructors

		public CrescimentosTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Format('{0:dd/MM/yyyy}', DataCriacao)");

			//_charRed = UIImage.FromFile("vermelhochar");
			//_charGreen = UIImage.FromFile("verdechar");
		}

		public CrescimentosTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("CrescimentosTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		#endregion

		#region Public Properties

		public ObservableCollection<Crescimento> Crescimentos
		{
			get
			{
				return (ObservableCollection<Crescimento>)ItemsSource;
			}
		}

		#endregion

		#region Public Methods and Operators

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			if(CrescimentoHelpers.PrecisaAtualizar())
			{
				return 83;
			}

			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 83;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var view = new UIView(new CGRect(0, 0, 320, GetHeightForHeader(tableView, section)))
							{
								BackgroundColor = UIColor.White
							};

			view.AddSubview(new UIView(new CGRect(0, 0, 320, 1))
								{
									BackgroundColor = UIColor.FromRGB(224, 224, 224)
								});

			if(CrescimentoHelpers.PrecisaAtualizar())
			{
				var labelHoje = new UILabel(new CGRect(30, 7, 290, 40))
									{
										Text = "ÚLTIMA MEDIÇÃO",
										TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f),
										Font = UIFont.SystemFontOfSize(14)
									};
				view.Add(labelHoje);

				var corAzul = UIColor.FromRGB(86, 126, 214);

				var labelAzul = new UILabel(new CGRect(30, 45, 43, 21))
									{
										Text = "?",
										TextAlignment = UITextAlignment.Center,
										BackgroundColor = corAzul,
										TextColor = UIColor.White,
										ClipsToBounds = true
									};
				labelAzul.Layer.CornerRadius = 4;
				view.Add(labelAzul);

				var labelAtualize = new UILabel(new CGRect(30 + 51, 45, 240, 21))
										{
											Text = "atualize o peso e altura",
											TextColor = corAzul,
											Font = UIFont.SystemFontOfSize(16)
										};
				view.Add(labelAtualize);

				view.Add(new UIView(new CGRect(30, 82, 290, 1))
				{
					BackgroundColor = UIColor.FromRGB(224, 224, 224)
				});

				var button = new UIButton(UIButtonType.Custom)
								{
									Frame = new CGRect(0, 0, 320, 83)
								};
				var color = new UIColor(0, 0, 0, 0.5f);
				button.SetBackgroundImage(color.ToImage(), UIControlState.Highlighted);
				button.TouchUpInside += (sender, args) =>
					{
						if(SelectionChangedCommand != null && SelectionChangedCommand.CanExecute(null))
						{
							SelectionChangedCommand.Execute(null);
						}
					};
				view.Add(button);
			}

			return view;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
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
                Text = "O gráfico acima não substitui acompanhamento de um profissional de saúde. " + NSBundle.MainBundle.LocalizedString("Disclaimer", null).Replace("*",""),
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0
            };

            view.AddSubview(disclaimerLabel);
            return view;
        }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);

			tableView.DeselectRow(indexPath, true);
		}

		#endregion

		#region Methods

		protected CrescimentoTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath, Crescimento item)
		{
			var cell = new CrescimentoTableViewCell(_bindingDescriptions, UITableViewCellStyle.Subtitle, _cellIdentifier, this, item);

			return cell;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var crescimento = (Crescimento)item;

			var reuse = tableView.DequeueReusableCell(_cellIdentifier);
			if(reuse != null)
			{
				((CrescimentoTableViewCell)reuse).Update(this, crescimento);
				return reuse;
			}

			return CreateDefaultBindableCell(tableView, indexPath, crescimento);
		}

		#endregion

		public class CrescimentoTableViewCell : MvxStandardTableViewCell
		{
			#region Fields

			private UIView _border;

			private UIImageView _imageView;

			#endregion

			#region Constructors and Destructors

			public CrescimentoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public CrescimentoTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public CrescimentoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public CrescimentoTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public CrescimentoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, CrescimentosTableViewSource viewSource, Crescimento crescimento)
				: base(bindingDescriptions, cellStyle, cellIdentifier, UITableViewCellAccessory.None)
			{
				InitializeSubViews();
				Update(viewSource, crescimento);
			}

			#endregion

			#region Public Properties

			public UILabel DifLabel { get; private set; }

			public UIImage Image { get; set; }

			#endregion

			#region Public Methods and Operators

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(30, TextLabel.Frame.Top, 290, TextLabel.Frame.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;

				nfloat posDetails;
				if(DifLabel.Hidden)
				{
					posDetails = DifLabel.Frame.Left - 5;
				}
				else
				{
					DifLabel.SizeToFit();
					var frame = DifLabel.Frame;
					frame.Width += 10;
					frame.Height = 20;
					DifLabel.Frame = frame;

					posDetails = DifLabel.Frame.Right;
				}

				posDetails += 8;

				DetailTextLabel.Frame = new CGRect(posDetails, 38, 320 - posDetails, 31.5f);
				DetailTextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;

				_imageView.Image = Image;
			}

			public void Update(CrescimentosTableViewSource viewSource, Crescimento crescimento)
			{
				UIColor fontColor;
				UIColor backgroundColor;
				//UIImage image;

				if(crescimento.TipoCrescimento == TipoCrescimento.Normal)
				{
					// GREEN
					fontColor = UIColor.FromRGB(40, 187, 6);
					backgroundColor = UIColor.FromRGB(107, 211, 0);
					//image = viewSource._charGreen;
				}
				else
				{
					// RED
					fontColor = UIColor.FromRGB(253, 75, 65);
					backgroundColor = UIColor.FromRGB(254, 76, 66);
					//image = viewSource._charRed;
				}

				DetailTextLabel.TextColor = fontColor;
				var strDetails = string.Format("{0:0.#}kg / {1:0.##}m", crescimento.Peso, crescimento.Altura);

				var attrs = NSDictionary.FromObjectAndKey(UIFont.BoldSystemFontOfSize(26), UIStringAttributeKey.Font);
				var attributedText = new NSMutableAttributedString(strDetails, attrs);
				var regularFont = UIFont.SystemFontOfSize(25);
				attributedText.SetAttributes(NSDictionary.FromObjectAndKey(regularFont, UIStringAttributeKey.Font),
					new NSRange(strDetails.Length - 1, 1));
				attributedText.SetAttributes(NSDictionary.FromObjectAndKey(regularFont, UIStringAttributeKey.Font),
					new NSRange(strDetails.IndexOf("kg / "), 5));
				DetailTextLabel.AttributedText = attributedText;

				//Image = image;

				var index = viewSource.Crescimentos.IndexOf(crescimento);

				if(index >= viewSource.Crescimentos.Count - 1)
				{
					DifLabel.Hidden = true;
				}
				else
				{
					DifLabel.Hidden = false;

					DifLabel.BackgroundColor = backgroundColor;

					var previousCrescimento = viewSource.Crescimentos.ElementAt(index + 1);

					var strDif = string.Format("{0:+0.#;-0.#;+0}kg", crescimento.Peso - previousCrescimento.Peso);

					attrs = NSDictionary.FromObjectAndKey(UIFont.BoldSystemFontOfSize(14), UIStringAttributeKey.Font);
					attributedText = new NSMutableAttributedString(strDif, attrs);
					attributedText.SetAttributes(NSDictionary.FromObjectAndKey(UIFont.SystemFontOfSize(13),
						UIStringAttributeKey.Font),
						new NSRange(strDif.Length - 2, 2));

					DifLabel.AttributedText = attributedText;
				}
			}

			#endregion

			#region Methods

			private void InitializeSubViews()
			{
				_imageView = new UIImageView(new CGRect(30, 43, 20, 20));
				AddSubview(_imageView);

				DifLabel = new UILabel(new CGRect(55, 43, 60, 20))
								{
									TextColor = UIColor.White,
									ClipsToBounds = true,
									TextAlignment = UITextAlignment.Center
								};
				DifLabel.Layer.CornerRadius = 4;
				AddSubview(DifLabel);

                _border = new UIView(new CGRect(0, 82, 320, 1))
                {
                    BackgroundColor = UIColor.FromRGB(224, 224, 224)
                };
                AddSubview(_border);

                TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
				TextLabel.Font = UIFont.SystemFontOfSize(14);

				BackgroundColor = UIColor.White;

				Editing = true;
				ShouldIndentWhileEditing = true;
			}

            //public override void SetHighlighted(bool highlighted, bool animated)
            //{
            //    var backgroundColor = _border.BackgroundColor;
            //    base.SetHighlighted(highlighted, animated);
            //    _border.BackgroundColor = backgroundColor;
            //}

            //public override void SetSelected(bool selected, bool animated)
            //{
            //    var backgroundColor = _border.BackgroundColor;
            //    base.SetSelected(selected, animated);
            //    _border.BackgroundColor = backgroundColor;
            //}

            #endregion
        }

	}

}
