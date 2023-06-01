using PledgeVault.Core.Models;
using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts;

public interface IResourceService : IService<Resource, AddResourceRequest, UpdateResourceRequest>, IDisposable
{
    Task<ICollection<Resource>> GetByPledgeIdAsync(int id);
}