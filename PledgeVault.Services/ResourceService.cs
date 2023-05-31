using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class ResourceService : IResourceService
{
    private readonly PledgeVaultContext _context;

    public ResourceService(PledgeVaultContext context) => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Resource>> GetAllAsync() => await _context.Resources.AsNoTracking().ToListAsync();

    public async Task<Resource> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Resources.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Resource> AddAsync(Resource entity)
    {
        ValidateEntity(entity);
        ValidateNewId(entity);

        await _context.Resources.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Resource> UpdateAsync(Resource entity)
    {
        ValidateExistingId(entity.Id);
        ValidateEntity(entity);

        return await UpdateEntityAndSave(entity);
    }

    public async Task<Resource> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);

        var entity = await _context.Resources.FindAsync(id) ?? throw new ArgumentException($"{nameof(Resource)} not found", nameof(Resource.Id));
        entity.EntityActive = false;

        return await UpdateEntityAndSave(entity);
    }

    private async Task<Resource> UpdateEntityAndSave(Resource entity)
    {
        entity.EntityActive = true;
        entity.EntityModified = DateTime.Now;
        _context.Resources.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    private void ValidateEntity(Resource entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        if (String.IsNullOrWhiteSpace(entity.Title)) throw new ArgumentException($"{nameof(Resource.Title)} is invalid", nameof(entity.Title));
    }

    private static void ValidateNewId(Resource entity)
    {
        if (entity?.Id is not 0) throw new ArgumentException($"{nameof(Resource.Id)} should not be set", nameof(Resource.Id));
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Resource.Id)} is invalid", nameof(id));
    }
}