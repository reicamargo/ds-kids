using System;
using System.Text;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que representa um Token
    /// </summary>
    public class Token : Support.BaseModel
    {
        /// <summary>
        /// Valor do Token
        /// </summary>
        public string Valor { get; set; }
        /// <summary>
        /// Id do Respons�vel
        /// </summary>
        public int ResponsavelId { get; set; }
        /// <summary>
        /// Respons�vel
        /// </summary>
        public virtual Responsavel Responsavel { get; set; }
        /// <summary>
        /// Status do Token.
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Data de cria��o
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Construtor padr�o
        /// </summary>
        public Token()
        {
            this.DataCriacao = DateTime.Now;
            this.Status = true;
        }

        /// <summary>
        /// Cria um token para um determinado respons�vel
        /// </summary>
        /// <param name="responsavel">Respons�vel</param>
        /// <returns>Token criado</returns>
        public static Token Criar(Responsavel responsavel)
        {
            Throw.IfIsNull(responsavel);

            var input = string.Format("{0}{1}{2}", responsavel.IdResponsavel, DateTime.Now, Guid.NewGuid());
            var inputBytes = Encoding.UTF8.GetBytes(input);

            var token = new Token();
            token.Valor = BitConverter.ToString(inputBytes);
            token.ResponsavelId = responsavel.IdResponsavel;

            return token;
        }
    }
}
