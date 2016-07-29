using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Apps.iOS.Controls;

using Foundation;

using UIKit;

namespace DS.Kids.Apps.iOS.Views
{
    partial class DiarioAlimentosView : ProgressView
    {
        public DiarioAlimentosView(IntPtr handle)
            : base(handle)
        {
            this.OnViewCreate();
        }

        #region Public Methods and Operators

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);
            base.TouchesBegan(touches, evt);
        }

        public override void OnUnload()
        {
            base.OnUnload();

            UnregisterNotifications();
        }

        private static void UnregisterNotifications()
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillHideNotification);
            NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillShowNotification);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<DiarioAlimentosView, DiarioAlimentosViewModel>();

            searchBar.Translucent = false;
            searchBar.BackgroundImage = new UIImage();
            searchBar.BarTintColor = new UIColor(226 / 255.0f, 226 / 255.0f, 226 / 255.0f, 1.0f);

            UITextField searchField = null;
            foreach (var subView in searchBar.Subviews)
            {
                foreach (var ndLeveSubView in subView.Subviews)
                {
                    var textField = ndLeveSubView as UITextField;
                    if (textField != null)
                    {
                        searchField = textField;
                        break;
                    }
                }
            }
            if (searchField != null)
            {
                searchField.TextColor = new UIColor(51 / 255.0f, 51 / 255.0f, 51 / 255.0f, 1);
                searchField.BackgroundColor = UIColor.White;
            }

            var source = new DiarioAlimentosTableViewSource(tableView);
            tableView.Source = source;

            var tapGesture = new UITapGestureRecognizer(r =>
            {
                if (searchBar.IsFirstResponder)
                {
                    searchBar.ResignFirstResponder();
                }
            })
            {
                CancelsTouchesInView = false
            };
            tableView.AddGestureRecognizer(tapGesture);

            OnKeyboardChanged(false, 0);

            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);

            searchBar.SearchButtonClicked += (sender, args) =>
            {
                searchBar.ResignFirstResponder();
                SearchAlimentosByText();
            };
            searchBar.TextChanged += (sender, args) => SearchAlimentosByText();
            searchBar.CancelButtonClicked += (sender, args) => { searchBar.ResignFirstResponder(); };

            bindingSet.Bind(this).For(v => v.Title).To(vm => vm.RefeicaoGrupo.TipoGrupoRefeicao).WithConversion("TipoGrupoRefeicaoText");

            bindingSet.Bind(tableView).For(v => v.Hidden).To(vm => vm.Alimentos.Count).WithConversion("Visibility");
            bindingSet.Bind(ResultadoNaoEncontradoView).For(v => v.Hidden).To(vm => vm.Alimentos.Count).WithConversion("InvertedVisibility");
            bindingSet.Bind(ResultadoNaoEncontradoLabel).For(v => v.Hidden).To(vm => vm.ProgressVisible).WithConversion("InvertedVisibility");
            bindingSet.Bind(DescricaoView).For(v => v.Hidden).To(vm => vm.DescricaoVisible).WithConversion("Visibility");
            bindingSet.Bind(ConteudoView).For(v => v.Hidden).To(vm => vm.DescricaoVisible).WithConversion("InvertedVisibility");
            //bindingSet.Bind(searchBar).To(vm => vm.SearchQuery);
            bindingSet.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.AlimentoSelectedCommand);
            bindingSet.Bind(source).To(vm => vm.Alimentos);

            bindingSet.Apply();

            tableView.ReloadData();
        }

        private void SearchAlimentosByText()
        {
            var viewModel = ViewModel as DiarioAlimentosViewModel;
            if (viewModel != null && viewModel.SearchCommand != null)
            {
                viewModel.SearchQuery = searchBar.Text;
                viewModel.SearchCommand.Execute();
            }
        }

        #endregion

        #region Methods

        private void OnKeyboardChanged(bool visible, nfloat keyboardHeight)
        {
            nfloat height = 49;

            if (visible)
            {
                height = keyboardHeight;
            }

            tableView.ContentInset = new UIEdgeInsets(0, 0, height, 0);
            tableView.ScrollIndicatorInsets = new UIEdgeInsets(44, 0, height, 0);
        }

        private void OnKeyboardNotification(NSNotification notification)
        {
            try
            {
                if(!IsViewLoaded)
                {
                    return;
                }
            }
            catch
            {
                UnregisterNotifications();
                return;
            }

            var visible = notification.Name == UIKeyboard.WillShowNotification;

            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            var keyboardFrame = visible
                                    ? UIKeyboard.FrameEndFromNotification(notification)
                                    : UIKeyboard.FrameBeginFromNotification(notification);
            OnKeyboardChanged(visible, keyboardFrame.Height);

            UIView.CommitAnimations();
        }

        #endregion
    }
}
