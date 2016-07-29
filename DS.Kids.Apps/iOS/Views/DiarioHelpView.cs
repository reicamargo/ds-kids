using System;

using BRFX.Core.IOS.Views;

using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.iOS.Views
{
    partial class DiarioHelpView : BaseView, IMvxModalTouchView
    {
        #region Constructors and Destructors

        public DiarioHelpView(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            NavigationController.NavigationBarHidden = true;
            base.ViewWillAppear(animated);
        }

        public override sealed void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<DiarioHelpView, DiarioHelpViewModel>();

            bindingSet.Bind(btnClose).To(vm => vm.GoBackCommand);

            bindingSet.Apply();
        }

        #endregion
    }
}
