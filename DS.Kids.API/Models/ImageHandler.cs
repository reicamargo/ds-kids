using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Models
{
    public class ImageHandler
    {
        private readonly string _entity;
        private readonly int _id;
        private readonly string _hash;

        public ImageHandler(string entity, string hash)
        {
            Throw.IfIsNullOrEmpty(entity);
            Throw.IfIsNullOrEmpty(hash);

            this._entity = entity;
            this._id = this.GetId(hash);
            this._hash = hash;
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            if (this._id == 0)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var image = await this.GetImageContentAsync();
            if (image == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var ms = new MemoryStream(image);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(ImageMedia.MEDIA_TYPE);
            return response;
        }

        private async Task<byte[]> GetImageContentAsync()
        {
            var ioc = new UnityContainer();
            UnityConfig.RegisterRepositoriesTypes(ioc);

            switch (this._entity)
            {
                case "categorias":
                    return await this.GetCategoriaImageAsync(ioc);
                case "brincadeiras":
                    return await this.GetBrincadeirasImageAsync(ioc);
                case "criancas":
                    return await this.GetCriancasImageAsync(ioc);
                case "parceiros":
                    return await this.GetParceirosImageAsync(ioc);
                case "paragrafos":
                    return await this.GetParagrafosImageAsync(ioc);
                default:
                    return null;
            }
        }

        private async Task<byte[]> GetParagrafosImageAsync(UnityContainer ioc)
        {
            var repository = ioc.Resolve<Repositories.IParagrafos>();
            var entity = await repository.ObterPorIdAsync(this._id);
            if (entity == null) return null;
            return entity.Imagem;
        }

        private async Task<byte[]> GetCategoriaImageAsync(UnityContainer ioc)
        {
            var repository = ioc.Resolve<Repositories.ICategorias>();
            var entity = await repository.ObterPorIdAsync(this._id);
            if (entity == null) return null;
            return entity.Imagem;
        }

        private async Task<byte[]> GetBrincadeirasImageAsync(UnityContainer ioc)
        {
            var repository = ioc.Resolve<Repositories.IBrincadeiras>();
            var entity = await repository.ObterPorIdAsync(this._id);
            if (entity == null) return null;
            return entity.Imagem;
        }

        private async Task<byte[]> GetCriancasImageAsync(UnityContainer ioc)
        {
            var repository = ioc.Resolve<Repositories.ICriancas>();
            var entity = await repository.ObterPorIdAsync(this._id);
            if (entity == null) return null;
            return entity.Imagem;
        }

        private async Task<byte[]> GetParceirosImageAsync(UnityContainer ioc)
        {
            var repository = ioc.Resolve<Repositories.IParceiros>();
            var entity = await repository.ObterPorIdAsync(this._id);
            if (entity == null) return null;
            if (this._hash.Contains("_ico"))
                return entity.Icone;
            return entity.Imagem;
        }

        private int GetId(string hash)
        {
            string[] vet = hash.Split('_');
            if (vet.Length > 0)
                return Int32.Parse(vet[0]);

            return 0;
        }
    }
}