using System;
namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um login via Rede Social
    /// </summary>
    public class LoginSocial : Support.BaseModel
    {
        /// <summary>
        /// Id do Responsável
        /// </summary>
        public int IdResponsavel { get; set; }

        /// <summary>
        /// Responsável
        /// </summary>
        public virtual Responsavel Responsavel { get; set; }

        /// <summary>
        /// Chave da Rede Social
        /// </summary>
        public string Chave { get; set; }

        /// <summary>
        /// Nome obtido no perfil do Facebook
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email obtido no perfil do Facebook
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Rede Social
        /// </summary>
        public RedesSociais RedeSocial { get; set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public LoginSocial()
        {
            this.DataCriacao = DateTime.Now;
        }

        /// <summary>
        /// Valida informações sobre o Login do Facebook
        /// </summary>
        /// <returns>Resultado da Validação</returns>
        public override Validations.ResultCodes Validate()
        {
            var nome = Validations.Validate.Nome(this.Nome);
            if (nome != Validations.ResultCodes.Success) return nome;

            var email = Validations.Validate.Email(this.Email);
            if (email != Validations.ResultCodes.Success) return email;

            var chave = Validations.Validate.ChaveRedeSocial(this.Chave);
            if (chave != Validations.ResultCodes.Success) return chave;

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Criar Responsável a partir de um Login Social
        /// </summary>
        /// <returns>Responsável</returns>
        public Model.Responsavel CriarResponsavel()
        {
            var responsavel = new Model.Responsavel();
            responsavel.Email = this.Email;
            responsavel.Nome = this.Nome;
            responsavel.Senha = Model.Login.GerarSenhaAleatoria();
            responsavel.LoginSocial = this;
            return responsavel;
        }
    }
}