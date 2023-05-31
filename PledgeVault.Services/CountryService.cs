using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class CountryService : ICountryService
{
    private readonly PledgeVaultContext _context;

    public CountryService(PledgeVaultContext context) => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Country>> GetAllAsync() => await _context.Countries.AsNoTracking().ToListAsync();

    public async Task<Country> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Countries.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Country>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Country.Name)} is invalid", nameof(name));
        return await _context.Countries.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
    }

    public async Task<Country> AddAsync(Country entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Countries.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Country> UpdateAsync(Country entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity);
    }

    public async Task<Country> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);

        var entity = await _context.Countries.FindAsync(id) ?? throw new ArgumentException($"{nameof(Country)} not found", nameof(Country.Id));
        entity.EntityActive = false;

        return await UpdateEntityAndSave(entity);
    }

    private async Task<Country> UpdateEntityAndSave(Country entity)
    {
        entity.EntityActive = true;
        entity.EntityModified = DateTime.Now;
        _context.Countries.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    private void ValidateEntity(Country entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.Name)) throw new ArgumentException($"{nameof(Country.Name)} is invalid", nameof(entity.Name));
        if (entity.DateEstablished.HasValue && entity.DateEstablished.Value > DateTime.Now) throw new ArgumentException($"{nameof(Country.DateEstablished)} is invalid", nameof(entity.DateEstablished));
        DetachExternalEntities(entity);
    }

    private void DetachExternalEntities(Country entity)
    {
        foreach (var party in entity.Parties) _context.Entry(party).State = EntityState.Detached;
    }

    private static void ValidateNewId(Country entity)
    {
        if (entity?.Id != 0) throw new ArgumentException($"{nameof(Country.Id)} should not be set", nameof(Country.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));
    }
}