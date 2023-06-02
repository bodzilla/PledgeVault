using System.Collections.Generic;
using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Core.Exceptions;
using PledgeVault.Services.Queries.Countries;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetCountriesByNameQueryHandler : IRequestHandler<GetCountriesByNameQuery, IEnumerable<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetCountriesByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryResponse>> Handle(GetCountriesByNameQuery query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        return await _context.Countries.AsNoTracking()
            .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
            .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}