using System.Threading.Tasks;

namespace DS.Kids.Model.Repositories
{
    public interface ITokens
    {
        Task<Token> ObterPorValorAsync(string valor);
        Task<Token> ObterPorResponsavelIdAsync(int responsavelId);
        Task InserirAsync(Token token);
        Task ExcluirPorResponsavelIdAsync(int responsavelId);
    }
}
