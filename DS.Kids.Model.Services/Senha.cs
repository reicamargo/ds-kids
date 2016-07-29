using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public class Senha : ISenha
    {
        private readonly Events.ISenha _events;
        private readonly Repositories.IResponsaveis _responsaveis;
        /// <summary>
        /// Construtor Padrão
        /// </summary>
        /// <param name="responsaveis">Repositório de Responsáveis</param>
        public Senha(Events.ISenha events, Repositories.IResponsaveis responsaveis)
        {
            Throw.IfIsNull(responsaveis);

            this._responsaveis = responsaveis;
            this._events = events;
        }

        /// <summary>
        /// Efetuar Esqueci minha Senha de forma assíncrona
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Resultado da execução.</returns>
        public async Task<Result> EsqueciAsync(string email)
        {
            var validate = Validations.Validate.Email(email);
            if (validate != Validations.ResultCodes.Success) return new Model.Result<Model.Token>(validate);

            var responsavel = await this._responsaveis.ObterPorEmailAsync(email);
            if (responsavel == null)
                return new Model.Result<Model.Token>(Validations.ResultCodes.ResponsavelNaoEncontrado);

            this.GeraTokenRecuperacaoSenha(responsavel);
            await this._responsaveis.AtualizarAsync(responsavel);
            await this._events.TrocaDeSenhaSolicitadaAsync(responsavel);

            return Result.SUCCESS;
        }

        /// <summary>
        /// Troca a Senha
        /// </summary>
        /// <param name="trocaDeSenha">Informações sobre a Troca de Senha</param>
        /// <returns>Resultado da Troca</returns>
        public async Task<Model.Result> TrocaAsync(Model.TrocaDeSenha trocaDeSenha)
        {
            Throw.IfIsNull(trocaDeSenha);

            var validate = trocaDeSenha.Validate();
            if (validate != Validations.ResultCodes.Success)
                return new Model.Result(validate);

            var responsavel = await this._responsaveis.ObterPorIdAsync(trocaDeSenha.IdResponsavel); ;
            if (responsavel == null)
                return new Model.Result(Validations.ResultCodes.ResponsavelNaoEncontrado);

            var senhaAtualCriptografada = Security.SHA512.ComputeHash(trocaDeSenha.SenhaAtual);
            validate = responsavel.ValidarTrocaSenha(senhaAtualCriptografada);
            if (validate != Validations.ResultCodes.Success) 
                return new Model.Result(validate);

            this.CriptografarSenha(responsavel, trocaDeSenha);

            responsavel.ExcluirTokenRecuperacaoSenha();

            await this._responsaveis.AtualizarAsync(responsavel);
            await this._events.TrocaDeSenhaEfetuadaAsync(responsavel);

            return Result.SUCCESS;
        }

        /// <summary>
        /// Gera Token de Recuperação de Senha
        /// </summary>
        /// <param name="responsavel">Responsável</param>
        private void GeraTokenRecuperacaoSenha(Model.Responsavel responsavel)
        {
            var tokenRecuperacaoSenha = new TokenRecuperacaoSenha(responsavel.IdResponsavel);
            responsavel.TokenRecuperacaoSenha = tokenRecuperacaoSenha.CriarTokenRecuperacaoSenha();
        }

        /// <summary>
        /// Criptografa a Senha
        /// </summary>
        /// <param name="responsavel">Responsável</param>
        /// <param name="trocaDeSenha">Troca de Senha</param>
        private void CriptografarSenha(Model.Responsavel responsavel,  Model.TrocaDeSenha trocaDeSenha)
        {
            responsavel.Senha = Security.SHA512.ComputeHash(trocaDeSenha.NovaSenha);
            trocaDeSenha.SenhaAtual = responsavel.Senha;
        }

        /// <summary>
        /// Criptografa a Senha
        /// </summary>
        /// <param name="responsavel"></param>
        private void CriptografarSenha(Model.Responsavel responsavel)
        {
            responsavel.Senha = Security.SHA512.ComputeHash(responsavel.Senha);
        }

        /// <summary>
        /// Token de Recuperação da Senha.
        /// Essa entidade administra o Token Gerado para um Responsável, contendo a data de expiração.
        /// Esse token serve para a identificação do Responsável na hora da troca de senha à partir do Esqueci Minha Senha
        /// </summary>
        public class TokenRecuperacaoSenha
        {
            private int _responsavelId;
            public string _salto;
            public DateTime _dataExpiracao;

            /// <summary>
            /// Id do Responsável
            /// </summary>
            public int ResponsavelId
            {
                get { return this._responsavelId; }
                private set { this._responsavelId = value; }
            }

            /// <summary>
            /// Salto para o Token
            /// </summary>
            public string Salto
            {
                get { return this._salto; }
                private set { this._salto = value; }
            }

            /// <summary>
            /// Data da Expiração
            /// </summary>
            public DateTime DataExpiracao
            {
                get { return this._dataExpiracao; }
                private set { this._dataExpiracao = value; }
            }

            /// <summary>
            /// Cria um Token de Recuperação de Senha a partir do Id do Responsável
            /// </summary>
            /// <param name="responsavelId"></param>
            public TokenRecuperacaoSenha(int responsavelId)
            {
                this.ResponsavelId = responsavelId;
                this.DataExpiracao = DateTime.Now.AddDays(2);
                this.Salto = Guid.NewGuid().ToString();
            }

            /// <summary>
            /// Cria um Token de Recuperação de senha a partir de um Token
            /// </summary>
            /// <param name="token"></param>
            public TokenRecuperacaoSenha(string token)
            {
                this.ObtemTokenRecuperacaoSenha(token);
            }

            /// <summary>
            /// Cria um Token com as informações de recuperação de senha
            /// </summary>
            /// <returns></returns>
            public string CriarTokenRecuperacaoSenha()
            {
                var chave = string.Format("{0}|{1}|{2}", this.ResponsavelId, this.DataExpiracao, this.Salto);
                return Security.Rijndael.Encrypt(chave);
            }

            /// <summary>
            /// Descriptografa um token
            /// </summary>
            /// <param name="token"Token></param>
            /// <returns>Resultado da Decriptografia</returns>
            private Model.Result ObtemTokenRecuperacaoSenha(string token)
            {
                if (string.IsNullOrWhiteSpace(token))
                    return new Model.Result<string>(Validations.ResultCodes.TokenRecuperacaoSenhaInvalido);

                var chaveDecriptografada = Security.Rijndael.Decrypt(token);
                var partes = chaveDecriptografada.Split('|');

                if (partes.Length != 3)
                    return new Model.Result<string>(Validations.ResultCodes.TokenRecuperacaoSenhaInvalido);

                try
                {
                    int.TryParse(partes[0], out this._responsavelId);
                    DateTime.TryParse(partes[1], out this._dataExpiracao);
                    this._salto = partes[2];
                }
                catch
                {
                    return new Model.Result<string>(Validations.ResultCodes.TokenRecuperacaoSenhaInvalido);
                }

                return Model.Result.SUCCESS;
            }
        }
    }
}
