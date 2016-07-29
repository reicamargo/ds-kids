using BRFX.Core.ViewModels;

using Cirrious.CrossCore;

using DS.Kids.Apps.Core.Analytics;
using DS.Kids.Apps.Core.Resources;
using DS.Kids.Model;
using System.Collections.Generic;
using System.Linq;

namespace DS.Kids.Apps.Core.ViewModels
{

	public class BrincadeiraViewModel : BaseViewModel<Brincadeira>
	{
		#region Fields

		private Brincadeira _brincadeira;

		#endregion

		#region Constructors and Destructors

		public BrincadeiraViewModel()
		{
			var analytics = Mvx.Resolve<IAnalytics>();
			analytics.SendView("BrincadeiraView");
		}

		#endregion

		#region Public Properties

		public Brincadeira Brincadeira
		{
			get
			{
				return _brincadeira;
			}
			private set
			{
				SetProperty(ref _brincadeira, value);
			}
		}

		private List<BrincadeiraItemList> _brincadeiraEnumerable;
		public List<BrincadeiraItemList> BrincadeiraEnumerable
		{
			get
			{
			    return _brincadeiraEnumerable ??
			           (_brincadeiraEnumerable = new List<BrincadeiraItemList>
			                                         {
			                                             new BrincadeiraItemList(Brincadeira.Materiais, Brincadeira),
			                                             new BrincadeiraItemList(Brincadeira.Objetivos, Brincadeira),
			                                             new BrincadeiraItemList(Brincadeira.Instrucoes, Brincadeira)
			                                         });
			}
		}



		#endregion

		#region Public Methods and Operators

		public override string GetResourceStringForIndex(string index)
		{
			return AppResources.ResourceManager.GetString(index);
		}

		#endregion

		#region Methods

		/// <summary>
		///     Obtém os parâmetros enviado para este view model.
		/// </summary>
		protected override void GetParams(Brincadeira brincadeira)
		{
			Brincadeira = brincadeira;
		}

		#endregion
	}

	public class BrincadeiraItemList : List<BrincadeiraItem>
	{
		public BrincadeiraItemType Tipo { get; set;}

		public TipoAmbiente Ambiente { get; set; }

		public string FaixaEtaria { get; set; }

		public string Titulo {
			get { 
				switch (Tipo) {

					case BrincadeiraItemType.Instrucao:
						return "Instruções";
					case BrincadeiraItemType.Material:
						return "Material Necessário";
					case BrincadeiraItemType.Objetivo:
						return "Objetivos";
					default:
						return "";
				}
			}
		}

		public BrincadeiraItemList(ICollection<Material> material, Brincadeira brincadeira)
			:base(material.Select(m => new BrincadeiraItem(m)))
		{
			Tipo = BrincadeiraItemType.Material;
			Ambiente = brincadeira.Ambiente;
			FaixaEtaria = brincadeira.FaixaEtaria;
		}
		
		public BrincadeiraItemList(ICollection<Objetivo> objetivo, Brincadeira brincadeira)
			:base(objetivo.Select(o => new BrincadeiraItem(o)))
		{
			Tipo = BrincadeiraItemType.Objetivo;
			Ambiente = brincadeira.Ambiente;
			FaixaEtaria = brincadeira.FaixaEtaria;
		}

		public BrincadeiraItemList(string instrucao, Brincadeira brincadeira)
			:base(new List<BrincadeiraItem>
					  { new BrincadeiraItem(instrucao) })
		{
			Tipo = BrincadeiraItemType.Instrucao;
			Ambiente = brincadeira.Ambiente;
			FaixaEtaria = brincadeira.FaixaEtaria;
		}
	}

	public class BrincadeiraItem
	{
		public string Descricao { get; set; }
		public BrincadeiraItemType Tipo { get; set; }
		
		public BrincadeiraItem(Material material)
		{
			Descricao = material.Descricao;
			Tipo = BrincadeiraItemType.Material;
		}

		public BrincadeiraItem(Objetivo objetivo)
		{
			Descricao = objetivo.Descricao;
			Tipo = BrincadeiraItemType.Objetivo;
		}

		public BrincadeiraItem(string instrucoes)
		{
			Descricao = instrucoes;
			Tipo = BrincadeiraItemType.Instrucao;
		}
	}

	public enum BrincadeiraItemType
	{
		Material,
		Objetivo,
		Instrucao
	}
}
