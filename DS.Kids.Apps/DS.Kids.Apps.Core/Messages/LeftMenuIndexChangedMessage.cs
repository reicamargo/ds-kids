using Cirrious.MvvmCross.Plugins.Messenger;

using DS.Kids.Apps.Core.ViewModels;

namespace DS.Kids.Apps.Core.Messages
{

	public class LeftMenuIndexChangedMessage : MvxMessage
	{
		#region Constructors and Destructors

		public LeftMenuIndexChangedMessage(object sender, LeftMenuViewModel.LeftMenuIndex leftMenuIndex)
			: base(sender)
		{
			LeftMenuIndex = leftMenuIndex;
		}

		#endregion

		#region Public Properties

		public LeftMenuViewModel.LeftMenuIndex LeftMenuIndex { get; private set; }

		#endregion
	}

}
