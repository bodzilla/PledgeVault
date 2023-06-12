using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Dtos.Pagination;
using System.Linq;

namespace PledgeVault.Persistence.Extensions;

public static class PaginateQueryableExtension
{
    /// <summary>
    /// Given a <see cref="PageOptions"/>, applies pagination to the queryable. Ensure that this method is always called after
    /// any filtering (including <see cref="OnlyActiveEntitiesQueryableExtension"/>) is applied to the queryable,
    /// otherwise the pagination will be applied to all entities.
    /// </summary>
    public static IQueryable<T> WithPagination<T>(this IQueryable<T> queryable, PageOptions pageOptions) where T : IEntity
        => queryable.Skip((pageOptions.PageNumber - 1) * pageOptions.PageSize).Take(pageOptions.PageSize);
}