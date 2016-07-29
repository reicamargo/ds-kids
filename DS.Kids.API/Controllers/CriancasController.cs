using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;
using Services = DS.Kids.Model.Services;
using DS.Kids.Model;

namespace DS.Kids.API.Controllers
{
    [Filters.Authorization]
    public class CriancasController : ApiController
    {
        private readonly Services.ICrianca _service;
        public CriancasController(Repositories.ICriancas criancas, Repositories.IResponsaveis responsaveis, Repositories.ICrescimentos crescimentos)
        {
            this._service = new DS.Kids.Model.Services.Crianca(criancas, responsaveis, crescimentos);
        }

        public async Task<Model.Result<Model.Crianca>> Post(int id, Model.Crianca crianca)
        {
            Models.ImageMedia.SetImageCrianca(crianca);
            var result = await this._service.InserirAsync(crianca);
            return result;
        }

        public async Task<Model.Result<Model.Crianca>> Put(int id, Model.Crianca crianca)
        {
            Models.ImageMedia.SetImageCrianca(crianca);
            var result = await this._service.AtualizarAsync(crianca);
            return result;
        }

        public async Task<Model.Result> Delete(int id)
        {
            var result = await this._service.ExcluirAsync(id);
            return result;
        }
    }
}
