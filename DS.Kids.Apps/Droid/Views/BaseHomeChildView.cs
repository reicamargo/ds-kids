using Android.Views;

using BRFX.Core.Droid.Views;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Views
{

	internal class BaseHomeChildView : BaseView
	{
		#region Public Methods and Operators

		public override void ConfigureActionBarView(View view)
		{
			base.ConfigureActionBarView(view);

			var baseHomeChildViewModel = ViewModel as BaseHomeChildViewModel;
			if(baseHomeChildViewModel != null)
			{
				baseHomeChildViewModel.Displayed();
			}
		}

		#endregion
	}

}
