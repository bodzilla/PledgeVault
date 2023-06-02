using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Parties;

public sealed class GetByNameQueryHandler : IRequestHandler<GetByNameQuery<PartyResponse>, Page<PartyResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<PartyResponse>> Handle(GetByNameQuery<PartyResponse> query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        return new Page<PartyResponse>
        {
            Data = await _context.Parties
                .AsNoTracking()
                .PaginateFrom(query.PageOptions)
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
                .ProjectTo<PartyResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await _context.Parties.CountAsync(cancellationToken)
        };
    }
}