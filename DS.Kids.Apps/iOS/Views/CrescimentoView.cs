using System;
using System.ComponentModel;

using BRFX.Core.IOS.Views;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;

using CorePlot;

using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class CrescimentoView : BaseHomeChildView
	{
		#region Fields

		private readonly MvxSubscriptionToken _crescimentosUpdatedToken;

		private CrescimentosTableViewSource _source;

		private CPTGraphHostingView _hostView;

		private CPTXYGraph _graph;

		private GraphHelper.MyDataSource _dataSource;

		#endregion

		#region Constructors and Destructors

		public CrescimentoView(IntPtr handle)
			: base(handle)
		{
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_crescimentosUpdatedToken = messenger.SubscribeOnMainThread<CrescimentosUpdatedMessage>(ReceiveCrescimentosUpdatedMessage);
		}

		#endregion

		#region Public Methods and Operators

		public override void OnUnload()
		{
			var mvxNotifyPropertyChanged = ViewModel as MvxNotifyPropertyChanged;
			if(mvxNotifyPropertyChanged != null)
			{
				mvxNotifyPropertyChanged.PropertyChanged -= OnPropertyChanged;
			}

			if(_crescimentosUpdatedToken != null)
			{
				var messenger = Mvx.Resolve<IMvxMessenger>();
				messenger.Unsubscribe<CrescimentosUpdatedMessage>(_crescimentosUpdatedToken);
			}

			base.OnUnload();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

            System.Drawing.SizeF size = new System.Drawing.SizeF(320, 50);
            MoPubSDK.MPAdView _mpAdView = new MoPubSDK.MPAdView(Ads.MoPubconfig.AD_UNIT_ID_BANNER, size);

            _mpAdView.Frame = new System.Drawing.RectangleF(0, 0, 320, 50);
            _mpAdView.LoadAd();
            viewLeaderBoard.Add(_mpAdView);
            //_mpAdView.Delegate
            var a = new MoPubSDK.MPAdViewDelegate();

            var bindingSet = this.CreateBindingSet<CrescimentoView, CrescimentoViewModel>();

			_source = new CrescimentosTableViewSource(tableView);
			tableView.Source = _source;

			bindingSet.Bind(_source).To(vm => vm.Crescimentos);
			bindingSet.Bind(_source).For(s => s.SelectionChangedCommand).To(vm => vm.AtualizarPesoAlturaCommand).OneWay();

			bindingSet.Apply();

			GraphHelper.InitPlot(out _hostView, out _graph, out _dataSource, graphArea.Frame, 4, true);
			graphArea.AddSubview(_hostView);

			UpdateIcon();

			var mvxNotifyPropertyChanged = ViewModel as MvxNotifyPropertyChanged;
			if(mvxNotifyPropertyChanged != null)
			{
				mvxNotifyPropertyChanged.PropertyChanged += OnPropertyChanged;
			}
		}

		#endregion

		#region Methods

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName == "Crescimentos")
			{
				UpdateIcon();
			}
		}

		private void ReceiveCrescimentosUpdatedMessage(CrescimentosUpdatedMessage obj)
		{
			if(_graph != null)
			{
				_dataSource.UpdateValues();
				_graph.ReloadData();
				GraphHelper.UpdatePlotScape(_hostView);
			}
		}

		private void UpdateIcon()
		{
			tableView.ReloadData();

			var baseTabView = NavigationController.ParentViewController as BaseTabView;
			if(baseTabView != null && baseTabView.ViewControllers != null)
			{
				TabBarItem.BadgeValue = CrescimentoHelpers.PrecisaAtualizar() ? "!" : null;
			}
		}

		#endregion
	}

}
