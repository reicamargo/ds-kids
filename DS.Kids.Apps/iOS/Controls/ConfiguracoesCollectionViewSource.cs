using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.ViewModels;

using CoreGraphics;

using DS.Kids.Model;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public sealed class ConfiguracoesCollectionViewSource : MvxCollectionViewSource
	{
		#region Fields

		private readonly NSString _newCriancaCellIdentifier = new NSString("NewCriancaCollectionViewCell");

		#endregion

		#region Constructors and Destructors

		public ConfiguracoesCollectionViewSource(UICollectionView collectionView)
			: base(collectionView, new NSString("CriancaCollectionViewCell"))
		{
			CollectionView.RegisterClassForCell(typeof(CriancaCollectionViewCell), DefaultCellIdentifier);
			CollectionView.RegisterClassForCell(typeof(NewCriancaCollectionViewCell), _newCriancaCellIdentifier);
			ReloadOnAllItemsSourceSets = true;
		}

		#endregion

		#region Public Properties

		public MvxCommand AdicionarFilhoCommand { get; set; }

		public ObservableCollection<Crianca> Criancas
		{
			get
			{
				return (ObservableCollection<Crianca>)ItemsSource;
			}
		}

		#endregion

		#region Public Methods and Operators

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			if(ItemsSource == null)
			{
				return 0;
			}

			return ItemsSource.Count() + 1;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			ICommand command;
			if(indexPath.Row == ItemsSource.Count())
			{
				command = AdicionarFilhoCommand;
				if(command != null)
				{
					command.Execute(null);
				}
				return;
			}

			command = SelectionChangedCommand;
			if(command != null)
			{
				command.Execute(GetItemAt(indexPath));
			}
		}

		#endregion

		#region Methods

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if(indexPath.Row == ItemsSource.Count())
			{
				return null;
			}

			return base.GetItemAt(indexPath);
		}

		protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
		{
			if(indexPath.Row == ItemsSource.Count())
			{
				return (UICollectionViewCell)collectionView.DequeueReusableCell(_newCriancaCellIdentifier, indexPath);
			}

			var cell = (CriancaCollectionViewCell)collectionView.DequeueReusableCell(DefaultCellIdentifier, indexPath);
			cell.IsSelected = item == SelectedItem;
			return cell;
		}

		#endregion

		public sealed class CriancaCollectionViewCell : MvxCollectionViewCell
		{
			#region Fields

			private readonly MvxImageView _imageView;

			private readonly UILabel _nomeLabel;

			private readonly UIView _selectedBlackView;

			private bool _isSelected;

			#endregion

			#region Constructors and Destructors

			[Export("initWithFrame:")]
			public CriancaCollectionViewCell(RectangleF frame)
				: base(frame)
			{
				this.CreateBindingContext("NomeText Nome; Image RelativeToAbsoluteUrl(UrlImagem)");

				_imageView = new MvxImageView(new CGRect(35, 25, 90, 90))
								{
									ContentMode = UIViewContentMode.ScaleAspectFill,
									ClipsToBounds = true,
									DefaultImagePath = "res:avatar-default"
								};

				_imageView.Layer.CornerRadius = 45;
				_imageView.Layer.MasksToBounds = true;
				_imageView.ClipsToBounds = true;

				ContentView.AddSubview(_imageView);

				_nomeLabel = new UILabel(new CGRect(35, 115, 90, 25))
								{
									TextAlignment = UITextAlignment.Center,
									TextColor = UIColor.FromRGB(100, 100, 100)
								};
				ContentView.AddSubview(_nomeLabel);

				_selectedBlackView = new UIView(new CGRect(35, 25, 90, 90))
										{
											BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0.65f)
										};
				_selectedBlackView.Layer.CornerRadius = 45;
				_selectedBlackView.Layer.MasksToBounds = true;
				_selectedBlackView.ClipsToBounds = true;
				ContentView.AddSubview(_selectedBlackView);
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

			public bool IsSelected
			{
				get
				{
					return _isSelected;
				}
				set
				{
					_isSelected = value;
					if(_selectedBlackView != null)
					{
						_selectedBlackView.Hidden = value;
					}
				}
			}

			#endregion
		}

		public sealed class NewCriancaCollectionViewCell : MvxCollectionViewCell
		{
			#region Constructors and Destructors

			[Export("initWithFrame:")]
			public NewCriancaCollectionViewCell(RectangleF frame)
				: base(frame)
			{
				var roundBorderView = new UIView(new CGRect(35, 25, 90, 90))
									{
										BackgroundColor = UIColor.FromRGB(193, 193, 193)
									};
				roundBorderView.Layer.CornerRadius = 45;
				roundBorderView.Layer.MasksToBounds = true;
				roundBorderView.ClipsToBounds = true;
				ContentView.AddSubview(roundBorderView);

				roundBorderView.AddSubview(new UIView(new CGRect(28, 44, 35, 2))
											{
												BackgroundColor = UIColor.White
											});
				roundBorderView.AddSubview(new UIView(new CGRect(44, 28, 2, 35))
											{
												BackgroundColor = UIColor.White
											});

				var addLabel = new UILabel(new CGRect(35, 115, 90, 25))
									{
										TextAlignment = UITextAlignment.Center,
										TextColor = UIColor.FromRGB(100, 100, 100),
										Text = "adicionar"
									};
				ContentView.AddSubview(addLabel);
			}

			#endregion
		}

	}

}
