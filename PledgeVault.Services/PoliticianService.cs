using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PoliticianService : IPoliticianService
{
    private readonly PledgeVaultContext _context;

    public PoliticianService(PledgeVaultContext context) => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Politician>> GetAllAsync() => await _context.Politicians.AsNoTracking().ToListAsync();

    public async Task<Politician> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Politicians.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Politician> AddAsync(Politician entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Politicians.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Politician> UpdateAsync(Politician entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity, true);
    }

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

    private void ValidateEntity(Politician entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.CountryOfBirth)) throw new ArgumentException($"{nameof(Politician.CountryOfBirth)} is invalid", nameof(entity.CountryOfBirth));
        if (entity.DateOfBirth > DateTime.Now) throw new ArgumentException($"{nameof(Politician.DateOfBirth)} is invalid", nameof(entity.DateOfBirth));
        if (entity.DateOfDeath.HasValue && entity.DateOfDeath.Value > DateTime.Now) throw new ArgumentException($"{nameof(Politician.DateOfDeath)} is invalid", nameof(entity.DateOfDeath));
        DetachExternalEntities(entity);
    }

    private void DetachExternalEntities(Politician entity)
    {
        if (entity.Party is not null) _context.Entry(entity.Party).State = EntityState.Detached;
        if (entity.Position is not null) _context.Entry(entity.Position).State = EntityState.Detached;
        foreach (var pledge in entity.Pledges) _context.Entry(pledge).State = EntityState.Detached;
    }

    private static void ValidateNewId(Politician entity)
    {
        if (entity?.Id is not 0) throw new ArgumentException($"{nameof(Politician.Id)} should not be set", nameof(Politician.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Politician.Id)} is invalid", nameof(id));
    }
}