using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Messages
{

    public class RefreshDiarioMessage : MvxMessage
    {

        public TipoGrupoRefeicao TipoGrupoRefeicao { get; private set; }

        public RefreshDiarioMessage(object sender, TipoGrupoRefeicao tipoGrupoRefeicao)
            : base(sender)
        {
            TipoGrupoRefeicao = tipoGrupoRefeicao;
        }

    }

}