using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.IO;

namespace DS.Kids.API.Models
{
    public class ImageUnzip
    {
        public static Byte[] Unzip(string imageBase64)
        {
            if (string.IsNullOrWhiteSpace(imageBase64))
                return null;

            var zippedImageFile = System.Convert.FromBase64String(imageBase64);
            using (var ms = new MemoryStream(zippedImageFile))
            {
                using (var br = new BinaryReader(ms))
                {
                    int length = br.ReadInt32();    //read the length first
                    byte[] bytesUncompressed = new byte[length]; //you can convert this back to the object using a MemoryStream :wink:

                    using (BZip2InputStream zis = new BZip2InputStream(ms))
                    {
                        zis.Read(bytesUncompressed, 0, length); //read the decompressed file                        
                    }
                    return bytesUncompressed;
                }
            }
        }
    }
}