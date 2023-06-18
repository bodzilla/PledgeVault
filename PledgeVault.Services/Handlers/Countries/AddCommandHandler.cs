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
using Microsoft.EntityFrameworkCore;

namespace PledgeVault.Services.Handlers.Countries;

internal sealed class AddCommandHandler : IRequestHandler<AddCommand<AddCountryRequest, CountryResponse>, CountryResponse>
{
    private readonly IDbContextFactory<PledgeVaultContext> _contextFactory;
    private readonly ICountryEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public AddCommandHandler(IDbContextFactory<PledgeVaultContext> contextFactory, ICountryEntityValidator entityValidator, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(AddCommand<AddCountryRequest, CountryResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Country>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.Countries.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryResponse>(entity);
    }
}