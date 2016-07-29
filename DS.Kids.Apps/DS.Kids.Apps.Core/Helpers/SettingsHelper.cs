using System;
using System.Reflection;

namespace DS.Kids.Apps.Core.Helpers
{

	public static class SettingsHelper
	{

		public const string FacebookAppId = "405153029664649";

		public const string FacebookPermissions = "public_profile,email";

		public const string GoogleAnalyticsId = "UA-446223-43";

		public const string GoogleAnalyticsViewPrefix = "/mobile/DSKids/";

		public const string ParseApplicationId = "Hw5nLTl7oyqumw3UFPwq4yohnmX1oZNxBkx5MbIu";

		public const string ParseDotNetKey = "w42qIFwybDM3JZnUmHtex0SK7QxgvNuhcCLQtIbC";

		public const string CrashlyticsApiKey = "d0b597577cc0dccc16efeb076f4683b41313a075";
		
		public static Version Version
		{
			get
			{
				return typeof(SettingsHelper).GetTypeInfo().Assembly.GetName().Version;
			}
		}

	}

}
