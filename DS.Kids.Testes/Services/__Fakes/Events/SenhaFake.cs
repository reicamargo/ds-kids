using System.Threading.Tasks;

using DS.Kids.Model;
using DS.Kids.Model.Services.Events;

namespace DS.Kids.Testes.Services.__Fakes.Events
{
    public class SenhaFake : ISenha
    {
        public async Task TrocaDeSenhaSolicitadaAsync(Responsavel responsavel)
        {
            await Task.Delay(0);
        }

        public async Task TrocaDeSenhaEfetuadaAsync(Responsavel responsavel)
        {
            await Task.Delay(0);
        }
    }
}
