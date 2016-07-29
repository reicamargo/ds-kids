using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Messages
{

	public class NewCrescimentoMessage : MvxMessage
	{

		public NewCrescimentoMessage(object sender, Crescimento crescimento)
			: base(sender)
		{
			Crescimento = crescimento;
		}

		public Crescimento Crescimento { get; private set; }

	}

}
