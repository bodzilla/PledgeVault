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

    public async Task<ICollection<Country>> GetAllAsync() => await _context.Countries.AsNoTracking().ToListAsync();

    public async Task<Country> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Countries.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Country>> GetByNameAsync(string name)
    {
        if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(Country.Name)} is invalid", nameof(name));
        return await _context.Countries.AsNoTracking().Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
    }

    public async Task<Country> AddAsync(AddCountryRequest request)
    {
        var entity = _mapper.Map<Country>(request);
        await _context.Countries.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Country> UpdateAsync(UpdateCountryRequest request) => await UpdateEntityAndSave(_mapper.Map<Country>(request), true);

    public async Task<Country> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Countries.FindAsync(id) ?? throw new ArgumentException($"{nameof(Country)} not found", nameof(Country.Id)), false);
    }

    private async Task<Country> UpdateEntityAndSave(Country entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Countries.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Country.Id)} is invalid", nameof(id));
    }
}