using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IPoliticianService
{
    Task CheckIfPartyLeaderExists(Politician entity, CancellationToken cancellationToken);
}