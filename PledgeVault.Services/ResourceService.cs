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

public sealed class ResourceService : IResourceService
{
    private readonly PledgeVaultContext _context;
    private readonly IMapper _mapper;

    public ResourceService(PledgeVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Dispose() => _context?.Dispose();

    public async Task<ICollection<ResourceResponse>> GetAllAsync() => await _context.Resources.AsNoTracking().ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider).ToListAsync();

    public async Task<ResourceResponse> GetByIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Resources.Where(x => x.Id == id).ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
    }

    public async Task<ICollection<ResourceResponse>> GetByPledgeIdAsync(int id)
    {
        ValidateExistingId(id);
        return await _context.Resources.Where(x => x.PledgeId == id).ProjectTo<ResourceResponse>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ResourceResponse> AddAsync(AddResourceRequest request)
    {
        var entity = _mapper.Map<Resource>(request);
        await _context.Resources.AddAsync(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ResourceResponse>(entity);
    }

    public async Task<ResourceResponse> UpdateAsync(UpdateResourceRequest request) => await UpdateEntityAndSave(_mapper.Map<Resource>(request), true);


    public async Task<ResourceResponse> SetInactiveAsync(int id)
    {
        ValidateExistingId(id);
        return await UpdateEntityAndSave(await _context.Resources.FindAsync(id) ?? throw new ArgumentException($"{nameof(Resource)} not found", nameof(Resource.Id)), false);
    }

    private async Task<ResourceResponse> UpdateEntityAndSave(Resource entity, bool entityActive)
    {
        entity.EntityActive = entityActive;
        entity.EntityModified = DateTime.Now;
        _context.Resources.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ResourceResponse>(entity);
    }

    private static void ValidateExistingId(int id)
    {
        if (id <= 0) throw new ArgumentException($"{nameof(Resource.Id)} is invalid", nameof(id));
    }
}