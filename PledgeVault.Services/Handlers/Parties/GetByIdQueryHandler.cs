﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PledgeVault.Services.Queries;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery<PartyResponse>, PartyResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PartyResponse> Handle(GetByIdQuery<PartyResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Parties
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}