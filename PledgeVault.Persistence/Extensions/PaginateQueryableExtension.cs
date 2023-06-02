using PledgeVault.Core.Dtos.Pagination;
using System.Linq;

namespace PledgeVault.Persistence.Extensions;

public static class PaginateQueryableExtension
{
    /// <summary>
    /// Given a <see cref="Page"/>, applies pagination to the queryable.
    /// </summary>
    public static IQueryable<T> PaginateFrom<T>(this IQueryable<T> queryable, Page page)
        => queryable.Skip((page.PageNumber - 1) * page.PageSize).Take(page.PageSize);
}