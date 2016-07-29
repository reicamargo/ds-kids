using Android.OS;
using Android.Views;

using BRFX.Core.Droid.Views;

using Cirrious.MvvmCross.Binding.Droid.BindingContext;

using DS.Kids.Apps.Core.Converters;
using DS.Kids.Apps.Core.ViewModels;

using MoPub.MobileAds;
using DS.Kids.Apps.Droid.Ads;

namespace DS.Kids.Apps.Droid.Views
{
	public class DiarioAlimentosView : BaseView
	{
        private Ad _adBanner;

        public override void OnDestroy()
        {
            if (_adBanner != null) _adBanner.Destroy();

            base.OnDestroy();
        }

        #region Public Methods and Operators

        public override void ConfigureActionBarView(View view)
		{
		    base.ConfigureActionBarView(view);

		    var diarioAlimentosViewModel = ViewModel as DiarioAlimentosViewModel;
		    if(diarioAlimentosViewModel != null)
		    {
		        Title = TipoGrupoRefeicaoTextConverter.Convert(diarioAlimentosViewModel.RefeicaoGrupo.TipoGrupoRefeicao);
		    }
		}

	    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var ignored = base.OnCreateView(inflater, container, savedInstanceState);

			ShowActionBar();

			var view = this.BindingInflate(Resource.Layout.DiarioAlimentosView, null);

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
		}

		#endregion
	}
}