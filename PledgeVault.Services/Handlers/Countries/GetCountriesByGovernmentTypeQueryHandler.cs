﻿using System.Collections.Generic;
using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Services.Queries.Countries;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetCountriesByGovernmentTypeQueryHandler : IRequestHandler<GetCountriesByGovernmentTypeQuery, IEnumerable<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetCountriesByGovernmentTypeQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryResponse>> Handle(GetCountriesByGovernmentTypeQuery query, CancellationToken cancellationToken) =>
        await _context.Countries.AsNoTracking().Where(x => x.GovernmentType == query.GovernmentType)
            .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
}