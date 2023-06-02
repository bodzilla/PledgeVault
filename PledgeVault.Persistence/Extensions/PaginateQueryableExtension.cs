using PledgeVault.Core.Dtos.Pagination;
using System.Linq;

namespace PledgeVault.Persistence.Extensions;

public static class PaginateQueryableExtension
{
    /// <summary>
    /// Applies pagination to a queryable.
    /// </summary>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, Page page)
        => queryable.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
}