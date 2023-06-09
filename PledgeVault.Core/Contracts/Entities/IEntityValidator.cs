using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Entities;

public interface IEntityValidator<in T> where T : IEntity
{
    Task ValidateAllRules(T request, CancellationToken cancellationToken);
}