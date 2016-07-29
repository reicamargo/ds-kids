using System;

using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;

using CoreGraphics;

using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class CardapiosView : BaseHomeChildView
	{
		#region Fields

		private readonly MvxSubscriptionToken _cardapioChangedToken;

		#endregion

		#region Constructors and Destructors

		public CardapiosView(IntPtr handle)
			: base(handle)
		{
			this.OnViewCreate();

			var messenger = Mvx.Resolve<IMvxMessenger>();
			_cardapioChangedToken = messenger.Subscribe<CardapioChangedMessage>(CardapioChangedMessageMessage);
		}

		private void CardapioChangedMessageMessage(CardapioChangedMessage obj)
		{
			if (tableView != null)
			{
				tableView.ReloadData();
			}
		}

		#endregion

		#region Public Methods and Operators

		public override void OnUnload()
		{
			base.OnUnload();

			if(_cardapioChangedToken != null)
			{
				var messenger = Mvx.Resolve<IMvxMessenger>();
				messenger.Unsubscribe<CardapioChangedMessage>(_cardapioChangedToken);
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindingSet = this.CreateBindingSet<CardapiosView, CardapiosViewModel>();

			var topview = new UIView(new CGRect(0, -480, 320, 480))
							{
								BackgroundColor = UIColor.FromRGB(238, 237, 243)
							};

			tableView.BackgroundView = topview;

			var source = new CardapiosTableViewSource(tableView);
			tableView.Source = source;

			bindingSet.Bind(source).To(vm => vm.Cardapio);
			bindingSet.Bind(source).For(s => s.OutraSugestaoCommand).To(vm => vm.OutraSugestaoCommand).OneWay();
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.DicaCommand).OneWay();

			bindingSet.Apply();

			tableView.ReloadData();
		}

		#endregion
	}

}
