using System;
using System.Collections.Generic;

using BRFX.Core.IOS.Controls;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;

using CoreGraphics;

using DS.Kids.Model;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public sealed class BrincadeiraTableViewSource : MvxBaseTableViewSource
	{
		#region Constructors and Destructors

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("BrincadeiraTableViewCell");
		private readonly NSString _cellInstrucoesIdentifier = new NSString("BrincadeiraInstrucaoTableViewCell");

		private Brincadeira _brincadeira;

		private static readonly UIColor _textYellowColor = UIColor.FromRGB(255, 139, 23);

		private static readonly UIColor _textBlueColor = UIColor.FromRGB(69, 101, 204);

		public Brincadeira Brincadeira
		{
			get { return _brincadeira; }
			set
			{
				_brincadeira = value;
				ReloadTableData();
			}
		}

		public event EventHandler ScrollChanged;

		public override void Scrolled(UIScrollView scrollView)
		{
			OnScrollChanged();
		}

		public BrincadeiraTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText .");
			TableView.SectionHeaderHeight = 0.0f;
			TableView.SectionFooterHeight = 0.0f;
		}

		#endregion

		#region Methods

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			switch(section)
			{
				case 0:
					return 115;
				case 1:
					return 83;
				case 2:
				case 3:
				case 4:
					if(RowsInSection(tableView, section) > 0)
					{
						return 55;
					}
					return 0;
			}

			return 0;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			switch (section)
			{
				case 2:
				case 3:
					if(RowsInSection(tableView, section) > 0)
					{
						return 15;
					}
					return 0;
			}

			return 0;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var view = new UIView(new CGRect(0, 0, 320, GetHeightForHeader(tableView, section)));
			switch(section)
			{
				case 0:
					view.BackgroundColor = UIColor.White;
					break;
				case 1:
					view.AddSubview(new UIImageView(UIImage.FromBundle("ico1"))
										{
											Frame = new CGRect(19, 20, 27, 41)
										});
					view.AddSubview(new UIImageView(UIImage.FromBundle("ico2"))
										{
											Frame = new CGRect(176, 21, 41, 40)
										});
					view.AddSubview(new UIImageView(UIImage.FromBundle("separador"))
										{
											Frame = new CGRect(159, 5, 1, 73)
										});
					var color = UIColor.FromRGB(228, 52, 75);
					AddHeaderLabel(55, color, "Idade ", Brincadeira.FaixaEtaria, view);
					string ambiente;
					switch(Brincadeira.Ambiente)
					{
						case TipoAmbiente.Externo:
							ambiente = "Externo";
							break;
						case TipoAmbiente.Interno:
							ambiente = "Interno";
							break;
						//case TipoAmbiente.InternoOuExterno:
						default:
							ambiente = "Interno ou Externo";
							break;
					}
					AddHeaderLabel(225, color, "Ambiente ", ambiente, view);
					break;
				case 2:
					AddLabelHeader(view, "Material necessário", _textYellowColor, section);
					break;
				case 3:
					AddLabelHeader(view, "Objetivos", _textBlueColor, section);
					break;
				case 4:
					AddLabelHeader(view, "Instruções", UIColor.FromRGB(73, 160, 90), section);
					break;
			}

			return view;
		}

		private static void AddHeaderLabel(int x, UIColor color, string textBold, string text, UIView view)
		{
			var label = new UILabel(new CGRect(x, 0, 95, 83))
							{
								TextColor = color,
								LineBreakMode = UILineBreakMode.WordWrap,
								Lines = 0
							};

			var str = textBold + text;

			var attrs = NSDictionary.FromObjectAndKey(UIFont.SystemFontOfSize(15), UIStringAttributeKey.Font);
			var attributedText = new NSMutableAttributedString(str, attrs);
			var boldFont = UIFont.BoldSystemFontOfSize(15);
			attributedText.SetAttributes(NSDictionary.FromObjectAndKey(boldFont, UIStringAttributeKey.Font),
				new NSRange(0, textBold.Length));
			label.AttributedText = attributedText;

			view.AddSubview(label);
		}

		private void AddLabelHeader(UIView hostView, string text, UIColor textColor, nint section)
		{
			var label = new BRFXLabel
							{
								CustomFont = "Hangyaboly",
								CustomFontSize = 32,
								Text = text,
								TextAlignment = UITextAlignment.Center,
								TextColor = textColor,
								Frame = new CGRect(0, 0, 320, GetHeightForHeader(null, section))
							};
			label.AwakeFromNib();
			hostView.AddSubview(label);

			var divider = new UIView(new CGRect(30, 0, 290, 1))
			{
				BackgroundColor = UIColor.FromRGB(224, 224, 224)
			};
			hostView.AddSubview(divider);
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			UIFont cellFont = UIFont.SystemFontOfSize(15);
			string cellText = string.Empty;

			var item = GetItemAt(indexPath);
			if(item != null)
			{
				cellText = item.ToString();
			}

			switch (indexPath.Section)
			{
				case 2:
				case 3:
					var attachment = new NSTextAttachment
					{
						Image = UIImage.FromBundle("visto")
					};

					var attachmentString = new NSMutableAttributedString(NSAttributedString.CreateFrom(attachment));
					var myString = new NSMutableAttributedString(" " + cellText);
					myString.SetAttributes(NSDictionary.FromObjectAndKey(cellFont, UIStringAttributeKey.Font),
						new NSRange(0, myString.Length));
					attachmentString.Append(myString);

					var rect = attachmentString.GetBoundingRect(new CGSize(260, float.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin | NSStringDrawingOptions.UsesFontLeading, null);

					return (float)Math.Max(25, rect.Height);
				case 4:
					CGSize constraintSize = new CGSize(260, float.MaxValue);
					CGSize labelSize = cellText.StringSize(cellFont, constraintSize, UILineBreakMode.WordWrap);

					return labelSize.Height;
			}

			return 0;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			if(indexPath.Section == 4)
			{
				var reuse = tableView.DequeueReusableCell(_cellInstrucoesIdentifier) as BrincadeiraInstrucaoTableViewCell;
				if(reuse != null)
				{
					return reuse;
				}

				var cell = new BrincadeiraInstrucaoTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellInstrucoesIdentifier);

				return cell;
			}
			else
			{
				var reuse = tableView.DequeueReusableCell(_cellIdentifier) as BrincadeiraTableViewCell;
				if(reuse != null)
				{
					reuse.Update(item.ToString(), indexPath.Section == 2 ? _textYellowColor : _textBlueColor);
					return reuse;
				}

				return CreateDefaultBindableCell(tableView, indexPath, item);
			}
		}

		private BrincadeiraTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = new BrincadeiraTableViewCell(UITableViewCellStyle.Default, _cellIdentifier, item.ToString(), indexPath.Section == 2 ? _textYellowColor : _textBlueColor);

			return cell;
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			switch(indexPath.Section)
			{
				case 2:
					return Brincadeira.Materiais.ElementAt(indexPath.Row);
				case 3:
					return Brincadeira.Objetivos.ElementAt(indexPath.Row);
				case 4:
					return Brincadeira.Instrucoes;
			}

			return null;
		}

		#endregion

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if(Brincadeira == null)
			{
				return 0;
			}

			if(section <= 1)
			{
				return 0;
			}

			if(section == 2)
			{
				return Brincadeira.Materiais.Count;
			}

			if(section == 3)
			{
				return Brincadeira.Objetivos.Count;
			}

			if (section == 4)
			{
				return 1;
			}

			return 0;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 5;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
		}

		public sealed class BrincadeiraTableViewCell : MvxStandardTableViewCell
		{

			public BrincadeiraTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public BrincadeiraTableViewCell(UITableViewCellStyle cellStyle, NSString cellIdentifier, string text, UIColor color)
				: base("", cellStyle, cellIdentifier, UITableViewCellAccessory.None)
			{
				TextLabel.TextAlignment = UITextAlignment.Center;
				TextLabel.Font = UIFont.SystemFontOfSize(15);
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				TextLabel.Lines = 0;

				Update(text, color);

				BackgroundColor = UIColor.White;
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(30, TextLabel.Frame.Top, 260, TextLabel.Frame.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;
			}

			public void Update(string text, UIColor color)
			{
				var attachment = new NSTextAttachment
				{
					Image = UIImage.FromBundle("visto")
				};

				var attachmentString = new NSMutableAttributedString(NSAttributedString.CreateFrom(attachment));
				var myString = new NSMutableAttributedString(" " + text);
				attachmentString.Append(myString);

				TextLabel.AttributedText = attachmentString;
				TextLabel.TextColor = color;
			}

		}

		public sealed class BrincadeiraInstrucaoTableViewCell : MvxStandardTableViewCell
		{

			public BrincadeiraInstrucaoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public BrincadeiraInstrucaoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
				TextLabel.Font = UIFont.SystemFontOfSize(15);
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				TextLabel.Lines = 0;
				BackgroundColor = UIColor.White;
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(30, TextLabel.Frame.Top, 260, TextLabel.Frame.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;
			}

		}

		private void OnScrollChanged()
		{
			var handler = ScrollChanged;
			if(handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

	}

}
