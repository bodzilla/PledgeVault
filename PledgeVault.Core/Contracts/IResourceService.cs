using PledgeVault.Core.Models;
using System;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Contracts;

public interface IResourceService : IService<Resource, AddResourceRequest, UpdateResourceRequest>, IDisposable
{
}