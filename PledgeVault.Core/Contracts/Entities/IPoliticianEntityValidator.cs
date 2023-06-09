using PledgeVault.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities;

public interface IPoliticianEntityValidator : IEntityValidator<Politician>
{
    Task EnsureOnlyOnePartyLeader(Politician request, CancellationToken cancellationToken);
}