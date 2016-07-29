using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Repositories = DS.Kids.Model.Repositories;

namespace DS.Kids.API.Controllers
{
    public class ImagensController : ApiController
    {
        public async Task<HttpResponseMessage> Get(string entidade, string hash)
        {
            var image = new Models.ImageHandler(entidade, hash);
            var result = await image.GetAsync();
            return result;
        }
    }
}