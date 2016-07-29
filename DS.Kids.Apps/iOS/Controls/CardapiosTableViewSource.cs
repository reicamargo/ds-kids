using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Plugins.Color.Touch;

using CoreGraphics;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;

using Foundation;

using UIKit;
using DS.Kids.Model.Communication;

namespace DS.Kids.Apps.iOS.Controls
{

	public class CardapiosTableViewSource : MvxTableViewSource
	{

		public ICommand OutraSugestaoCommand { get; set; }

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("CardapioTableViewCell");

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);

			tableView.DeselectRow(indexPath, true);
		}

		public override bool ShouldHighlightRow(UITableView tableView, NSIndexPath rowIndexPath)
		{
			var refeicaoItem = GetItemAt(rowIndexPath) as RefeicaoItem;
			if(refeicaoItem != null && refeicaoItem.Alimento != null && refeicaoItem.Alimento.Dica != null)
			{
				return true;
			}

			return false;
		}

		public CardapiosTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Alimento.Nome; DetailText Format('{0:0.#} {1}', Quantidade, Medida.Nome)");
		}

		public CardapiosTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("CardapiosTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		public ObservableCollection<CardapiosViewModel.CardapioRefeicao> Cardapio
		{
			get
			{
				return (ObservableCollection<CardapiosViewModel.CardapioRefeicao>)ItemsSource;
			}
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if(ItemsSource == null)
			{
				return null;
			}

			return Cardapio[indexPath.Section][indexPath.Row];
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if(ItemsSource == null)
			{
				return 0;
			}

			return Cardapio[(int)section].Count;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if(ItemsSource == null)
			{
				return 0;
			}

			return ItemsSource.Count();
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var refeicaoItem = (RefeicaoItem)item;

			var reuse = tableView.DequeueReusableCell(_cellIdentifier) as RefeicaoTableViewCell;
			if(reuse != null)
			{
				reuse.Update(refeicaoItem);
				return reuse;
			}

			return CreateDefaultBindableCell(tableView, indexPath, refeicaoItem);
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var view = new UIView(new CGRect(0, 0, 320, 116))
							{
								BackgroundColor = UIColor.White
							};
			var uiImageView = new UIImageView(UIImage.FromBundle(string.Format("tit0{0}.png", section + 1)))
								{
									Frame = new CGRect(0, 0, 320, 109)
								};
			view.AddSubview(uiImageView);
			return view;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 116;
		}

		public override UIView GetViewForFooter(UITableView tableView, nint section)
		{
			var view = new UIView(new CGRect(0, 0, 320, GetHeightForFooter(tableView, section)))
							{
								BackgroundColor = UIColor.White
							};

			var cardapio = Cardapio[(int)section];

			int yBotao = 10;
			if(cardapio.Parceiro != null && string.IsNullOrEmpty(cardapio.Parceiro.Nome) == false)
			{
				var oferecidoLabel = new UILabel
				{
					Frame = new CGRect(50, 0, 140, 20),
					TextAlignment = UITextAlignment.Left,
					TextColor = cardapio.Color.ToNativeColor(),
					Font = UIFont.ItalicSystemFontOfSize(13),
					Text = "cardápio oferecido por"
				};                
				view.AddSubview(oferecidoLabel);

                view.AddSubview(new MvxImageView(new CGRect(190, -5, 60, 30))
                {
                    ImageUrl = Endpoints.BASE + cardapio.Parceiro.UrlIcone,
                    ContentMode = UIViewContentMode.ScaleAspectFill,
                    ClipsToBounds = true
                });


                yBotao += 22;
			}

			var outraSugestaoButton = new UIButton(UIButtonType.Custom)
										{
											Frame = new CGRect(30, yBotao, 260, 43)
										};
			outraSugestaoButton.SetBackgroundImage(UIImage.FromBundle(string.Format("bot0{0}.png", section + 1)), UIControlState.Normal);
			outraSugestaoButton.SetTitle("quero ver outra sugestão", UIControlState.Normal);
			outraSugestaoButton.TitleEdgeInsets = new UIEdgeInsets(0, 40, 0, 0);
			outraSugestaoButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			outraSugestaoButton.Font = UIFont.BoldSystemFontOfSize(15);
			outraSugestaoButton.TouchUpInside += (sender, args) =>
				{
					if (OutraSugestaoCommand != null && OutraSugestaoCommand.CanExecute((int)section + 1))
					{
						OutraSugestaoCommand.Execute((int)section + 1);
					}
				};

			view.AddSubview(outraSugestaoButton);

			// Se for a última
			if(section < NumberOfSections(tableView) - 1)
			{
				view.AddSubview(new UIView(new CGRect(0, 84, 320, 20))
				{
					BackgroundColor = UIColor.FromRGB(238, 237, 243)
				});
				view.AddLine(84);
			}
			else
			{
				var infoLabel = new UILabel
				{
					Frame = new CGRect(30, 74, 260, 38),
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.FromRGB(102, 102, 102),
					Font = UIFont.SystemFontOfSize(11),
					Text = "Os cardápios são qualitativos e não têm o objetivo de sugerir uma dieta específica.",
					LineBreakMode = UILineBreakMode.WordWrap,
					Lines = 0
				};
				view.AddSubview(infoLabel);

                var disclaimerLabel = new UILabel
                {
                    Frame = new CGRect(5, 120, 300, 38),
                    TextAlignment = UITextAlignment.Center,
                    TextColor = UIColor.FromRGB(102, 102, 102),
                    Font = UIFont.BoldSystemFontOfSize(9),
                    Text = NSBundle.MainBundle.LocalizedString("Disclaimer", null),
                    LineBreakMode = UILineBreakMode.WordWrap,
                    Lines = 0
                };
                view.AddSubview(disclaimerLabel);
            }

			return view;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			// Se for a última
			if(section == NumberOfSections(tableView) - 1)
			{
				return 160;
			}

			return 104;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var refeicaoItem = GetItemAt(indexPath) as RefeicaoItem;

			if(refeicaoItem != null && refeicaoItem.Alimento != null)
			{
				var temDica = refeicaoItem.Alimento.Dica != null;

				var width = 290;
				if(temDica)
				{
					width = 220;
				}

				return HeightForItem(width, refeicaoItem.Alimento.Nome);
			}

			return 62;
		}

		public static float HeightForItem(int width, string nome)
		{
			UIFont cellFont = UIFont.SystemFontOfSize(17);
			CGSize constraintSize = new CGSize(width, float.MaxValue);
			CGSize labelSize = nome.StringSize(cellFont, constraintSize, UILineBreakMode.WordWrap);

			return (float)Math.Max(62, labelSize.Height + 42);
		}

		protected RefeicaoTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath, RefeicaoItem item)
		{
			var cell = new RefeicaoTableViewCell(_bindingDescriptions, UITableViewCellStyle.Subtitle, _cellIdentifier, item);

			cell.TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
			cell.DetailTextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
			cell.DetailTextLabel.Font = UIFont.BoldSystemFontOfSize(13);

			cell.BackgroundColor = UIColor.White;

			return cell;
		}

		public class RefeicaoTableViewCell : MvxStandardTableViewCell
		{

			private bool _temDica;

			private UIImageView _imageView;

			public RefeicaoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public RefeicaoTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public RefeicaoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public RefeicaoTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public RefeicaoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, RefeicaoItem refeicaoItem, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				InitializeSubViews();
				Update(refeicaoItem);
			}

			private void InitializeSubViews()
			{
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				TextLabel.Lines = 0;

				var image = UIImage.FromBundle("ico_star");

				_imageView = new UIImageView(new CGRect(259, 6, 33, 30))
								{
									Image = image,
									ContentMode = UIViewContentMode.Center,
									ClipsToBounds = true
								};

				AddSubview(_imageView);
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				var width = 290;

				if(_temDica)
				{
					width = 220;
				}

				var height = HeightForItem(width, TextLabel.Text);

				TextLabel.Frame = new CGRect(30, 10, width, height - 42);
				//TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;

				DetailTextLabel.Frame = new CGRect(30, TextLabel.Frame.Height + 7, width, 22);
				//DetailTextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;

				var frame = _imageView.Frame;
				frame.Y = (TextLabel.Frame.Top + DetailTextLabel.Frame.Bottom - frame.Height) / 2;
				_imageView.Frame = frame;
			}

			public void Update(RefeicaoItem refeicaoItem)
			{
				_temDica = refeicaoItem != null && refeicaoItem.Alimento != null && refeicaoItem.Alimento.Dica != null;

				_imageView.Hidden = _temDica == false;
			}

			public new string TitleText
			{
				get
				{
					return TextLabel.AttributedText.Value;
				}
				set
				{
					if(string.IsNullOrEmpty(value) == false)
					{
						var attrString = new NSMutableAttributedString(value);
						var style = new NSMutableParagraphStyle
										{
											LineHeightMultiple = 0.9f,
											LineBreakMode = UILineBreakMode.WordWrap
										};
						attrString.SetAttributes(NSDictionary.FromObjectAndKey(style, UIStringAttributeKey.ParagraphStyle),
							new NSRange(0, value.Length));
						TextLabel.AttributedText = attrString;
					}
				}
			}

		}
	}

}
