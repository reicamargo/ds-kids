using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public class LoginsSociais : ILoginsSociais
    {
        public async Task<Model.LoginSocial> ObterPorResponsavelIdRedeSocialAsync(int responsavelId, RedesSociais redeSocial)
        {
            Throw.IfLessThanOrEqZero(responsavelId);

            using (var context = new Context())
            {
                return await context.LoginsSociais.FirstOrDefaultAsync(a => a.Responsavel.IdResponsavel == responsavelId && a.RedeSocial == redeSocial);
            }
        }

        public async Task InserirAsync(LoginSocial loginSocial)
        {
            Throw.IfIsNull(loginSocial);

            using (var context = new Context())
            {
                context.LoginsSociais.Add(loginSocial);
                await context.SaveChangesAsync();
            }
        }

        public async Task ExcluirPorResponsavelIdAsync(int responsavelId)
        {
            Throw.IfLessThanOrEqZero(responsavelId);

            using (var context = new Context())
            {
                var tokens = context.LoginsSociais.Where(t => t.IdResponsavel == responsavelId);
                context.LoginsSociais.RemoveRange(tokens);
                await context.SaveChangesAsync();
            }
        }
    }
}
