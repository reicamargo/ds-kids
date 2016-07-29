using System.Threading.Tasks;
using Services = DS.Kids.Model.Services;

namespace DS.Kids.API.Events
{
    public class Senha : Services.Events.ISenha
    {
        public async Task TrocaDeSenhaSolicitadaAsync(Model.Responsavel responsavel)
        {
            using (var email = new Emails.TrocaSenha())
            {
                await email.EnviarAsync(responsavel);
            }
        }

        public async Task TrocaDeSenhaEfetuadaAsync(Model.Responsavel responsavel)
        {
            await Task.Delay(0);
        }
    }
}