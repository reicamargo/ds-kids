using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Views
{

    internal class CategoriaView : BaseView
    {
        #region Public Methods and Operators

        public override void ConfigureActionBarView(View view)
        {
            ShowActionBar();

            string title = "Categoria";

            var VM = ViewModel as CategoriaViewModel;
            if (VM != null && VM.ShowHamburgerMenu)
            {
                IsActionBarHomeView = true;
                title = VM.Categoria.Nome;
            }

            base.ConfigureActionBarView(view);
            Title = title;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.CategoriaView, null);

            return view;
        }

        #endregion
    }

}
