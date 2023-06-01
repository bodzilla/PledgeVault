using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts.Services;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class PledgeService : IPledgeService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public PledgeService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<PledgeResponse>> GetAllAsync() => await _context.Pledges.AsNoTracking().ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<PledgeResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Pledges.Where(x => x.Id == id).ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<PledgeResponse>> GetByPoliticianIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Pledges.Where(x => x.PoliticianId == id).ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ICollection<PledgeResponse>> GetByTitleAsync(string title)
    {
        if (String.IsNullOrWhiteSpace(title)) throw new ArgumentException($"{nameof(Pledge.Title)} is invalid", nameof(title));
        return await _context.Pledges.AsNoTracking().Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{title.ToLower()}%")).ProjectTo<PledgeResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<PledgeResponse> AddAsync(AddPledgeRequest request)
    {
        var entity = _mapper.Map<Pledge>(request);
        await _context.Pledges.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PledgeResponse>(entity);
    }

    public async Task<PledgeResponse> UpdateAsync(UpdatePledgeRequest request) => await UpdateEntityAndSave(_mapper.Map<Pledge>(request), true);


    public async Task<PledgeResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Pledges.FindAsync(id) ?? throw new ArgumentException($"{nameof(Pledge)} not found", nameof(Pledge.Id)), false);
    }

    private async Task<PledgeResponse> UpdateEntityAndSave(Pledge entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Pledges.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PledgeResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Pledge.Id)} is invalid", nameof(id));
    }
}