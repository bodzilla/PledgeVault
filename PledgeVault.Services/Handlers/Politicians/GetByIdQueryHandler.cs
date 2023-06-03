using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Exceptions;
using PledgeVault.Persistence;
using PledgeVault.Services.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

public sealed class GetByCountryIdQueryHandler : IRequestHandler<GetByIdQuery<PoliticianResponse>, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByCountryIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(GetByIdQuery<PoliticianResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Politicians
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}