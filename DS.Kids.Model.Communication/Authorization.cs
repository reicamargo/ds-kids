using System;
namespace DS.Kids.Model.Communication
{
    /// <summary>
    /// Singleton que gerencia o Token de Autorização da Comunicação com as APIs
    /// O Token é setado ao efetuar um login, seja social ou via e-mail, e é removido ao efetuar logoff
    /// </summary>
    public class Authorization
    {
        #region Singleton
        private Authorization() { }
        private static Authorization _instance;
        private static object _lock = new object();
        /// <summary>
        /// Objeto de Instância única
        /// </summary>
        public static Authorization Singleton
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Authorization();

                    return _instance;
                }
            }
        }
        #endregion Singleton

        private string _token;

        /// <summary>
        /// Seta o Token na Autorização
        /// </summary>
        /// <param name="token"></param>
        public void SetToken(string token)
        {
            this._token = token;
        }

        /// <summary>
        /// Seta o Token na Autorização
        /// </summary>
        /// <param name="result">Result com um Responsável</param>
        public void SetToken(Model.Result<Model.Responsavel> result)
        {
            if (result.Data != null && result.Data.Token != null)
                this._token = result.Data.Token.Valor;
        }

        /// <summary>
        /// Seta o Token na Autorização
        /// </summary>
        /// <param name="responsavel">Responsável</param>
        public void SetToken(Model.Responsavel responsavel)
        {
            if (responsavel != null && responsavel.Token != null)
                this._token = responsavel.Token.Valor;
        }

        /// <summary>
        /// Seta o Token na Autorização
        /// </summary>
        /// <param name="token">Token</param>
        public void SetToken(Model.Token token)
        {
            if (token != null)
                this._token = token.Valor;
        }

        /// <summary>
        /// Obtém Token
        /// </summary>
        /// <returns>Token</returns>
        public string GetToken()
        {
            return this._token;
        }

        /// <summary>
        /// Mata o token \m/
        /// </summary>
        public void KillToken()
        {
            this._token = null;
        }
    }
}
