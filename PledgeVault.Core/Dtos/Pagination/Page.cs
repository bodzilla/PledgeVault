using PledgeVault.Core.Contracts.Dtos;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Pagination;

public sealed class Page<T> where T : IResponse
{
    public Page() => Data = new List<T>();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));

    public IEnumerable<T> Data { get; set; }
}