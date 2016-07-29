using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface IToken
    {
        Task<bool> TokenValidoAsync(string chave);
    }
}
