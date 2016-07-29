using BRFX.Core.ViewModels;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Resources;

namespace DS.Kids.Apps.Core.ViewModels
{

    public class DiarioHelpViewModel : BaseViewModel
    {

        public DiarioHelpViewModel()
        {
            var analytics = Mvx.Resolve<IAnalytics>();
            analytics.SendView("DiarioHelpView");
        }

        public override string GetResourceStringForIndex(string index)
        {
            return AppResources.ResourceManager.GetString(index);
        }

    }

}