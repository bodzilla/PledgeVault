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

public sealed class PoliticianService : IPoliticianService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public PoliticianService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Politician>> GetAllAsync() => await _context.Politicians.AsNoTracking().ToListAsync();

    public async Task<Politician> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Politicians.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Politician>> GetByPartyIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Politicians.AsNoTracking().Where(x => x.PartyId == id).ToListAsync();
    }

    public async Task<ICollection<Politician>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Politician.Name)} is invalid", nameof(name));
        return await _context.Politicians.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
    }

    public async Task<Politician> AddAsync(AddPoliticianRequest request)
    {
        var entity = _mapper.Map<Politician>(request);
        await _context.Politicians.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Politician> UpdateAsync(UpdatePoliticianRequest request) => await UpdateEntityAndSave(_mapper.Map<Politician>(request), true);

    public async Task<Politician> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Politicians.FindAsync(id) ?? throw new ArgumentException($"{nameof(Politician)} not found", nameof(Politician.Id)), false);
    }

    private async Task<Politician> UpdateEntityAndSave(Politician entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Politicians.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Politician.Id)} is invalid", nameof(id));
    }
}