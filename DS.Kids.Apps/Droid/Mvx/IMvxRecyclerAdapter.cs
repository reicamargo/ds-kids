using System.Collections;
using System.Windows.Input;

using Cirrious.MvvmCross.Binding.Attributes;

namespace Cirrious.MvvmCross.Droid.RecyclerView
{

	public interface IMvxRecyclerAdapter
	{
		#region Public Properties

		ICommand ItemClick { get; set; }

		ICommand ItemLongClick { get; set; }

		[MvxSetToNullAfterBinding]
		IEnumerable ItemsSource { get; set; }

		int ItemTemplateId { get; set; }

		#endregion

		#region Public Methods and Operators

		object GetItem(int position);

		#endregion
	}

}
