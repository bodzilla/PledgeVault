using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Core.Contracts;

public interface IResourceService : IService<AddResourceRequest, UpdateResourceRequest, ResourceResponse>, IDisposable
{
    Task<ICollection<ResourceResponse>> GetByPledgeIdAsync(int id);
}