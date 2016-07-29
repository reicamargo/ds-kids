using Cirrious.MvvmCross.Binding.BindingContext;

namespace Cirrious.MvvmCross.Droid.RecyclerView
{

	public interface IMvxRecyclerViewHolder : IMvxBindingContextOwner
	{
		#region Public Properties

		object DataContext { get; set; }

		#endregion

		#region Public Methods and Operators

		void OnAttachedToWindow();

		void OnDetachedFromWindow();

		#endregion
	}

}
