using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Solicitação de recuperação de senha
    /// </summary>
    public class EsqueciMinhaSenha : Support.BaseModel
    {
        /// <summary>
        /// Token de Recuperação de senha
        /// </summary>
        public string TokenRecuperacaoSenha { get; set; }

        /// <summary>
        /// Nova Senha
        /// </summary>
        public string NovaSenha { get; set; }

        /// <summary>
        /// Confirmação da nova senha
        /// </summary>
        public string ConfirmacaoNovaSenha { get; set; }

        /// <summary>
        /// Valida as informações da Entidade
        /// </summary>
        /// <returns>Resultado da Valiação</returns>
        public override Validations.ResultCodes Validate()
        {
            var token = Validations.Validate.TokenRecuperacaoSenha(this.TokenRecuperacaoSenha);
            if (token != Validations.ResultCodes.Success) return token;

            var senha = Validations.Validate.EsqueciMinhaSenha(this.NovaSenha, this.ConfirmacaoNovaSenha);
            if (senha != Validations.ResultCodes.Success) return senha;

            return Validations.ResultCodes.Success;
        }
    }
}
