using System;
using BRFX.Core.MessageBox;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.File;

namespace DS.Kids.Apps.Core.Helpers
{
	public static class ErrorHelpers
	{

		private const string DefaultErrorTitle = "Não foi possível iniciar o DS Kids. Verifique sua conexão com a internet e tente novamente.";

		public static void Log(this IMessageBox messageBox, Exception exception)
		{
			messageBox.Show(DefaultErrorTitle);
			SaveLog(exception.ToString());
		}

		public static void Log(this IMessageBox messageBox, string resultMessage, string title = DefaultErrorTitle)
		{
			messageBox.Show(resultMessage, null, title);
			SaveLog(resultMessage);
		}

		private static void SaveLog(string error)
		{
			IMvxFileStore fileManager;
			if (Mvx.TryResolve(out fileManager))
			{
				fileManager.WriteFile(_errorFile, string.Format("{0}: {1}", DateTime.Now, error));
			}
		}

		private static string _errorFile = "OpppsssError.dat";
	}
}
