using DS.Kids.Model;
using System;

namespace DS.Kids.API.Models
{
    public static class ImageMedia
    {
        public const string MEDIA_TYPE = "image/jpeg";
        
        public static string GetRandomImageName()
        {
            return string.Format("{0}.jpg", DateTime.Now.Ticks);
        }

        public static void SetImageCrianca(Crianca crianca)
        {
            crianca.Imagem = Models.ImageUnzip.Unzip(crianca.ImagemZip);
            if (!string.IsNullOrEmpty(crianca.ImagemZip))
                crianca.NomeImagem = Models.ImageMedia.GetRandomImageName();
        }
    }
}