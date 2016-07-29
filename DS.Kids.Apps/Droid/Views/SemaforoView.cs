using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

using BRFX.Core.Droid.Validation;
using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Views
{
	public class SemaforoView : BaseView, TextView.IOnEditorActionListener
	{
		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			Title = "Semáforo";
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.SemaforoView, null);

			var search = view.FindViewById<BRFXEditText>(Resource.Id.semaforo_search);
			search.SetOnEditorActionListener(this);

			return view;
		}

		public bool OnEditorAction (TextView v, ImeAction actionId, KeyEvent e)
		{
			if (actionId == ImeAction.Done) 
			{
				var vm = ViewModel as SemaforoViewModel;

				if (vm != null)
				{
					vm.SearchCommand.Execute();
				}
			}
			return false;
		}

		#endregion
	}
}