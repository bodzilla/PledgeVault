using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PositionService : IPositionService
{
    private readonly PledgeVaultContext _context;

    public PositionService(PledgeVaultContext context) => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Position>> GetAllAsync() => await _context.Positions.AsNoTracking().ToListAsync();

    public async Task<Position> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Positions.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Position> AddAsync(Position entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Positions.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Position> UpdateAsync(Position entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity);
    }

    public async Task<Position> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);

        var entity = await _context.Positions.FindAsync(id) ?? throw new ArgumentException($"{nameof(Position)} not found", nameof(Position.Id));
        entity.EntityActive = false;

        return await UpdateEntityAndSave(entity);
    }

    private async Task<Position> UpdateEntityAndSave(Position entity)
    {
        entity.EntityModified = DateTime.Now;
        _context.Positions.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    private void ValidateEntity(Position entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.Title)) throw new ArgumentException($"{nameof(Position.Title)} is invalid", nameof(entity.Title));
        DetachExternalEntities(entity);
    }

    private void DetachExternalEntities(Position entity)
    {
        foreach (var politician in entity.Politicians) _context.Entry(politician).State = EntityState.Detached;
    }

    private static void ValidateNewId(Position entity)
    {
        if (entity?.Id != 0) throw new ArgumentException($"{nameof(Position.Id)} should not be set", nameof(Position.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Position.Id)} is invalid", nameof(id));
    }
}