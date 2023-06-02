﻿using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Core.Exceptions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Services.Queries;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByIdQuery<CountryResponse>, CountryResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(GetByIdQuery<CountryResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Countries
            .Where(x => x.Id == query.Id)
            .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);
    }
}