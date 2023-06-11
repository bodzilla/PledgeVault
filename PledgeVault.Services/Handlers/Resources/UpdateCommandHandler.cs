using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Resources;

internal sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdateResourceRequest, ResourceResponse>, ResourceResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IResourceEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, IResourceEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<ResourceResponse> Handle(UpdateCommand<UpdateResourceRequest, ResourceResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Resource>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Resources.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResourceResponse>(entity);
    }
}