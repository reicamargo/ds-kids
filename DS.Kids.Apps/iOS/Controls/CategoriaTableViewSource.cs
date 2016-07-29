using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public class CategoriaTableViewSource : MvxTableViewSource
	{

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		public Categoria Categoria { get; set; }

		private readonly NSString _cellIdentifier = new NSString("CategoriaDicaTableViewCell");

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);

			tableView.DeselectRow(indexPath, true);
		}

		public CategoriaTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Titulo");
			TableView.SectionHeaderHeight = 0.0f;
			TableView.SectionFooterHeight = 0.0f;
		}

		public CategoriaTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("CategoriaTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		public ObservableCollection<Dica> Dicas
		{
			get
			{
				return (ObservableCollection<Dica>)ItemsSource;
			}
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			//var reuse = tableView.DequeueReusableCell(_cellIdentifier);
            var dica = Categoria.Dicas[indexPath.Row];

            //if (reuse != null)
            //{
            //    ((CategoriaDicaTableViewCell)reuse).Update(this, dica);
            //    return reuse;
            //}

            return new CategoriaDicaTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellIdentifier, this, dica);
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var view = new UIView(new CGRect(0, 0, 320, GetHeightForHeader(tableView, section)))
							{
								BackgroundColor = CategoriasColorConverter.Convert(Categoria).ToNativeColor()
							};

			view.AddSubview(new UIImageView(UIImage.FromBundle("bg_glass"))
								{
									Frame = view.Frame
								});

			view.AddSubview(new MvxImageView(new CGRect(215, 30, 77, 48))
								{
									ImageUrl = Endpoints.BASE + Categoria.UrlImagem,
									ContentMode = UIViewContentMode.ScaleAspectFill,
									ClipsToBounds = true
								});

			var label = new BRFXLabel
							{
								Frame = new CGRect(30, 4, 180, view.Frame.Height - 4),
								Text = Categoria.Nome,
								TextColor = UIColor.White,
								Lines = 0,
								LineBreakMode = UILineBreakMode.WordWrap,
								TextAlignment = UITextAlignment.Center,
								CustomFont = "Hangyaboly",
								CustomFontSize = 24
							};
			label.AwakeFromNib();
			view.AddSubview(label);

			return view;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 109;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 63;
		}

		public class CategoriaDicaTableViewCell : MvxStandardTableViewCell
		{

			public CategoriaDicaTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public CategoriaDicaTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public CategoriaDicaTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public CategoriaDicaTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public CategoriaDicaTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, CategoriaTableViewSource viewSource, Dica dica)
				: base(bindingDescriptions, cellStyle, cellIdentifier, UITableViewCellAccessory.DisclosureIndicator)
			{
				InitializeSubViews();
				Update(viewSource, dica);
			}

			private void InitializeSubViews()
			{
                TextLabel.Font = UIFont.SystemFontOfSize(14);
				TextLabel.Lines = 0;
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;
				SeparatorInset = new UIEdgeInsets(0, 29, 0, 0);

				BackgroundColor = UIColor.White;
			}

            public void Update(CategoriaTableViewSource viewSource, Dica dica)
            {
                if (dica.Parceiro != null)
                {
                    var oferecidoLabel = new UILabel
                    {
                        Frame = new CGRect(230, TextLabel.Frame.Top + 18, 25, 13),
                        TextAlignment = UITextAlignment.Left,
                        TextColor = UIColor.Gray,
                        Font = UIFont.ItalicSystemFontOfSize(12),
                        Text = "por"
                    };
                    this.Add(oferecidoLabel);
                    this.Add(new MvxImageView(new CGRect(250, TextLabel.Frame.Top + 16, 40, 20))
                    {
                        ImageUrl = Endpoints.BASE + dica.Parceiro.UrlIcone,
                        ContentMode = UIViewContentMode.ScaleAspectFill,
                        ClipsToBounds = true
                    });
                }
                TextLabel.TextColor = CategoriasColorConverter.Convert(viewSource.Categoria).ToNativeColor();
            }

            public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(30, TextLabel.Frame.Top, 195, TextLabel.Frame.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;
			}

		}
	}

}
