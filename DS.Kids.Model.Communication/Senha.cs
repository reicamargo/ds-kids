using DS.Kids.Model.Communication.Support;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
    public class Senha : Services.ISenha
    {
        public async Task<Model.Result> TrocaAsync(Model.TrocaDeSenha trocaDeSenha)
        {
            var result = await Rest.PostAsync<Model.Result, Model.TrocaDeSenha>(Endpoints.TROCA_DE_SENHA, trocaDeSenha);
            return result;
        }

        public async Task<Result> EsqueciAsync(string email)
        {
            var esqueciMinhaSenha = new Model.Login.EsqueciMinhaSenha { Email = email };
            var result = await Rest.PostAsync<Model.Result, Model.Login.EsqueciMinhaSenha>(Endpoints.ESQUECI_MINHA_SENHA, esqueciMinhaSenha);
            return result;
        }
    }
}
