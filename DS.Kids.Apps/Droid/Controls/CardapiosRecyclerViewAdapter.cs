using System;
using System.Collections;

using DS.Kids.Apps.Core.ViewModels;
using System.Collections.ObjectModel;

namespace DS.Kids.Apps.Droid.Controls
{

	public class CardapiosRecyclerViewAdapter : HeaderFooterRecyclerViewAdapter<CardapiosViewModel.CardapioRefeicao>
	{
		#region Properties

		protected override Func<CardapiosViewModel.CardapioRefeicao, IEnumerable> GetInternalList
		{
			get
			{
				return cardapioRefeicao => cardapioRefeicao;
			}
		}

		#endregion
	}

}
