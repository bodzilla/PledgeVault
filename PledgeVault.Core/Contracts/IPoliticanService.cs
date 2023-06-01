using PledgeVault.Core.Models;
using System;
using PledgeVault.Core.Dtos.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PledgeVault.Core.Contracts;

public interface IPoliticianService : IService<Politician, AddPoliticianRequest, UpdatePoliticianRequest>, IDisposable
{
    Task<ICollection<Politician>> GetByPartyIdAsync(int id);

    Task<ICollection<Politician>> GetByNameAsync(string name);
}