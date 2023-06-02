using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers;

public sealed class AddCommandHandler<TEntity, TRequest, TResponse> : IRequestHandler<AddCommand<TRequest, TResponse>, TResponse>
    where TEntity : class, IEntity
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TResponse> Handle(AddCommand<TRequest, TResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(command.Request);
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<TResponse>(entity);
    }
}