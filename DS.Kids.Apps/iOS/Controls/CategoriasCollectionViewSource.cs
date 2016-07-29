using System;
using System.Collections.ObjectModel;
using System.Linq;

using BRFX.Core.IOS.Controls;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Plugins.Color.Touch;

using CoreGraphics;

using DS.Kids.Apps.Core.Converters;
using DS.Kids.Model;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Controls
{

	public sealed class CategoriasCollectionViewSource : MvxCollectionViewSource
	{

		private readonly NSString _categoriaDestaqueCellIdentifier = new NSString("CategoriaDestaqueCollectionViewCell");

		#region Constructors and Destructors

		public CategoriasCollectionViewSource(UICollectionView collectionView)
			: base(collectionView, new NSString("CategoriaCollectionViewCell"))
		{
			CollectionView.RegisterClassForCell(typeof(CategoriaCollectionViewCell), DefaultCellIdentifier);
			CollectionView.RegisterClassForCell(typeof(CategoriaDestaqueCollectionViewCell), _categoriaDestaqueCellIdentifier);
			ReloadOnAllItemsSourceSets = true;
		}

		public ObservableCollection<Categoria> Categorias
		{
			get
			{
				return (ObservableCollection<Categoria>)ItemsSource;
			}
		}

		protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, NSIndexPath indexPath, object item)
		{
			
			UICollectionViewCell cell;
			if(indexPath.Row < Categorias.Count(c => c.Destaque))
			{
				cell = (UICollectionViewCell)collectionView.DequeueReusableCell(_categoriaDestaqueCellIdentifier, indexPath);
			}
			else
			{
				cell = (UICollectionViewCell)collectionView.DequeueReusableCell(DefaultCellIdentifier, indexPath);
			}

			cell.ContentView.BackgroundColor = CategoriasColorConverter.Convert((Categoria)item).ToNativeColor();

			return cell;
		}

		#endregion

		public sealed class CategoriaCollectionViewCell : MvxCollectionViewCell
		{
			#region Fields

			private readonly MvxImageView _imageView;

			private readonly UILabel _nomeLabel;

			#endregion

			#region Constructors and Destructors

			[Export("initWithFrame:")]
			public CategoriaCollectionViewCell(System.Drawing.RectangleF frame)
				: base(frame)
			{
				this.CreateBindingContext("NomeText Nome; Image RelativeToAbsoluteUrl(UrlImagem)");

				_imageView = new MvxImageView(new CGRect(15, 8, 77, 48))
								{
									ContentMode = UIViewContentMode.ScaleAspectFill,
									ClipsToBounds = true
								};

				ContentView.AddSubview(_imageView);

				_nomeLabel = new UILabel
								{
									Frame = new CGRect(5, 59, 94, 43),
									TextAlignment = UITextAlignment.Center,
									TextColor = UIColor.White,
									Font = UIFont.SystemFontOfSize(11),
									Lines = 3,
									LineBreakMode = UILineBreakMode.WordWrap
								};
				ContentView.AddSubview(_nomeLabel);
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

			#endregion
		}

		public sealed class CategoriaDestaqueCollectionViewCell : MvxCollectionViewCell
		{
			#region Fields

			private readonly MvxImageView _imageView;

			private readonly BRFXLabel _nomeLabel;

			#endregion

			#region Constructors and Destructors

			[Export("initWithFrame:")]
			public CategoriaDestaqueCollectionViewCell(System.Drawing.RectangleF frame)
				: base(frame)
			{
				this.CreateBindingContext("NomeText Nome; Image RelativeToAbsoluteUrl(UrlImagem)");

				_imageView = new MvxImageView(new CGRect(225, 28, 77, 48))
				{
					ContentMode = UIViewContentMode.ScaleAspectFill,
					ClipsToBounds = true
				};

				ContentView.AddSubview(_imageView);

				_nomeLabel = new BRFXLabel
				{
					Frame = new CGRect(30, 0, 190, 108),
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.White,
					CustomFont = "Hangyaboly",
					CustomFontSize = 26,
					Lines = 0,
					LineBreakMode = UILineBreakMode.WordWrap
				};
				_nomeLabel.AwakeFromNib();
				ContentView.AddSubview(_nomeLabel);
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

			#endregion
		}

	}

	public class CategoriasCollectionViewDelegateFlowLayout : UICollectionViewDelegateFlowLayout
	{

		private readonly CategoriasCollectionViewSource _categoriasCollectionViewSource;

		public CategoriasCollectionViewDelegateFlowLayout(CategoriasCollectionViewSource categoriasCollectionViewSource)
		{
			_categoriasCollectionViewSource = categoriasCollectionViewSource;
		}

		#region Public Methods and Operators

		public override nfloat GetMinimumInteritemSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
		{
			return 4;
		}

		public override nfloat GetMinimumLineSpacingForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
		{
			return 4;
		}

		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			_categoriasCollectionViewSource.ItemSelected(collectionView, indexPath);
		}

		public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
		{
			if (indexPath.Row < _categoriasCollectionViewSource.Categorias.Count(c => c.Destaque))
			{
				return new CGSize(320, 104);
			}

			return new CGSize(104, 104);
		}

		#endregion
	}

}
