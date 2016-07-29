using System;
using System.Threading.Tasks;
namespace DS.Kids.Model.Services
{
    /// <summary>
    /// Service de Responsável
    /// </summary>
    public class Responsavel : DS.Kids.Model.Services.IResponsavel
    {
        private readonly Repositories.IResponsaveis _responsaveis;
        private readonly Repositories.ITokens _tokens;
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        /// <param name="responsaveis">Repositório de Responsáveis</param>
        /// <param name="tokens">Repositório de Tokens</param>
        public Responsavel(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens)
        {
            Throw.IfIsNull(responsaveis);
            Throw.IfIsNull(tokens);

            this._responsaveis = responsaveis;
            this._tokens = tokens;
        }

        /// <summary>
        /// Inserir um responsável de forma assíncrona
        /// </summary>
        /// <param name="responsavel">Responsável</param>
        /// <returns>Token gerado</returns>
        public async Task<Model.Result<Model.Responsavel>> InserirAsync(Model.Responsavel responsavel)
        {
            Throw.IfIsNull(responsavel);

            var validate = responsavel.Validate();
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Responsavel>(validate);

            responsavel.Senha = Security.SHA512.ComputeHash(responsavel.Senha);
            responsavel.Token = Model.Token.Criar(responsavel);
            responsavel.Optin = true;
            try
            {
                await this._responsaveis.InserirAsync(responsavel);
                return new Result<Model.Responsavel>(responsavel);
            }
            catch (Repositories.DuplicateEntityException)
            {
                return new Result<Model.Responsavel>(null, Validations.ResultCodes.EmailResponsavelJaCadastradoNoSistema);
            }
        }

        /// <summary>
        /// Atualizar responsável de forma assíncrona
        /// </summary>
        /// <param name="responsavel">Responsável</param>
        /// <returns>Resultado da execução</returns>
        public async Task<Model.Result> AtualizarAsync(Model.Responsavel responsavel)
        {
            Throw.IfIsNull(responsavel);

            var validate = responsavel.Validate();
            if (validate != Validations.ResultCodes.Success && validate != Validations.ResultCodes.TamanhoMaximoCampoSenha)
                return new Model.Result<Model.Token>(validate);

            await this._responsaveis.AtualizarAsync(responsavel);

            return Model.Result.SUCCESS;
        }
    }
}