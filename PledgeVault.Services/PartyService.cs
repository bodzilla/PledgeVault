﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PartyService : IPartyService
{
    private readonly PledgeVaultContext _context;

    public PartyService(PledgeVaultContext context) => _context = context;

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

    public async Task<Party> AddAsync(Party entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Parties.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Party> UpdateAsync(Party entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity, true);
    }

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

    private void ValidateEntity(Party entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.Name)) throw new ArgumentException($"{nameof(Party.Name)} is invalid", nameof(entity.Name));
        if (entity.DateEstablished.HasValue && entity.DateEstablished.Value > DateTime.Now) throw new ArgumentException($"{nameof(Party.DateEstablished)} is invalid", nameof(entity.DateEstablished));
        DetachExternalEntities(entity);
    }

    private void DetachExternalEntities(Party entity)
    {
        if (entity.Country is not null) _context.Entry(entity.Country).State = EntityState.Detached;
        foreach (var politician in entity.Politicians) _context.Entry(politician).State = EntityState.Detached;
    }

    private static void ValidateNewId(Party entity)
    {
        if (entity?.Id is not 0) throw new ArgumentException($"{nameof(Party.Id)} should not be set", nameof(Party.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Party.Id)} is invalid", nameof(id));
    }
}