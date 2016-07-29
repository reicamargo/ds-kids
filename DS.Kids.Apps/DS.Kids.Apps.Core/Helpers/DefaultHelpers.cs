using System.Collections.ObjectModel;

using BRFX.Core.Validation;

namespace DS.Kids.Apps.Core.Helpers
{

	public static class DefaultHelpers
	{

		public static ObservableCollection<decimal> GetAlturasPossiveis(object viewModel)
		{
			var attrAltura = Validator.GetValidationAttribute<RangeAttribute>(viewModel, "Altura");
			var alturasPossiveis = new ObservableCollection<decimal>();
			for(var i = (double)attrAltura.Minimum; i <= (double)attrAltura.Maximum; i += 0.01)
			{
				alturasPossiveis.Add((decimal)i);
			}
			return alturasPossiveis;
		}

		public static ObservableCollection<decimal> GetPesosPossiveis(object viewModel)
		{
			var attrPeso = Validator.GetValidationAttribute<RangeAttribute>(viewModel, "Peso");
			var pesosPossiveis = new ObservableCollection<decimal>();
			for(var i = (double)attrPeso.Minimum; i <= (double)attrPeso.Maximum; i += 1)
			{
				pesosPossiveis.Add((int)i);
			}
			return pesosPossiveis;
		}

	}

}
