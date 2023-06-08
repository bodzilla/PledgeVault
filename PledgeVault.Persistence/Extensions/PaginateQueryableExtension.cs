using PledgeVault.Core.Dtos.Pagination;
using System.Linq;

namespace PledgeVault.Persistence.Extensions;

public static class PaginateQueryableExtension
{
    /// <summary>
    /// Given a <see cref="PageOptions"/>, applies pagination to the queryable.
    /// </summary>
    public static IQueryable<T> WithPagination<T>(this IQueryable<T> queryable, PageOptions pageOptions)
        => queryable.Skip((pageOptions.PageNumber - 1) * pageOptions.PageSize).Take(pageOptions.PageSize);
}