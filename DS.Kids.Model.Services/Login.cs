using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    /// <summary>
    /// Serviço de Login
    /// </summary>
    public class Login : DS.Kids.Model.Services.ILogin
    {
        private readonly Repositories.IResponsaveis _responsaveis;
        private readonly Repositories.ITokens _tokens;
        private readonly Repositories.ILoginsSociais _loginsSociais;
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        /// <param name="responsaveis">Repositório de Responsáveis</param>
        /// <param name="tokens">Repositório de Tokens</param>
        public Login(Repositories.IResponsaveis responsaveis, Repositories.ITokens tokens, Repositories.ILoginsSociais loginsSociais)
        {
            Throw.IfIsNull(responsaveis);
            Throw.IfIsNull(tokens);

            this._responsaveis = responsaveis;
            this._tokens = tokens;
            this._loginsSociais = loginsSociais;
        }

        /// <summary>
        /// Efetua Login de forma assíncrona
        /// </summary>
        /// <param name="login">Informações do Login</param>
        /// <returns>Token gerado</returns>
        public async Task<Model.Result<Model.Responsavel>> LogarAsync(Model.Login login)
        {
            Throw.IfIsNull(login);

            var validate = login.Validate();
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Responsavel>(validate);

            var responsavel = await this._responsaveis.ObterPorEmailAsync(login.Email);
            if (responsavel == null)
                return new Model.Result<Model.Responsavel>(Validations.ResultCodes.LoginOuSenhaInvalidos);

            var senhaLoginCriptografada = Security.SHA512.ComputeHash(login.Senha);
            validate = responsavel.ValidarSenha(senhaLoginCriptografada);
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Responsavel>(validate);

            if (responsavel.Token != null)
                return new Model.Result<Model.Responsavel>(responsavel);

            var token = Model.Token.Criar(responsavel);
            await this._tokens.InserirAsync(token);

            responsavel.Token = token;
            return new Model.Result<Model.Responsavel>(responsavel);
        }

        /// <summary>
        /// Efetuar login via Facebook de forma assíncrona
        /// </summary>
        /// <param name="login">Informações de Login</param>
        /// <returns>Token gerado</returns>
        public async Task<Model.Result<Model.Responsavel>> LogarRedeSocialAsync(LoginSocial login)
        {
            Throw.IfIsNull(login);

            var validate = login.Validate();
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Responsavel>(validate);

            /*Situação em que o Responsável faz o login via rede social MAS JÁ TEM UM CADASTRO NO SISTEMA*/
            var responsavel = await this._responsaveis.ObterPorEmailAsync(login.Email);
            if (responsavel != null)
            {
                /*Responsável tem cadastro mas nunca logou via rede social*/
                if (responsavel.LoginSocial == null)
                {
                    login.IdResponsavel = responsavel.IdResponsavel;
                    await this._loginsSociais.InserirAsync(login);
                }

                /*Responsável tem cadastro e já logou via rede social*/
                return new Model.Result<Model.Responsavel>(responsavel);
            }
            /*Situação em que o Responsável faz o login via rede social MAS NÃO TEM UM CADASTRO NO SISTEMA*/
            else
            {
                responsavel = login.CriarResponsavel();

                var service = new Services.Responsavel(this._responsaveis, this._tokens);
                var result = await service.InserirAsync(responsavel);
                return result;
            }
        }

        /// <summary>
        /// Efetuar Logoff de forma assíncrona
        /// </summary>
        /// <param name="responsavelId">Id do responsável</param>
        /// <returns>Resultado da execução.</returns>
        public async Task<Model.Result> LogoffAsync(int responsavelId)
        {
            Throw.IfLessThanOrEqZero(responsavelId);

            await this._tokens.ExcluirPorResponsavelIdAsync(responsavelId);
            return Result.SUCCESS;
        }
    }
}