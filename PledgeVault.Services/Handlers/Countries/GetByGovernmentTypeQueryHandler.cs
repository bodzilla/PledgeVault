﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Countries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetByGovernmentTypeQueryHandler : IRequestHandler<GetByGovernmentTypeQuery, PageResponse<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByGovernmentTypeQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PageResponse<CountryResponse>> Handle(GetByGovernmentTypeQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Countries
                .AsNoTracking()
                .Paginate(query.Page)
                .Where(x => x.GovernmentType == query.GovernmentType)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.Page.PageNumber,
            PageSize = query.Page.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}