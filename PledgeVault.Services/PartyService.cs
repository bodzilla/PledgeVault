using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PartyService : IPartyService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public PartyService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Party>> GetAllAsync() => await _context.Parties.AsNoTracking().ToListAsync();

    public async Task<Party> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Parties.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }


    public async Task<ICollection<Party>> GetByCountryIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Parties.AsNoTracking().Where(x => x.CountryId == id).ToListAsync();
    }

    public async Task<ICollection<Party>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Party.Name)} is invalid", nameof(name));
        return await _context.Parties.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
    }

    public async Task<Party> AddAsync(AddPartyRequest request)
    {
        var entity = _mapper.Map<Party>(request);
        await _context.Parties.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Party> UpdateAsync(UpdatePartyRequest request) => await UpdateEntityAndSave(_mapper.Map<Party>(request), true);


    public async Task<Party> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Parties.FindAsync(id) ?? throw new ArgumentException($"{nameof(Party)} not found", nameof(Party.Id)), false);
    }

    private async Task<Party> UpdateEntityAndSave(Party entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Party.Id)} is invalid", nameof(id));
    }
}