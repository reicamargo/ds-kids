using BRFX.Core.ViewModels;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Resources;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class PoliticaPrivacidadeViewModel : BaseViewModel
	{
		#region Fields

		private readonly string _url = "http://www.dietaesaude.com.br/politicas/";

		#endregion

		#region Constructors and Destructors

		public PoliticaPrivacidadeViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("PoliticaPrivacidadeView");
		}

		#endregion

		#region Public Properties

		public string Url
		{
			get
			{
				return _url;
			}
		}

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		#endregion
	}

}
