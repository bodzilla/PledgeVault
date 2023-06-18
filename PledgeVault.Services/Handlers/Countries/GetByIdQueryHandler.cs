using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Persistence.Extensions;
using PledgeVault.Services.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Countries;

internal sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByIdQuery<CountryResponse>, CountryResponse>
{
    private readonly IDbContextFactory<PledgeVaultContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(IDbContextFactory<PledgeVaultContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(GetByIdQuery<CountryResponse> query, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        if (query.Id <= 0) throw new InvalidRequestException();

        return await context.Countries
            .AsNoTracking()
            .WithOnlyActiveEntities()
            .Where(x => x.Id == query.Id)
            .ProjectTo<CountryResponse>(_mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}