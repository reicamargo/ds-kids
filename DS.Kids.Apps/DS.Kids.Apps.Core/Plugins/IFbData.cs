using System.Threading.Tasks;

using DS.Kids.Model;

namespace DS.Kids.Apps.Core.Plugins
{

	public interface IFbData
	{

		Task<LoginSocial> GetUserInfoAsync();

	}

}
