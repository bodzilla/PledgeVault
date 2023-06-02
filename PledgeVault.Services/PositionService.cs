using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public async Task<ICollection<PositionResponse>> GetAllAsync() => await _context.Positions.AsNoTracking().ProjectTo<PositionResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<PositionResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Positions.Where(x => x.Id == id).ProjectTo<PositionResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<PositionResponse>> GetByTitleAsync(string title)
    {
        if (String.IsNullOrWhiteSpace(title)) throw new ArgumentException($"{nameof(Position.Title)} is invalid", nameof(title));
        return await _context.Positions.AsNoTracking().Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{title.ToLower()}%")).ProjectTo<PositionResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<PositionResponse> AddAsync(AddPositionRequest request)
    {
        var entity = _mapper.Map<Position>(request);
        await _context.Positions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PositionResponse>(entity);
    }

    public async Task<PositionResponse> UpdateAsync(UpdatePositionRequest request) => await UpdateEntityAndSave(_mapper.Map<Position>(request), true);


    public async Task<PositionResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Positions.FindAsync(id) ?? throw new ArgumentException($"{nameof(Position)} not found", nameof(Position.Id)), false);
    }

    private async Task<PositionResponse> UpdateEntityAndSave(Position entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Positions.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PositionResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Position.Id)} is invalid", nameof(id));
    }
}