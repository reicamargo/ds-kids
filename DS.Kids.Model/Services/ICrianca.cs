using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
	public interface ICrianca
	{
        Task<Model.Result<Model.Crianca>> InserirAsync(Crianca crianca);
        Task<Model.Result<Model.Crianca>> AtualizarAsync(Crianca crianca);
        Task<Model.Result> ExcluirAsync(int id);
	}
}
