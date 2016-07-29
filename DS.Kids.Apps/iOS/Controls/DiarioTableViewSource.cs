using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

	public class DiarioTableViewSource : MvxTableViewSource
	{

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("DiarioTableViewCell");

		public ICommand SemaforoCommand { get; set; }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);

			tableView.DeselectRow(indexPath, true);
		}

		public DiarioTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText TipoRefeicaoText(TipoRefeicao); Image DiarioRefeicaoComidaImage(TipoRefeicaoRealizada)");
		}

		public DiarioTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("DiarioTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		public ObservableCollection<RefeicaoDiario> TiposRefeicoesDiario
		{
			get
			{
				return (ObservableCollection<RefeicaoDiario>)ItemsSource;
			}
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if(ItemsSource == null)
			{
				return null;
			}

			return ItemsSource.ElementAt(indexPath.Row);
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if(ItemsSource == null)
			{
				return 0;
			}

			return ItemsSource.Count();
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var reuse = tableView.DequeueReusableCell(_cellIdentifier) as DiarioTableViewCell;
			if(reuse != null)
			{
				return reuse;
			}

			return CreateDefaultBindableCell(tableView, indexPath);
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var height = GetHeightForHeader(tableView, section);
			var view = new UIView(new CGRect(0, 0, 320, height))
							{
								BackgroundColor = new UIColor(237 / 255.0f, 238 / 255.0f, 243 / 255.0f, 1.0f)
							};

			return view;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 14;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 56;
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

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 40;
        }


        protected DiarioTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new DiarioTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellIdentifier, UITableViewCellAccessory.DisclosureIndicator);

			cell.TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);

			return cell;
		}

		public class DiarioTableViewCell : MvxStandardTableViewCell
		{

			private MvxImageView _imageView;

			public DiarioTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public DiarioTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public DiarioTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public DiarioTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public DiarioTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				InitializeSubViews();
			}

			private void InitializeSubViews()
			{
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				TextLabel.Font = UIFont.SystemFontOfSize(17);
				TextLabel.Lines = 0;

				_imageView = new MvxImageView(new CGRect(29, 14, 29, 29));

				AddSubview(_imageView);
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(67, 1, 238, TextLabel.Frame.Height);
			}

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

		}
	}

}
