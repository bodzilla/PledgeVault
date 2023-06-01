using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
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

    public async Task<ICollection<PoliticianResponse>> GetAllAsync() => await _context.Politicians.AsNoTracking().ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<PoliticianResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Politicians.Where(x => x.Id == id).ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<PoliticianResponse>> GetByPartyIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Politicians.Where(x => x.PartyId == id).ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ICollection<PoliticianResponse>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Politician.Name)} is invalid", nameof(name));
        return await _context.Politicians.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ProjectTo<PoliticianResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<PoliticianResponse> AddAsync(AddPoliticianRequest request)
    {
        var entity = _mapper.Map<Politician>(request);
        await _context.Politicians.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PoliticianResponse>(entity);
    }

    public async Task<PoliticianResponse> UpdateAsync(UpdatePoliticianRequest request) => await UpdateEntityAndSave(_mapper.Map<Politician>(request), true);

    public async Task<PoliticianResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Politicians.FindAsync(id) ?? throw new ArgumentException($"{nameof(Politician)} not found", nameof(Politician.Id)), false);
    }

    private async Task<PoliticianResponse> UpdateEntityAndSave(Politician entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Politicians.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PoliticianResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Politician.Id)} is invalid", nameof(id));
    }
}