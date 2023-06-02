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
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class GetCountriesByNameQueryHandler : IRequestHandler<GetCountriesByNameQuery, PaginationResponse<CountryResponse>>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetCountriesByNameQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<CountryResponse>> Handle(GetCountriesByNameQuery query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(query.Name)) throw new InvalidRequestException();

        return new PaginationResponse<CountryResponse>
        {
            Data = await _context.Countries
                .AsNoTracking()
                .Skip((query.PaginationQuery.PageNumber - 1) * query.PaginationQuery.PageSize)
                .Take(query.PaginationQuery.PageSize)
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{query.Name.ToLower()}%"))
                .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken),
            PageNumber = query.PaginationQuery.PageNumber,
            PageSize = query.PaginationQuery.PageSize,
            TotalItems = await _context.Countries.CountAsync(cancellationToken)
        };
    }
}