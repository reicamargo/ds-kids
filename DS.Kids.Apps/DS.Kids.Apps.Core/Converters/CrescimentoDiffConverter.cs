using System;
using System.Globalization;

using Cirrious.CrossCore.Converters;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Converters
{

	public class CrescimentoDiffConverter : MvxValueConverter<Crescimento, string>
	{
		#region Methods

		protected override string Convert(Crescimento value, Type targetType, object parameter, CultureInfo culture)
		{
//
//			viewSource.Crescimentos = new System.Collections.ObjectModel.ObservableCollection<Crescimento> ();
//	
//			var index = viewSource.Crescimentos.IndexOf(value);
//
//			if(index < viewSource.Crescimentos.Count - 1)
//			{
//				var previousCrescimento = viewSource.Crescimentos.ElementAt(index + 1);
//
//				return string.Format("{0:+0.#;-0.#;+0}kg", value.Peso - previousCrescimento.Peso);
//			}
//
			return "";
		}

		#endregion
	}

}
