using System;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;

using CoreGraphics;

using DS.Kids.Apps.Core.Converters;
using DS.Kids.Apps.Core.Messages;
using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{

	partial class DiarioView : BaseHomeChildView
	{
		#region Constructors and Destructors

		private MvxSubscriptionToken _refreshDiarioToken;

		private MvxSubscriptionToken _showDiarioCalendarToken;

		private UIView _datePickerView;

		private UIDatePicker _datePicker;

		public DiarioView(IntPtr handle)
			: base(handle)
		{
			this.OnViewCreate();
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			View.EndEditing(true);
			base.TouchesBegan(touches, evt);
		}

		public override void OnLoad()
		{
			base.OnLoad();

			var diarioViewModel = ViewModel as DiarioViewModel;
			if (diarioViewModel != null)
			{
				_refreshDiarioToken = diarioViewModel.Messenger.SubscribeOnMainThread<RefreshDiarioMessage>(ReceiveRefreshDiarioMessage);
				_showDiarioCalendarToken = diarioViewModel.Messenger.SubscribeOnMainThread<ShowDiarioCalendarMessage>(ReceiveShowDiarioCalendarMessage);
			}
		}

		private void ReceiveShowDiarioCalendarMessage(ShowDiarioCalendarMessage obj)
		{
			var diarioViewModel = ViewModel as DiarioViewModel;
			if(diarioViewModel != null)
			{
				_datePicker.Date = NSDate.FromTimeIntervalSinceReferenceDate(
					(diarioViewModel.Data - (new DateTime(2001, 1, 1, 0, 0, 0))).TotalSeconds);
			}

			UIView.BeginAnimations(null);
			UIView.SetAnimationDuration(0.3);
			_datePickerView.Frame = new CGRect(0, View.Bounds.Bottom - TabBarController.TabBar.Frame.Height - _datePickerView.Frame.Size.Height, 320, _datePickerView.Frame.Size.Height);
			UIView.CommitAnimations();
		}

		private void DismissPicker()
		{
			UIView.BeginAnimations(null);
			UIView.SetAnimationDuration(0.3);
			_datePickerView.Frame = new CGRect(0, View.Bounds.Bottom - TabBarController.TabBar.Frame.Height, 320, 258);
			UIView.CommitAnimations();
		}

		private void ReceiveRefreshDiarioMessage(RefreshDiarioMessage obj)
		{
			if (tableView != null)
			{
				tableView.ReloadData();
			}
		}

		public override void OnUnload()
		{
			var diarioViewModel = ViewModel as DiarioViewModel;
			if (diarioViewModel != null)
			{
				diarioViewModel.Messenger.Unsubscribe<RefreshDiarioMessage>(_refreshDiarioToken);
				diarioViewModel.Messenger.Unsubscribe<ShowDiarioCalendarMessage>(_showDiarioCalendarToken);
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

			var bindingSet = this.CreateBindingSet<DiarioView, DiarioViewModel>();

			var calendarioButton = new UIButton(UIButtonType.Custom)
			{
				Frame = new CGRect(0, 0, 21, 23),
				ShowsTouchWhenHighlighted = true,
				ImageEdgeInsets = new UIEdgeInsets(0, 0, 0, 0)
			};
			calendarioButton.SetBackgroundImage(UIImage.FromBundle("ico_calendar"), UIControlState.Normal);
			NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(calendarioButton), true);

			bindingSet.Bind(calendarioButton).To(vm => vm.CalendarioCommand);

			_datePickerView = new UIView(new CGRect(0, View.Bounds.Bottom - TabBarController.TabBar.Frame.Height, 320, 258))
								  {
									  BackgroundColor = UIColor.White
								  };
			
			_datePicker = new UIDatePicker
			{
				Mode = UIDatePickerMode.Date,
				MaximumDate = (NSDate)DateTime.Now,
				Frame = new CGRect(0, 44, 320, 216),
				TimeZone = NSTimeZone.LocalTimeZone
			};
			_datePickerView.Add(_datePicker);

			AddToolbar();

			View.AddSubview(_datePickerView);

			//var topview = new UIView(new CGRect(0, -480, 320, 480))
			//{
			//	BackgroundColor = new UIColor(1, 159 / 255.0f, 27 / 255.0f, 1)
			//};

			//tableView.BackgroundView = topview;

			var diarioViewModel = ViewModel as DiarioViewModel;
			if (diarioViewModel != null)
			{
				int i = 0;
				var itemSize = new CGSize(70, 75);

				foreach (var carinha in diarioViewModel.Carinhas)
				{
					var carinhasString = string.Format("Carinhas[{0}].", (int)carinha.TipoGrupoRefeicao - 1);

					var pageLeft = ScrollView.Frame.Size.Width * (i / 4);
					var frame = new CGRect
									   {
										   X = pageLeft + 160 - (2 - i % 4) * itemSize.Width,
										   Y = 10,
										   Size = itemSize
									   };

					var subview = new UIView(frame);

					var imageView = new MvxImageView(new CGRect((frame.Width - 34) / 2 - 5, 0, 34, 35));
					subview.AddSubview(imageView);

					var yInicial = (imageView.Frame.Height - carinha.Max * 6) / 2;
					var frameBullet = new CGRect((frame.Width + 34) / 2, yInicial, 7, 5);
					for (int j = 0; j < carinha.Max; j++)
					{
						var bullet = new UIImageView(frameBullet)
										 {
											 Image = UIImage.FromBundle("ico_stats")
										 };
						subview.AddSubview(bullet);

						bindingSet.Bind(bullet).For(v => v.Alpha).To(carinhasString + "Count").WithConversion("DiarioBulletAlpha", j);

						frameBullet.Y += 6;
					}

					var label = new UILabel(new CGRect(0, 32, frame.Width, frame.Height - 32))
									{
										Lines = 0,
										LineBreakMode = UILineBreakMode.WordWrap,
										Font = UIFont.SystemFontOfSize(10),
										TextColor = UIColor.White,
										TextAlignment = UITextAlignment.Center,
										Text = TipoGrupoRefeicaoTextConverter.Convert(carinha.TipoGrupoRefeicao)
									};
					var newSize = label.SizeThatFits(label.Frame.Size);
					label.Frame = new CGRect(label.Frame.GetMidX() - newSize.Width / 2,
						label.Frame.Y + 5,
						newSize.Width,
						newSize.Height);
					subview.AddSubview(label);

					bindingSet.Bind(imageView).For(v => v.ImageUrl).To(carinhasString + ".Carinha").WithConversion("CarinhaImage");

					ScrollView.AddSubview(subview);

					i++;
				}

				ScrollView.ContentSize = new CGSize(ScrollView.Frame.Size.Width * 2, ScrollView.Frame.Size.Height);

				var diarioHelpButton = new UIButton(new CGRect(CGPoint.Empty, ScrollView.ContentSize));
				bindingSet.Bind(diarioHelpButton).To(vm => vm.HelpCommand).OneWay();
				ScrollView.AddSubview(diarioHelpButton);
			}

			ScrollView.Scrolled += ScrollViewOnScrolled;

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

            var source = new DiarioTableViewSource(tableView);
			tableView.Source = source;

			bindingSet.Bind(source).To(vm => vm.RefeicoesDiario);
			bindingSet.Bind(source).For(s => s.SemaforoCommand).To(vm => vm.SemaforoCommand).OneWay();
			bindingSet.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.RefeicaoDiarioSelectedCommand).OneWay();

			bindingSet.Apply();

			tableView.ReloadData();
		}

		private void AddToolbar()
		{
			var inputAccessoryView = new UIToolbar
										 {
											 BarStyle = UIBarStyle.BlackOpaque,
											 Frame = new CGRect(0, 0, 320, 44)
										 };

			var cancelItem = new UIBarButtonItem("Cancelar", UIBarButtonItemStyle.Bordered, (sender, e) =>
			{
				DismissPicker();
			});
			var attributes = new UITextAttributes
								{
									TextColor = UIColor.White
								};
			cancelItem.SetTitleTextAttributes(attributes, UIControlState.Normal);

			var flexItem = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			var doneText = "Pronto";
			var doneItem = new UIBarButtonItem(doneText, UIBarButtonItemStyle.Done, (sender, e) =>
			{
				DismissPicker();
				var diarioViewModel = ViewModel as DiarioViewModel;
				if(diarioViewModel != null)
				{
					diarioViewModel.Data = (new DateTime(2001, 1, 1, 0, 0, 0)).AddSeconds(_datePicker.Date.SecondsSinceReferenceDate);
				}
			});
			doneItem.SetTitleTextAttributes(attributes, UIControlState.Normal);

			inputAccessoryView.SetItems(new[]
											{
												cancelItem, flexItem, doneItem
											}, true);

			_datePickerView.Add(inputAccessoryView);
		}

		private void ScrollViewOnScrolled(object sender, EventArgs eventArgs)
		{
			if (ScrollView.Dragging || ScrollView.Decelerating)
			{
				PageControl.CurrentPage = (int)(ScrollView.ContentOffset.X / (ScrollView.ContentSize.Width / PageControl.Pages));
			}
		}

		#endregion
	}

}
