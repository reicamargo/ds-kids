using System.Linq;
using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Repositories;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    public class LoginsSociaisFake : ILoginsSociais
    {
        private readonly Database _database;

        public LoginsSociaisFake(Database database)
        {
            _database = database;
        }

        public async Task<LoginSocial> ObterPorResponsavelIdRedeSocialAsync(int responsavelId, RedesSociais redeSocial)
        {
            await Task.Delay(0);
            lock (_database.logins_Sociais)
            {
                return _database.logins_Sociais.FirstOrDefault(d => d.Responsavel.IdResponsavel == responsavelId && d.RedeSocial == redeSocial); 
            }
        }

        public async Task InserirAsync(LoginSocial loginSocial)
        {
            lock (_database.logins_Sociais)
            {
                _database.logins_Sociais.Add(loginSocial); 
            }
            await Task.Delay(0);
        }

        public async Task ExcluirPorResponsavelIdAsync(int responsavelId)
        {
            await Task.Delay(0);
            lock (_database.logins_Sociais)
            {
                var existente = _database.logins_Sociais.FirstOrDefault(d => d.IdResponsavel == responsavelId);
                _database.logins_Sociais.Remove(existente); 
            }
        }
    }
}
