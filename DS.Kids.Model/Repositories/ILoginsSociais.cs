using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface ILoginsSociais
    {
        Task<Model.LoginSocial> ObterPorResponsavelIdRedeSocialAsync(int responsavelId, RedesSociais redeSocial);
        Task InserirAsync(Model.LoginSocial loginSocial);
        Task ExcluirPorResponsavelIdAsync(int responsavelId);
    }
}
