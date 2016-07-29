using Cirrious.MvvmCross.Plugins.Messenger;

namespace DS.Kids.Apps.Core.Messages
{

	public class CurrentUserChangedMessage : MvxMessage
	{

		public CurrentUserChangedMessage(object sender)
			: base(sender)
		{
		}

	}

}
