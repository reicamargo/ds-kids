using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
	public interface IResponsavel
	{
        Task<Model.Result<Model.Responsavel>> InserirAsync(Responsavel responsavel);
        Task<Model.Result> AtualizarAsync(Responsavel responsavel);
	}
}
