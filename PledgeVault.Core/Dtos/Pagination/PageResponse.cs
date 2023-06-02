using System;
using System.Collections.Generic;
using PledgeVault.Core.Contracts.Dtos;

namespace PledgeVault.Core.Dtos.Pagination;

public sealed class PageResponse<T> where T : IResponse
{
    public PageResponse() => Data = new List<T>();

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalItems, PageSize));

    public List<T> Data { get; set; }
}