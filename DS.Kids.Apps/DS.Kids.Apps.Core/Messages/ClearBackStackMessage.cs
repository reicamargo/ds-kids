using Cirrious.MvvmCross.Plugins.Messenger;

namespace DS.Kids.Apps.Core.Messages
{

	public class ClearBackStackMessage : MvxMessage
	{
		#region Constructors and Destructors

		public ClearBackStackMessage(object sender)
			: base(sender)
		{
		}

		#endregion
	}

}
