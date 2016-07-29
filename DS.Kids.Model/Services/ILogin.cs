using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
	public interface ILogin
	{
        Task<Model.Result<Model.Responsavel>> LogarAsync(Login login);
        Task<Model.Result<Model.Responsavel>> LogarRedeSocialAsync(LoginSocial loginSocial);
        Task<Model.Result> LogoffAsync(int responsavelId);
	}
}
