using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Politicians;

internal sealed class AddCommandHandler : IRequestHandler<AddCommand<AddPoliticianRequest, PoliticianResponse>, PoliticianResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IPoliticianEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, IPoliticianEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<PoliticianResponse> Handle(AddCommand<AddPoliticianRequest, PoliticianResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Politician>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        await _context.Politicians.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PoliticianResponse>(entity);
    }
}