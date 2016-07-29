using System.Threading.Tasks;

namespace DS.Kids.Model.Services
{
    public interface IOptin
    {
        Task<Result> SetAsync(Optin optin);
    }
}
