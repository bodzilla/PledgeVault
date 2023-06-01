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

public sealed class PartyService : IPartyService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public PartyService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<PartyResponse>> GetAllAsync() => await _context.Parties.AsNoTracking().ProjectTo<PartyResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<PartyResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Parties.Where(x => x.Id == id).ProjectTo<PartyResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<PartyResponse>> GetByCountryIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Parties.Where(x => x.CountryId == id).ProjectTo<PartyResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ICollection<PartyResponse>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Party.Name)} is invalid", nameof(name));
        return await _context.Parties.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ProjectTo<PartyResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<PartyResponse> AddAsync(AddPartyRequest request)
    {
        var entity = _mapper.Map<Party>(request);
        await _context.Parties.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PartyResponse>(entity);
    }

    public async Task<PartyResponse> UpdateAsync(UpdatePartyRequest request) => await UpdateEntityAndSave(_mapper.Map<Party>(request), true);


    public async Task<PartyResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Parties.FindAsync(id) ?? throw new ArgumentException($"{nameof(Party)} not found", nameof(Party.Id)), false);
    }

    private async Task<PartyResponse> UpdateEntityAndSave(Party entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Parties.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<PartyResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Party.Id)} is invalid", nameof(id));
    }
}