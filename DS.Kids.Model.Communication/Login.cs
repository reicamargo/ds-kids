using DS.Kids.Model.Communication.Support;
using System.Threading.Tasks;

namespace DS.Kids.Model.Communication
{
    public class Login : Services.ILogin
    {
        public async Task<Model.Result<Model.Responsavel>> LogarAsync(Model.Login login)
        {
            var result = await Rest.PostAsync<Model.Result<Model.Responsavel>, Model.Login>(Endpoints.LOGIN, login);
            Authorization.Singleton.SetToken(result);
            return result;
        }

        public async Task<Model.Result<Model.Responsavel>> LogarRedeSocialAsync(Model.LoginSocial loginSocial)
        {
            var result = await Rest.PostAsync<Model.Result<Model.Responsavel>, Model.LoginSocial>(Endpoints.LOGIN_SOCIAL, loginSocial);
            Authorization.Singleton.SetToken(result);
            return result;
        }

        public async Task<Model.Result> LogoffAsync(int id)
        {
            var url = string.Format(Endpoints.LOGOFF, id);
            var result = await Rest.PostAsync<Model.Result>(url);
            Authorization.Singleton.KillToken();
            return result;
        }
    }
}