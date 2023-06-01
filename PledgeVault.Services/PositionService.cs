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

public sealed class PositionService : IPositionService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public PositionService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<Position>> GetAllAsync() => await _context.Positions.AsNoTracking().ToListAsync();

    public async Task<Position> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Positions.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Position>> GetByTitleAsync(string title)
    {
        if (String.IsNullOrWhiteSpace(title)) throw new ArgumentException($"{nameof(Position.Title)} is invalid", nameof(title));
        return await _context.Positions.AsNoTracking().Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{title.ToLower()}%")).ToListAsync();
    }

    public async Task<Position> AddAsync(AddPositionRequest request)
    {
        var entity = _mapper.Map<Position>(request);
        await _context.Positions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Position> UpdateAsync(UpdatePositionRequest request) => await UpdateEntityAndSave(_mapper.Map<Position>(request), true);


    public async Task<Position> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Positions.FindAsync(id) ?? throw new ArgumentException($"{nameof(Position)} not found", nameof(Position.Id)), false);
    }

    private async Task<Position> UpdateEntityAndSave(Position entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Positions.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Position.Id)} is invalid", nameof(id));
    }
}