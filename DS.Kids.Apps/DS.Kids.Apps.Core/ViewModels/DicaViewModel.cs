using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using BRFX.Core.MessageBox;
using BRFX.Core.ViewModels;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Helpers;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using DS.Kids.Model.Communication;
using DS.Kids.Model.Validations;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class DicaViewModel : ProgressViewModel<Dica>
	{
		#region Fields

		private Dica _dica;

		#endregion

		#region Constructors and Destructors

		public DicaViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("DicaView");
		}

		#endregion

		#region Public Properties

		public Dica Dica
		{
			get
			{
				return _dica;
			}
			private set
			{
				SetProperty(ref _dica, value);
			}
		}

		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		public void WebViewLoadStarted()
		{
			StartLoading();
		}

		public void WebViewLoadFinished()
		{
			StopLoading();
		}

		#endregion

		#region Methods

		protected override async void GetParams(Dica dica)
		{
			if(dica.Paragrafos == null || dica.Paragrafos.Count == 0)
			{
				await Task.Delay(100);

				var messageBox = Mvx.Resolve<IMessageBox>();

				StartLoading();
				try
				{
					var service = new Dicas();
					var result = await service.ObterPorIdAsync(dica.IdDica);

					if(result.ResultCode != ResultCodes.Success)
					{
						Debug.WriteLine("Error - Obter Dica: " + result.ResultMessage);
						messageBox.Log(result.ResultMessage);
						return;
					}

					if(result.Data != null)
					{
						var categoria = CategoriasViewModel.StaticCategorias.FirstOrDefault(c => c.IdCategoria == dica.Categoria.IdCategoria);
						result.Data.Categoria = categoria;
						Dica = result.Data;
					}
				}
				catch(Exception ex)
				{
					Debug.WriteLine("Error - Obter Dica: " + ex);
					messageBox.Log(ex);
				}
				finally
				{
					StopLoading();
				}
			}
			else
			{
				Dica = dica;
			}
		}

		#endregion
	}

}
