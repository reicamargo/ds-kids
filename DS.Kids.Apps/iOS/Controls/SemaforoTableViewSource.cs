using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

	public class SemaforoTableViewSource : MvxTableViewSource
	{
		#region Fields

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("SemaforoTableViewCell");

		#endregion

		#region Constructors and Destructors

		public SemaforoTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Nome");
			ReloadOnAllItemsSourceSets = true;
			TableView.SectionHeaderHeight = 0.0f;
			TableView.SectionFooterHeight = 0.0f;
		}

		public SemaforoTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("SemaforoTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		#endregion

		#region Public Properties

		public ObservableCollection<Alimento> Alimentos
		{
			get
			{
				return (ObservableCollection<Alimento>)ItemsSource;
			}
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
			return 50;
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
			if(reuse != null)
			{
				((SemaforoTableViewCell)reuse).Update(alimento);
				return reuse;
			}

			return new SemaforoTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellIdentifier, alimento);
		}

		#endregion

		public class SemaforoTableViewCell : MvxStandardTableViewCell
		{

			private UIView _border;

			#region Constructors and Destructors

			public SemaforoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public SemaforoTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public SemaforoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public SemaforoTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public SemaforoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, Alimento alimento)
				: base(bindingDescriptions, cellStyle, cellIdentifier, UITableViewCellAccessory.None)
			{
				InitializeSubViews();
				Update(alimento);
			}

			#endregion

			#region Public Methods and Operators

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(44, TextLabel.Frame.Top, 247, TextLabel.Frame.Height);
				TextLabel.ContentMode = UIViewContentMode.ScaleAspectFit;
			}

			public void Update(Alimento alimento)
			{
				_border.BackgroundColor = SemaforoColorConverter.Convert(alimento.AlimentoMedidaFaixaEtaria.Semaforo).ToNativeColor();
			}

			#endregion

			#region Methods

			public override void SetHighlighted(bool highlighted, bool animated)
			{
				var backgroundColor = _border.BackgroundColor;
				base.SetHighlighted(highlighted, animated);
				_border.BackgroundColor = backgroundColor;
			}

			public override void SetSelected(bool selected, bool animated)
			{
				var backgroundColor = _border.BackgroundColor;
				base.SetSelected(selected, animated);
				_border.BackgroundColor = backgroundColor;
			}

			private void InitializeSubViews()
			{
				TextLabel.Font = UIFont.SystemFontOfSize(14);
				TextLabel.Lines = 0;
				TextLabel.LineBreakMode = UILineBreakMode.WordWrap;

				_border = new UIView(new CGRect(22, 19, 12, 12));

				_border.Layer.CornerRadius = 6;
				_border.Layer.MasksToBounds = true;

				AddSubview(_border);

				BackgroundColor = UIColor.White;
			}

			#endregion
		}

	}

}
