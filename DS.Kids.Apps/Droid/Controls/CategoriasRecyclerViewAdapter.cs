using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Droid.RecyclerView;

using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Controls
{

	public class CategoriasRecyclerViewAdapter : HighlightRecyclerViewAdapter

	{
		#region Constructors and Destructors

		public CategoriasRecyclerViewAdapter()
		{
		}

		public CategoriasRecyclerViewAdapter(IMvxAndroidBindingContext bindingContext)
			: base(bindingContext)
		{
		}

		#endregion

		#region Public Methods and Operators

		public override int GetItemViewType(int position)
		{
			return (int)GetItemType(position);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
		{
			base.OnBindViewHolder(viewHolder, position);

			var layoutParams = viewHolder.ItemView.LayoutParameters.JavaCast<StaggeredGridLayoutManager.LayoutParams>();
			if(layoutParams != null)
			{
				layoutParams.FullSpan = GetItemType(position) == ItemListType.Highlighted;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflater);

			var itemType = (ItemListType)viewType;

			var itemTemplate = itemType == ItemListType.Highlighted ? HighlightItemTemplateId : ItemTemplateId;

			return new MvxRecyclerViewHolder(InflateViewForHolder(parent, itemTemplate, itemBindingContext), itemBindingContext)
						{
							Click = ItemClick,
							LongClick = ItemLongClick
						};
		}

		#endregion

		#region Methods

		protected override ItemListType GetItemType(int position)
		{
			var elem = ItemsSource.ElementAt(position) as Categoria;

			if(elem != null)
			{
				return elem.Destaque ? ItemListType.Highlighted : ItemListType.Normal;
			}

			return ItemListType.Normal;
		}

		#endregion
	}

}
