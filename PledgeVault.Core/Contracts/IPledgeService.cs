using PledgeVault.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Contracts;

public interface IPledgeService : IService<Pledge, AddPledgeRequest, UpdatePledgeRequest>, IDisposable
{
    Task<ICollection<Pledge>> GetByPoliticianIdAsync(int id);

    Task<ICollection<Pledge>> GetByTitleAsync(string title);
}