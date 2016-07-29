using System;
using System.Linq;
using System.Collections.Generic;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um respons�vel
    /// </summary>
    public sealed class Responsavel : Support.BaseModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int IdResponsavel { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Data de Cria��o
        /// </summary>
        public DateTime DataCriacao { get; set; }
        /// <summary>
        /// Data da �ltima atualiza��o
        /// </summary>
        public DateTime DataAtualizacao { get; set; }
        /// <summary>
        /// Indica se o Respons�vel est� ativo no sistema
        /// </summary>
        public bool Ativo { get; set; }
        /// <summary>
        /// Lista de Crian�as do respons�vel
        /// </summary>
        public ICollection<Crianca> Criancas { get; set; }
        /// <summary>
        /// Token do Respons�vel
        /// </summary>
        public Token Token { get; set; }
        /// <summary>
        /// Informa��es sobre Login em uma rede social
        /// </summary>
        public LoginSocial LoginSocial { get; set; }
        /// <summary>
        /// Indica se o usu�rio permite o envio de emails
        /// </summary>
        public bool Optin { get; set; }

        /// <summary>
        /// Token de Recupera��o de senha
        /// </summary>
        public string TokenRecuperacaoSenha { get; set; }

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public Responsavel()
        {
            this.Criancas = new List<Crianca>();

            this.DataCriacao = DateTime.Now;
            this.DataAtualizacao = DateTime.Now;
            this.Ativo = true;
            this.Optin = true;
        }

        /// <summary>
        /// Exlui o Token de Recupera��o de Senha
        /// </summary>
        public void ExcluirTokenRecuperacaoSenha()
        {
            //Pode parecer simpl�rio esse m�todo, mas no momento n�o temos definida a regra para troca de senha
            this.TokenRecuperacaoSenha = string.Empty;
        }

        /// <summary>
        /// Valida se a Crian�a pode ser inserida no Respons�vel
        /// </summary>
        /// <param name="crianca">Crian�a</param>
        /// <returns>Resultado da Valida��o</returns>
        public Validations.ResultCodes ValidarInsercaoCrianca(Model.Crianca crianca)
        {
            Throw.IfIsNull(crianca);

            if (this.Criancas != null)
            {
                var criancaExistente = this.Criancas.FirstOrDefault(c => c.Nome.Equals(crianca.Nome));
                if (criancaExistente != null)
                    return Validations.ResultCodes.CriancaJaCadastrada;
            }

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Validar a troca de senha
        /// </summary>
        /// <param name="novaSenhaCriptografada"></param>
        /// <returns></returns>
        public Validations.ResultCodes ValidarTrocaSenha(string novaSenhaCriptografada)
        {
            if (!this.Senha.Equals(novaSenhaCriptografada))
                return Validations.ResultCodes.LoginOuSenhaInvalidos;

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Validar Sennha
        /// </summary>
        /// <param name="novaSenhaCriptografada"></param>
        /// <returns></returns>
        public Validations.ResultCodes ValidarSenha(string senhaCriptografada)
        {
            if (!this.Senha.Equals(senhaCriptografada))
                return Validations.ResultCodes.LoginOuSenhaInvalidos;

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Valida informa��es sobre o Respons�vel
        /// </summary>
        /// <returns>Resultado da Valida��o</returns>
        public override Validations.ResultCodes Validate()
        {
            var nome = Validations.Validate.Nome(this.Nome);
            if (nome != Validations.ResultCodes.Success) return nome;

            var email = Validations.Validate.Email(this.Email);
            if (email != Validations.ResultCodes.Success) return email;

            var senha = Validations.Validate.Senha(this.Senha);
            if (senha != Validations.ResultCodes.Success) return senha;

            var telefone = Validations.Validate.Telefone(this.Telefone, false);
            if (telefone != Validations.ResultCodes.Success) return telefone;

            return Validations.ResultCodes.Success;
        }
    }
}
