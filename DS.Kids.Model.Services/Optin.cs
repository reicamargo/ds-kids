using System;
using System.Threading.Tasks;

using DS.Kids.Model.Validations;

namespace DS.Kids.Model.Services
{
    public class Optin : IOptin
    {
        private readonly Repositories.IResponsaveis _responsaveis;

        public Optin(Repositories.IResponsaveis responsaveis)
        {
            Throw.IfIsNull(responsaveis);

            _responsaveis = responsaveis;
        }

        public async Task<Result> SetAsync(Model.Optin optin)
        {
            Throw.IfIsNull(optin);

            Throw.IfLessThanOrEqZero(optin.IdResponsavel);

            var responsavel = await _responsaveis.ObterPorIdAsync(optin.IdResponsavel);

            if(responsavel == null)
            {
                return new Result(ResultCodes.ResponsavelNaoEncontrado);
            }

            responsavel.Optin = optin.OptinPrincipal;

            await this._responsaveis.AtualizarAsync(responsavel);

            return Model.Result.SUCCESS;
        }

    }
}
