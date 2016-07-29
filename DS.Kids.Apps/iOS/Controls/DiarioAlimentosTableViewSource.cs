using System;
using System.Collections.Generic;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Binders;
using Cirrious.MvvmCross.Binding.Bindings;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Plugins.Color.Touch;
using CoreGraphics;
using DS.Kids.Apps.Core.Converters;
using DS.Kids.Model;
using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

    public class DiarioAlimentosTableViewSource : MvxTableViewSource
    {
        #region Fields

        private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

        private readonly NSString _cellIdentifier = new NSString("DiarioAlimentosTableViewCell");

        #endregion

        #region Constructors and Destructors

        public DiarioAlimentosTableViewSource(UITableView tableView)
            : base(tableView)
        {
            _bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Nome; DetailText Format('{0:0.#} {1}', AlimentoMedidaFaixaEtaria.Quantidade, AlimentoMedidaFaixaEtaria.Medida.Nome)");
            ReloadOnAllItemsSourceSets = true;
            TableView.SectionHeaderHeight = 0.0f;
            TableView.SectionFooterHeight = 0.0f;
        }

        public DiarioAlimentosTableViewSource(IntPtr handle)
            : base(handle)
        {
            Mvx.Warning("DiarioAlimentosTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
        }

        #endregion

        #region Public Methods and Operators

        public override nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 0;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 44;
        }

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var view = new UIView(new CGRect(0, 0, 320, GetHeightForHeader(tableView, section)))
            {
                BackgroundColor = UIColor.White
            };

            return view;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 54;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            base.RowSelected(tableView, indexPath);

            tableView.DeselectRow(indexPath, true);
        }

        #endregion

        #region Methods

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var alimento = (Alimento)item;

            var reuse = tableView.DequeueReusableCell(_cellIdentifier);
            if (reuse != null)
            {
                //((DiarioAlimentosTableViewCell)reuse).Update(alimento);
                return reuse;
            }

            var cell = new DiarioAlimentosTableViewCell(_bindingDescriptions, UITableViewCellStyle.Subtitle, _cellIdentifier, alimento);

            cell.TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);
            cell.DetailTextLabel.TextColor = new UIColor(85 / 255.0f, 125 / 255.0f, 213 / 255.0f, 1.0f);

            return cell;
        }

        #endregion

        public class DiarioAlimentosTableViewCell : MvxStandardTableViewCell
        {

            //private UIView _border;

            #region Constructors and Destructors

            public DiarioAlimentosTableViewCell(IntPtr handle)
                : base(handle)
            {
            }

            public DiarioAlimentosTableViewCell(string bindingText, IntPtr handle)
                : base(bindingText, handle)
            {
            }

            public DiarioAlimentosTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
                : base(bindingDescriptions, handle)
            {
            }

            public DiarioAlimentosTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
                : base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
            {
            }

            public DiarioAlimentosTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, Alimento alimento)
                : base(bindingDescriptions, cellStyle, cellIdentifier, UITableViewCellAccessory.None)
            {
                InitializeSubViews();
                //Update(alimento);
            }

            #endregion

            #region Public Methods and Operators

            public override void LayoutSubviews()
            {
                base.LayoutSubviews();

                TextLabel.Frame = new CGRect(60, TextLabel.Frame.Top, 215, TextLabel.Frame.Height);
                DetailTextLabel.Frame = new CGRect(60, DetailTextLabel.Frame.Top, 215, DetailTextLabel.Frame.Height);
            }

            //public void Update(Alimento alimento)
            //{
            //    _border.BackgroundColor = alimento.AlimentoMedidaFaixaEtaria != null ? SemaforoColorConverter.Convert(alimento.AlimentoMedidaFaixaEtaria.Semaforo).ToNativeColor() : UIColor.Clear;
            //}

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

            #region Methods

            private void InitializeSubViews()
            {
                TextLabel.Font = UIFont.SystemFontOfSize(14);
                DetailTextLabel.Font = UIFont.SystemFontOfSize(12);

                var imageView = new UIImageView(new CGRect(Frame.Right - 40, 15, 25, 25))
                {
                    Image = UIImage.FromBundle("ico_add_food"),
                    UserInteractionEnabled = false
                };
                AddSubview(imageView);

                //_border = new UIView(new CGRect(38, 20, 12, 12));

                //_border.Layer.CornerRadius = 6;
                //_border.Layer.MasksToBounds = true;

                //AddSubview(_border);

                BackgroundColor = UIColor.White;
            }

            #endregion
        }

    }

}
