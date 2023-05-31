using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PledgeService : IPledgeService
{
    private readonly PledgeVaultContext _context;

    public PledgeService(PledgeVaultContext context) => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Pledge>> GetAllAsync() => await _context.Pledges.AsNoTracking().ToListAsync();

    public async Task<Pledge> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Pledges.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Pledge>> GetByPoliticianIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Pledges.AsNoTracking().Where(x => x.PoliticianId == id).ToListAsync();
    }

    public async Task<ICollection<Pledge>> GetByTitleAsync(string title)
    {
        if (String.IsNullOrWhiteSpace(title)) throw new ArgumentException($"{nameof(Pledge.Title)} is invalid", nameof(title));
        return await _context.Pledges.AsNoTracking().Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{title.ToLower()}%")).ToListAsync();
    }

    public async Task<Pledge> AddAsync(Pledge entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Pledges.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Pledge> UpdateAsync(Pledge entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity, true);
    }

    public async Task<Pledge> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Pledges.FindAsync(id) ?? throw new ArgumentException($"{nameof(Pledge)} not found", nameof(Pledge.Id)), false);
    }

    private async Task<Pledge> UpdateEntityAndSave(Pledge entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Pledges.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    private void ValidateEntity(Pledge entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.Title)) throw new ArgumentException($"{nameof(Pledge.Title)} is invalid", nameof(entity.Title));
        if (entity.DatePledged > DateTime.Now) throw new ArgumentException($"{nameof(Pledge.DatePledged)} is invalid", nameof(entity.DatePledged));
        if (entity.DateFulfilled.HasValue && entity.DateFulfilled.Value > DateTime.Now) throw new ArgumentException($"{nameof(Pledge.DateFulfilled)} is invalid", nameof(entity.DateFulfilled));
        DetachExternalEntities(entity);
    }

    private void DetachExternalEntities(Pledge entity)
    {
        if (entity.Politician is not null) _context.Entry(entity.Politician).State = EntityState.Detached;
        foreach (var resource in entity.Resources) _context.Entry(resource).State = EntityState.Detached;
    }

    private static void ValidateNewId(Pledge entity)
    {
        if (entity?.Id is not 0) throw new ArgumentException($"{nameof(Pledge.Id)} should not be set", nameof(Pledge.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Pledge.Id)} is invalid", nameof(id));
    }
}