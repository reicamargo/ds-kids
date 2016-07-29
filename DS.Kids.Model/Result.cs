using Newtonsoft.Json;

namespace DS.Kids.Model
{
    /// <summary>
    /// Entidade que encapsula o Resultado de uma Operação.
    /// É possível indicar um determinado erro na operação. Ou retornar Success para uma operação concluida com sucesso.
    /// </summary>
    /// <typeparam name="T">Tipo de Dado do Resultado da Operação</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Conteúdo de Retorno da Operação
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public Result() { }
        /// <summary>
        /// Constrói um resultado a partir do Conteúdo do Retorno e o Resultado da Operação
        /// </summary>
        /// <param name="data">Conteúdo do Retorno</param>
        /// <param name="resultCode">Resultado da Operação</param>
        public Result(T data, Validations.ResultCodes resultCode = Validations.ResultCodes.Success) : base(resultCode)
        {
            this.Data = data;
        }

        /// <summary>
        /// Constrói um resultado a partir do Resultado da Operação
        /// </summary>
        /// <param name="resultCode">Resultado da Operação</param>
        public Result(Validations.ResultCodes resultCode) : this(default(T), resultCode) { }
    }

    /// <summary>
    /// Entidade que encapsula o Resultado de uma Operação.
    /// É possível indicar um determinado erro na operação. Ou retornar Success para uma operação concluida com sucesso.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Resultado que indica que a operação foi efetuada com sucesso
        /// </summary>
        public static Result SUCCESS = new Result(Validations.ResultCodes.Success);

        private Validations.ResultCodes _resultCode;
        /// <summary>
        /// Código do Resultado da Operação
        /// </summary>
        [JsonIgnore]
        public Validations.ResultCodes ResultCode
        {
            get
            {
                return this._resultCode;
            }
            private set
            {
                this._resultCode = value;
            }
        }

        /// <summary>
        /// Código Numérido do Resultado da Operação
        /// </summary>
        public int ResultId
        {
            get
            {
                return (int)this._resultCode;
            }
            set
            {
                this._resultCode = (Validations.ResultCodes)value;
            }
        }

        /// <summary>
        /// Mensagem do Resultado da Operação
        /// </summary>
        public string ResultMessage { get; set; }

        /// <summary>
        /// Construtor Padrão
        /// </summary>
        public Result() { }

        /// <summary>
        /// Constrói um resultado a partir do Resultado da Operação
        /// </summary>
        /// <param name="resultCode">Resultado da Operação</param>
        public Result(Validations.ResultCodes resultCode = Validations.ResultCodes.Success)
        {
            this.ResultCode = resultCode;
            this.ResultMessage = Validations.Messages.Get(resultCode);
        }
    }
}