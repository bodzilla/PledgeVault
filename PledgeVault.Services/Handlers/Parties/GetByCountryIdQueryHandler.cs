﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries.Parties;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByCountryIdQuery, PageResponse<PartyResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PageResponse<PartyResponse>> Handle(GetByCountryIdQuery query, CancellationToken cancellationToken)
        => new()
        {
            Data = await _context.Parties
                .AsNoTracking()
                .Paginate(query.Page)
                .Where(x => x.CountryId == query.Id)
                .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.Page.PageNumber,
            PageSize = query.Page.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
}