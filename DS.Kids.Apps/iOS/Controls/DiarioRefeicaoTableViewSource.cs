using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using BRFX.Core.IOS.Controls;

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

	public class DiarioRefeicaoTableViewSource : MvxTableViewSource
	{

		private readonly IEnumerable<MvxBindingDescription> _bindingDescriptions;

		private readonly NSString _cellIdentifier = new NSString("DiarioRefeicaoTableViewCell");

		public ICommand GrupoAlimentarSelectedCommand { get; set; }

		public ICommand CheckBoxCommand { get; set; }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);

			tableView.DeselectRow(indexPath, true);
		}

		public DiarioRefeicaoTableViewSource(UITableView tableView)
			: base(tableView)
		{
			_bindingDescriptions = Mvx.Resolve<IMvxBindingDescriptionParser>().Parse("TitleText Nome");
		}

		public DiarioRefeicaoTableViewSource(IntPtr handle)
			: base(handle)
		{
			Mvx.Warning("DiarioRefeicaoTableViewSource IntPtr constructor used - we expect this only to be called during memory leak debugging - see https://github.com/MvvmCross/MvvmCross/pull/467");
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if(editingStyle == UITableViewCellEditingStyle.Delete)
			{
				var alimento = GetItemAt(indexPath) as Alimento;
				if(alimento != null)
				{
					if (DeleteRefeicaoCommand != null && DeleteRefeicaoCommand.CanExecute(alimento))
					{
						DeleteRefeicaoCommand.Execute(alimento);
					}
				}
				tableView.SetEditing(false, true);
			}
		}

		public ObservableCollection<RefeicaoGrupo> Refeicoes
		{
			get
			{
				return (ObservableCollection<RefeicaoGrupo>)ItemsSource;
			}
		}

		public ICommand DeleteRefeicaoCommand { get; set; }

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if(ItemsSource == null)
			{
				return null;
			}

			return Refeicoes[indexPath.Section].Alimentos[indexPath.Row];
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if(ItemsSource == null)
			{
				return 0;
			}

			return Refeicoes[(int)section].Alimentos.Count;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if (ItemsSource == null)
			{
				return 0;
			}

			return Refeicoes.Count;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var alimento = (Alimento)item;

			var reuse = tableView.DequeueReusableCell(_cellIdentifier) as DiarioRefeicaoTableViewCell;
			if(reuse != null)
			{
				//reuse.Update(alimento);
				return reuse;
			}

			return CreateDefaultBindableCell(tableView, indexPath, alimento);
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var height = GetHeightForHeader(tableView, section);

			var view = new UIView(new CGRect(0, 0, 320, height))
							{
								BackgroundColor = UIColor.White
							};

			string headerText = null;
			nfloat headerY = 0;
			if (section == 0)
			{
				headerText = "Grupos recomendados";
			}
			else if (section == Refeicoes.Count(r => r.Sugerido))
			{
				var greyView = new UIView(new CGRect(0, 0, 320, 15))
								   {
									   BackgroundColor = new UIColor(237 / 255.0f, 237 / 255.0f, 245 / 255.0f, 1.0f)
								   };
				view.Add(greyView);
				headerY += greyView.Frame.Height;
                headerText = "Outros grupos";
			}

			if(headerText != null)
			{
				var headerLabel = new BRFXLabel
										 {
											 Frame = new CGRect(29, headerY + 2, 300, 58),
											 Text = headerText,
											 TextColor = new UIColor(85 / 255.0f, 125 / 255.0f, 213 / 255.0f, 1.0f),
											 CustomFont = "Hangyaboly",
											 CustomFontSize = 28
										 };
				headerLabel.AwakeFromNib();
				view.Add(headerLabel);

				var separatorView = new UIView(new CGRect(30, headerLabel.Frame.Bottom, 290, 0.5f))
										{
											BackgroundColor = new UIColor(211 / 255.0f, 211 / 255.0f, 211 / 255.0f, 1.0f)
										};
				view.Add(separatorView);
			}

			var grupoAlimentarButton = new UIButton(UIButtonType.Custom)
										   {
											   Frame = new CGRect(64, height - 50, 256, 50),
											   HorizontalAlignment = UIControlContentHorizontalAlignment.Left,
											   Font = UIFont.SystemFontOfSize(16),
										   };
			var refeicao = Refeicoes[(int)section];
			grupoAlimentarButton.SetTitle(TipoGrupoRefeicaoTextConverter.Convert(refeicao.TipoGrupoRefeicao), UIControlState.Normal);
			grupoAlimentarButton.SetTitleColor(new UIColor(0.4f, 0.4f, 0.4f, 1.0f), UIControlState.Normal);
			grupoAlimentarButton.TouchUpInside += (sender, args) =>
				{
					if (GrupoAlimentarSelectedCommand != null && GrupoAlimentarSelectedCommand.CanExecute(refeicao))
					{
						GrupoAlimentarSelectedCommand.Execute(refeicao);
					}
				};

			view.AddSubview(grupoAlimentarButton);

			var checkBox = new UIButton(new CGRect(30, grupoAlimentarButton.Frame.Top + 11, 27, 27));
			checkBox.SetBackgroundImage(UIImage.FromBundle("ico_check_miss"), UIControlState.Normal);
			checkBox.SetBackgroundImage(UIImage.FromBundle("ico_check_ok"), UIControlState.Selected);
			checkBox.Selected = refeicao.RefeicaoRealizada;
			checkBox.TouchUpInside += (sender, args) =>
			{
				if (CheckBoxCommand != null && CheckBoxCommand.CanExecute(refeicao))
				{
					CheckBoxCommand.Execute(refeicao);
					checkBox.Selected = refeicao.RefeicaoRealizada;
				}
			};

			view.AddSubview(checkBox);

			var imageView = new UIImageView(new CGRect(grupoAlimentarButton.Frame.Right - 40, grupoAlimentarButton.Frame.Top + 13, 25, 25))
								{
									Image = UIImage.FromBundle("ico_add_food"),
									UserInteractionEnabled = false
								};
			view.AddSubview(imageView);
			
			return view;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			if (section == 0)
			{
				return 110;
			}
			if (section == Refeicoes.Count(r => r.Sugerido))
			{
				return 125;
			}

			return 50;
		}

		public override UIView GetViewForFooter(UITableView tableView, nint section)
		{
			var footer = new UIView(new CGRect(0, 0, 320, 0.5f))
							 {
								 BackgroundColor = UIColor.White
							 };
			var view = new UIView(new CGRect(30, 0, 290, 0.5f))
					   {
						   BackgroundColor = new UIColor(211 / 255.0f, 211 / 255.0f, 211 / 255.0f, 1.0f)
					   };
			footer.AddSubview(view);
			return footer;
		}

		public override nfloat GetHeightForFooter(UITableView tableView, nint section)
		{
			return 0.5f;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 50;
		}

		protected DiarioRefeicaoTableViewCell CreateDefaultBindableCell(UITableView tableView, NSIndexPath indexPath, Alimento alimento)
		{
			var cell = new DiarioRefeicaoTableViewCell(_bindingDescriptions, UITableViewCellStyle.Default, _cellIdentifier, alimento);

			cell.TextLabel.TextColor = new UIColor(0.4f, 0.4f, 0.4f, 1.0f);

			return cell;
		}

		public class DiarioRefeicaoTableViewCell : MvxStandardTableViewCell
		{

			//private UIView _border;

			public DiarioRefeicaoTableViewCell(IntPtr handle)
				: base(handle)
			{
			}

			public DiarioRefeicaoTableViewCell(string bindingText, IntPtr handle)
				: base(bindingText, handle)
			{
			}

			public DiarioRefeicaoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, IntPtr handle)
				: base(bindingDescriptions, handle)
			{
			}

			public DiarioRefeicaoTableViewCell(string bindingText, UITableViewCellStyle cellStyle, NSString cellIdentifier, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingText, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
			}

			public DiarioRefeicaoTableViewCell(IEnumerable<MvxBindingDescription> bindingDescriptions, UITableViewCellStyle cellStyle, NSString cellIdentifier, Alimento alimento, UITableViewCellAccessory tableViewCellAccessory = UITableViewCellAccessory.None)
				: base(bindingDescriptions, cellStyle, cellIdentifier, tableViewCellAccessory)
			{
				InitializeSubViews();
				//Update(alimento);
			}

			private  void InitializeSubViews()
			{
				TextLabel.Font = UIFont.SystemFontOfSize(14);

				var separatorView = new UIView(new CGRect(30, 0, 290, 0.5f))
				{
					BackgroundColor = new UIColor(211 / 255.0f, 211 / 255.0f, 211 / 255.0f, 1.0f)
				};

				Add(separatorView);

				//_border = new UIView(new CGRect(38, 19, 12, 12));

				//_border.Layer.CornerRadius = 6;
				//_border.Layer.MasksToBounds = true;

				//AddSubview(_border);

				BackgroundColor = UIColor.White;
			}

			public override void LayoutSubviews()
			{
				base.LayoutSubviews();

				TextLabel.Frame = new CGRect(62, 0, 256, TextLabel.Frame.Height);
			}

			//public void Update(Alimento alimento)
			//{
   //             _border.BackgroundColor = alimento.AlimentoMedidaFaixaEtaria != null ? SemaforoColorConverter.Convert(alimento.AlimentoMedidaFaixaEtaria.Semaforo).ToNativeColor() : UIColor.Clear;
			//}

			//public override void SetHighlighted(bool highlighted, bool animated)
			//{
			//	var backgroundColor = _border.BackgroundColor;
			//	base.SetHighlighted(highlighted, animated);
			//	_border.BackgroundColor = backgroundColor;
			//}

			//public override void SetSelected(bool selected, bool animated)
			//{
			//	var backgroundColor = _border.BackgroundColor;
			//	base.SetSelected(selected, animated);
			//	_border.BackgroundColor = backgroundColor;
			//}

		}
	}

}
