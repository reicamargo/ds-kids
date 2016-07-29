using System;
using System.Collections;

using DS.Kids.Apps.Core.ViewModels;
using BRFX.Core.Droid.Controls;
using Cirrious.MvvmCross.Droid.RecyclerView;

using DS.Kids.Model;
using Cirrious.CrossCore;
using BRFX.Core.MessageBox;

namespace DS.Kids.Apps.Droid.Controls
{

	public class DiarioRefeicaoRecyclerViewAdapter : HeaderFooterRecyclerViewAdapter<RefeicaoGrupo>
	{
		#region Properties

		protected override Func<RefeicaoGrupo, IEnumerable> GetInternalList
		{
			get
			{
				return grupoAlimentarRefeicao => grupoAlimentarRefeicao.Alimentos;
			}
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder (Android.Views.ViewGroup parent, int viewType)
		{
			var viewHolder = base.OnCreateViewHolder (parent, viewType);
			var check = viewHolder.ItemView.FindViewById<BRFXSVGImageView>(Resource.Id.diarioRefeicao_check);

            if (check != null)
            {
                check.Click += (sender, e) =>
                {
                    var mvxRecyclerViewHolder = viewHolder as MvxRecyclerViewHolder;
                    if (mvxRecyclerViewHolder != null)
                    {
                        var grupoAlimentarRefeicao = mvxRecyclerViewHolder.DataContext as RefeicaoGrupo;
                        if (grupoAlimentarRefeicao != null)
                        {
                            var diarioRefeicaoViewModel = DiarioRefeicaoViewModel.Instance;
                            if (diarioRefeicaoViewModel != null && diarioRefeicaoViewModel.CheckBoxCommand.CanExecute(grupoAlimentarRefeicao))
                            {
                                diarioRefeicaoViewModel.CheckBoxCommand.Execute(grupoAlimentarRefeicao);
                            }
                        }
                    }
                };
            }

			return viewHolder;
		}

		#endregion
	}
}
