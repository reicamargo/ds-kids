using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services.Events
{
    public interface ISenha
    {
        Task TrocaDeSenhaSolicitadaAsync(Model.Responsavel responsavel);
        Task TrocaDeSenhaEfetuadaAsync(Model.Responsavel responsavel);
    }
}
