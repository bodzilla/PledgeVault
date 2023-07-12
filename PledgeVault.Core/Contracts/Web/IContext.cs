using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Web;

public interface IContext
{
    Task<int?> GetCurrentUserId();
}