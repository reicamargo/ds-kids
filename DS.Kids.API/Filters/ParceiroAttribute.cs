using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DS.Kids.API.Filters
{
    public class ParceiroAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //7 -> DS default
            int parceiro = 7;

            IEnumerable<string> valores;
            if (actionContext.Request.Headers.TryGetValues("Parceiro", out valores))
            {
                Int32.TryParse(valores.FirstOrDefault(), out parceiro);
            }

            Model.ParceiroSingleton.Instance.Inserir(parceiro);
        }
    }
}