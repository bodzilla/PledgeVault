using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetByNameQueryHandler : IRequestHandler<GetByNameQuery<CountryResponse>, Page<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Page<CountryResponse>> Handle(GetByNameQuery<CountryResponse> query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        var dbSet = _context.Set<Country>();

        return new Page<CountryResponse>
        {
            Data = await dbSet
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
                .PaginateFrom(query.PageOptions)
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
                .ToListAsync(cancellationToken),
            PageNumber = query.PageOptions.PageNumber,
            PageSize = query.PageOptions.PageSize,
            TotalItems = await dbSet.CountAsync(cancellationToken)
        };
    }
}