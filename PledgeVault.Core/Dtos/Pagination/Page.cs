using PledgeVault.Core.Contracts.Dtos;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Pagination;

public sealed record Page<T> where T : IResponse
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));

    public IReadOnlyCollection<T> Data { get; init; }
}