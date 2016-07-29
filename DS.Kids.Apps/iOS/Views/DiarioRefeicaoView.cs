using System;

using BRFX.Core.IOS.Views;
using BRFX.Core.ViewModels;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;

using CoreGraphics;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{
    partial class DiarioRefeicaoView : ProgressView
    {
        private MvxSubscriptionToken _refreshDiarioToken;

        public DiarioRefeicaoView(IntPtr handle)
            : base(handle)
        {

        }

        public override void OnLoad()
        {
            base.OnLoad();

            var viewModel = ViewModel as BaseViewModel;
            if (viewModel != null)
            {
                _refreshDiarioToken = viewModel.Messenger.SubscribeOnMainThread<RefreshDiarioMessage>(ReceiveRefreshDiarioMessage);
            }
        }

        private void ReceiveRefreshDiarioMessage(RefreshDiarioMessage obj)
        {
            if(tableView != null)
            {
                tableView.ReloadData();
            }
        }

        public override void OnUnload()
        {
            var viewModel = ViewModel as BaseViewModel;
            if (viewModel != null)
            {
                viewModel.Messenger.Unsubscribe<RefreshDiarioMessage>(_refreshDiarioToken);
            }

            base.OnUnload();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.BackBarButtonItem = new UIBarButtonItem
            {
                Title = ""
            };

            var bindingSet = this.CreateBindingSet<DiarioRefeicaoView, DiarioRefeicaoViewModel>();

            var topview = new UIView(new CGRect(0, -480, 320, 480))
            {
                BackgroundColor = UIColor.FromRGB(238, 237, 243)
            };

            tableView.BackgroundView = topview;

            System.Drawing.SizeF size = new System.Drawing.SizeF(320, 50);
            MoPubSDK.MPAdView _mpAdView = new MoPubSDK.MPAdView(Ads.MoPubconfig.AD_UNIT_ID_BANNER, size);

            _mpAdView.Frame = new System.Drawing.RectangleF(0, -50, 320, 50);
            _mpAdView.LoadAd();
            tableView.AddSubview(_mpAdView);

            var source = new DiarioRefeicaoTableViewSource(tableView);
            tableView.Source = source;

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.TipoRefeicao).WithConversion("TipoRefeicaoText");

            bindingSet.Bind(source).To(vm => vm.RefeicoesGrupo);
            bindingSet.Bind(source).For(v => v.GrupoAlimentarSelectedCommand).To(vm => vm.RefeicaoGrupoSelectedCommand);
            bindingSet.Bind(source).For(v => v.CheckBoxCommand).To(vm => vm.CheckBoxCommand);
            bindingSet.Bind(source).For(v => v.DeleteRefeicaoCommand).To(vm => vm.DeleteAlimentoCommand);

            bindingSet.Apply();

            tableView.ReloadData();
        }
    }
}
