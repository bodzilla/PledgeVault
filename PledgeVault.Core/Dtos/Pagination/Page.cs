namespace PledgeVault.Core.Dtos.Pagination;

public sealed class Page
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 50;
}
