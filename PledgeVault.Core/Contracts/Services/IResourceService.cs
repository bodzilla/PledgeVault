using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts.Services;

public interface IResourceService : IService<AddResourceRequest, UpdateResourceRequest, ResourceResponse>, IDisposable
{
    Task<ICollection<ResourceResponse>> GetByPledgeIdAsync(int id);
}