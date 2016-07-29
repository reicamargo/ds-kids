using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DS.Kids.API.Filters
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if(actionContext.Request.Headers.Authorization == null)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
                
            var chave = actionContext.Request.Headers.Authorization.Parameter;
            var tokenValido = await TokenValidoAsync(chave);
            if (!tokenValido)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        private static async Task<bool> TokenValidoAsync(string chave)
        {
            var repositories = new DS.Kids.Model.Repositories.Tokens();
            var service = new DS.Kids.Model.Services.Token(repositories);
            var valido = await service.TokenValidoAsync(chave);
            if (valido)
            {
                // TODO: Thread.CurrentPrincipal = 
            }
            return valido;
        }
    }
}