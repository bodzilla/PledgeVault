using AutoMapper;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Persistence;
using PledgeVault.Core.Models;
using PledgeVault.Services.Commands.Countries;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace PledgeVault.Services.Handlers.Countries;

public sealed class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public UpdateCountryCommandHandler(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryResponse> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Country>(command.Request);
        entity.EntityModified = DateTime.Now;
        _context.Countries.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryResponse>(entity);
    }
}