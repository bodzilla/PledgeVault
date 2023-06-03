using AutoMapper;
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

namespace PledgeVault.Services.Handlers.Resources;

public sealed class GetByIdQueryHandler : IRequestHandler<GetByIdQuery<ResourceResponse>, ResourceResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public GetByIdQueryHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResourceResponse> Handle(GetByIdQuery<ResourceResponse> query, CancellationToken cancellationToken)
    {
        if (query.Id <= 0) throw new InvalidRequestException();

        return await _context.Resources
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}