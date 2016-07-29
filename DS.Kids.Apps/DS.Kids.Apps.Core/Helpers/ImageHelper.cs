using System;
using System.IO;

namespace DS.Kids.Apps.Core.Helpers
{

	public static class ImageHelper
	{

		public static string ByteArrayToZippedString64(byte[] bytes)
		{
			if (bytes == null)
			{
				return "";
			}

			string textFile;

			using(MemoryStream msZipped = new MemoryStream())
			{
				using (MemoryStream msTextFile = new MemoryStream(bytes))
				{
					ICSharpCode.SharpZipLib.BZip2.BZip2.Compress(msTextFile, msZipped, true, 9);
					byte[] zippedTextFile = msZipped.ToArray();
					byte[] returnTextFile = new byte[zippedTextFile.Length + 4];

					using(var bw = new BinaryWriter(new MemoryStream(returnTextFile)))
					{
						bw.Write(bytes.Length);
						bw.Write(zippedTextFile);
					}

					textFile = Convert.ToBase64String(returnTextFile);
				}
			}

			return textFile;
		}
	}

}
