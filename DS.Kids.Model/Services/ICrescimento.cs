using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface ICrescimento
    {
        Task<Result<Model.Crescimento>> InserirAsync(Model.PesoAltura pesoAltura);
        Task<Result<Model.Crescimento>> AtualizarAsync(Model.PesoAltura pesoAltura);
    }
}
