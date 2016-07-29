using Android.OS;
using Android.Views;
using BRFX.Core.Droid.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.RecyclerView;
using Cirrious.MvvmCross.Plugins.Messenger;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;
using MoPub.MobileAds;
using DS.Kids.Apps.Droid.Ads;

namespace DS.Kids.Apps.Droid.Views
{
    public class DiarioRefeicaoView : BaseView
    {
        private MvxRecyclerView _recyclerView;
        private MvxSubscriptionToken _refreshDiarioToken;
        private Ad _adBanner;

        public DiarioRefeicaoView()
        {
            _refreshDiarioToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<RefreshDiarioMessage>(ReceiveShowDiarioCalendarMessage);
        }

        public override void OnDestroy()
        {
            if (_adBanner != null) _adBanner.Destroy();

            base.OnDestroy();
        }

        #region Public Methods and Operators

        private void ReceiveShowDiarioCalendarMessage(RefreshDiarioMessage obj)
        {
            if (obj.Sender == ViewModel) 
            {
                _recyclerView.GetAdapter().NotifyDataSetChanged();
            }
        }

        public override void ConfigureActionBarView(View view)
        {
            base.ConfigureActionBarView(view);

            var diarioRefeicaoViewModel = ViewModel as DiarioRefeicaoViewModel;
            if (diarioRefeicaoViewModel != null)
            {
                Title = diarioRefeicaoViewModel.TipoRefeicao.GetString();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            ShowActionBar();

            var view = this.BindingInflate(Resource.Layout.DiarioRefeicaoView, null);

            _recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.diarioRefeicao_recyclerView);

            _adBanner = new Ad(view.FindViewById<MoPubView>(Resource.Id.ad_banner), MoPubconfig.AD_UNIT_ID_BANNER);
            _adBanner.Load();

            return view;
        }

        #endregion
    }
}