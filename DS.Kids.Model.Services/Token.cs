using System;
using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public class Token : IToken
    {
        private readonly Repositories.ITokens _tokens;
        public Token(Repositories.ITokens tokens)
        {
            Throw.IfIsNull(tokens);
            this._tokens = tokens;
        }

        public async Task<bool> TokenValidoAsync(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave))
                return false;

            var token = await this._tokens.ObterPorValorAsync(chave);
            return (token != null);
        }
    }
}