using System;
using System.Globalization;
using System.Linq;

using Cirrious.CrossCore.Converters;

using DS.Kids.Apps.Core.ViewModels;
using DS.Kids.Model;

namespace DS.Kids.Apps.Droid.Converters
{

	public class CrescimentoPesoConverter : MvxValueConverter<Crescimento, string>
	{
		#region Methods

		protected override string Convert(Crescimento value, Type targetType, object parameter, CultureInfo culture)
		{
			string diff = "";

			var index = CrescimentoViewModel.StaticCrescimentos.IndexOf(value);

			if (index < CrescimentoViewModel.StaticCrescimentos.Count - 1) {
								
				Crescimento previousCrescimento = (Crescimento) CrescimentoViewModel.StaticCrescimentos.ElementAt(index + 1);

				diff = string.Format ("{0:+0.#;-0.#;+0}kg", value.Peso - previousCrescimento.Peso);
			}

			return diff;
		}

		#endregion
	}

}
