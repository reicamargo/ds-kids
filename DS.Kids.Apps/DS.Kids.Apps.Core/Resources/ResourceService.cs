namespace DS.Kids.Apps.Core.Resources
{
	public class ResourceService
	{
		public static AppResources Resources;

		public AppResources LocalizedResources
		{
			get
			{
				return Resources ?? (Resources = new AppResources());
			}
		}
	}
}
