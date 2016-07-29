namespace DS.Kids.Apps.Core.Analytics
{

	public interface IAnalytics
	{
		#region Public Methods and Operators

		void SendException(string description, bool isFatal);

		void SendView(string screenName);

		#endregion
	}

}
