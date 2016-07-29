using Android.Support.V7.Widget;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Droid.RecyclerView;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Controls
{

	public sealed class DicaRecyclerViewAdapter : ArrayRecyclerAdapter
	{
		#region Constructors and Destructors

		public DicaRecyclerViewAdapter()
			: this(MvxAndroidBindingContextHelpers.Current())
		{
		}

		public DicaRecyclerViewAdapter(IMvxAndroidBindingContext bindingContext)
		{
			BindingContext = bindingContext;
		}

		#endregion

		#region Public Properties

		public override int ItemCount => ItemsSource?.Count() + 1 ?? 0;

	    #endregion

		#region Properties

		private IMvxAndroidBindingContext BindingContext { get; }

	    #endregion

		#region Public Methods and Operators

		public override int GetItemViewType(int position)
		{
		    if (position >= ItemsSource.Count()) return 0;

			var elem = ItemsSource.ElementAt(position) as Paragrafo;
			var result = (int)TipoParagrafo.Texto;

			if(elem != null)
			{
				result = (int)elem.TipoParagrafo;
			}

			return result;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			((MvxRecyclerViewHolder)holder).DataContext = position >= ItemsSource.Count() ? null : ItemsSource.ElementAt(position);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflater);

			int itemTemplate;
			switch((TipoParagrafo)viewType)
			{
				case TipoParagrafo.Imagem:
					itemTemplate = Resource.Layout.DicaViewImagem;
					break;
				case TipoParagrafo.Video:
					itemTemplate = Resource.Layout.DicaViewVideo;
					break;
			    case TipoParagrafo.Texto:
					itemTemplate = Resource.Layout.DicaViewTexto;
			        break;
			    default:
			        itemTemplate = Resource.Layout.DicaViewFooterItem;
					break;
			}

			return new MvxRecyclerViewHolder(InflateViewForHolder(parent, itemTemplate, itemBindingContext), itemBindingContext)
						{
							Click = ItemClick,
							LongClick = ItemLongClick
						};
		}

		#endregion

		#region Methods

		private static View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext) 
            => bindingContext.BindingInflate(viewType, parent, false);

	    #endregion
	}

}
