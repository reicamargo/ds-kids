using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface ISenha
    {
        Task<Model.Result> EsqueciAsync(string email);
        Task<Model.Result> TrocaAsync(Model.TrocaDeSenha trocaDeSenha);
    }
}
