using PledgeVault.Core.Contracts.Entities;
using System.Linq;

namespace PledgeVault.Persistence.Extensions;

public static class OnlyActiveEntitiesQueryableExtension
{
    /// <summary>
    /// Only returns entities where <see cref="IEntity.IsEntityActive"/> is true. Ensure that this method is always called before
    /// additional filtering is applied to the queryable, otherwise the additional filtering will be applied to all entities.
    /// </summary>
    public static IQueryable<T> WithOnlyActiveEntities<T>(this IQueryable<T> queryable) where T : IEntity
        => queryable.Where(x => x.IsEntityActive);
}