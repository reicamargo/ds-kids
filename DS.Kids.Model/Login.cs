using System;
using System.Text;
namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa uma Login
    /// </summary>
    public class Login : Support.BaseModel
    {
        private const byte QtdCaracteresSenha = 6;

        private string _email;
        private string _senha;

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                Notify();
            }
        }
        
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                Notify();
            }
        }

        /// <summary>
        /// Gera uma senha aleatória
        /// </summary>
        /// <returns></returns>
        public static string GerarSenhaAleatoria()
        {
            // Todo: Remover essa regra daqui e colocar numa infra
            var sb = new StringBuilder();

            var caracteres = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%^&*()?-\/";

            for (int i = 0; i < QtdCaracteresSenha; i++)
            {
                var indice = ExtentionMethods.Random.Next(0, caracteres.Length);
                var character = caracteres[indice];
                sb.Append(character);
                caracteres = caracteres.Remove(indice, 1);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Valida informações sobre o Login
        /// </summary>
        /// <returns>Resultado da Validação</returns>
        public override Validations.ResultCodes Validate()
        {
            var email = Validations.Validate.Email(this.Email);
            if (email != Validations.ResultCodes.Success) return email;

            var senha = Validations.Validate.Senha(this.Senha);
            if (senha != Validations.ResultCodes.Success) return senha;

            return Validations.ResultCodes.Success;
        }

        /// <summary>
        /// Entidade que representa uma informação de Esqueci Minha Senha
        /// </summary>
        public class EsqueciMinhaSenha : Support.BaseModel
        {
            /// <summary>
            /// Email
            /// </summary>
            public string Email { get; set; }

            public override Validations.ResultCodes Validate()
            {
                var email = Validations.Validate.Email(this.Email);
                if (email != Validations.ResultCodes.Success) return email;

                return Validations.ResultCodes.Success;
            }
        }
    }
}