﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PledgeVault.Services.Queries;
using PledgeVault.Core.Models;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class GetByNameQueryHandler : IRequestHandler<GetByNameQuery<PoliticianResponse>, Page<PoliticianResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PoliticianResponse>> Handle(GetByNameQuery<PoliticianResponse> query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        var dbSet = _context.Set<Politician>();

        return new Page<PoliticianResponse>
        {
            Data = await dbSet
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
                .PaginateFrom(query.PageOptions)
                .ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await dbSet.CountAsync(cancellationToken)
        };
    }
}