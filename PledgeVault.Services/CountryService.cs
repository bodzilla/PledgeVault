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
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;

namespace PledgeVault.Services;

public sealed class CountryService : ICountryService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public CountryService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<CountryResponse>> GetAllAsync() => await _context.Countries.AsNoTracking().ProjectTo<CountryResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<CountryResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Countries.Where(x => x.Id == id).ProjectTo<CountryResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<CountryResponse>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Country.Name)} is invalid", nameof(name));
        return await _context.Countries.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ProjectTo<CountryResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ICollection<CountryResponse>> GetByGovernmentTypeAsync(GovernmentType type) => await _context.Countries.AsNoTracking().Where(x => x.GovernmentType == type).ProjectTo<CountryResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<CountryResponse> AddAsync(AddCountryRequest request)
    {
        var entity = _mapper.Map<Country>(request);
        await _context.Countries.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<CountryResponse>(entity);
    }

    public async Task<CountryResponse> UpdateAsync(UpdateCountryRequest request) => await UpdateEntityAndSave(_mapper.Map<Country>(request), true);

    public async Task<CountryResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Countries.FindAsync(id) ?? throw new ArgumentException($"{nameof(Country)} not found", nameof(Country.Id)), false);
    }

    private async Task<CountryResponse> UpdateEntityAndSave(Country entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Countries.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<CountryResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));
    }
}