using System;
using System.Collections;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Droid.Controls
{

	public class BrincadeiraRecyclerViewAdapter : HeaderFooterRecyclerViewAdapter<BrincadeiraItemList>
	{
		#region Properties

		protected override Func<BrincadeiraItemList, IEnumerable> GetInternalList
		{
			get
			{
				return brincadeira => brincadeira;
			}
		}

		#endregion
	}

}
